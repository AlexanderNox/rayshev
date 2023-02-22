using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

[RequireComponent(typeof(CanvasGroup))]
public class MosaicMen : MonoBehaviour,  IBeginDragHandler, 
    IDragHandler, IEndDragHandler
{
    [field:SerializeField] public MosaicMenHolder MosaicMenHolder { get; private set; }
    [SerializeField] private Transform _externalPosition;
    [SerializeField] private float _runDistance;
    private CanvasGroup _canvasGroup;
    private bool _dragAvailable;

    public event Action InHolder;
    public event Action OnTarget;
    
    private void Awake()
    {
        _dragAvailable = false;
        _canvasGroup = GetComponent<CanvasGroup>();
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
         
        // transform.SetParent(_dragParentTransform);
        _canvasGroup.blocksRaycasts = false;
    }

    public void MoveToExternalPosition()
    {
        StartCoroutine(Move(_externalPosition.position));
    }

    private IEnumerator Move(Vector3 targetPosition)
    {
        _dragAvailable = false;
        
        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
        float traveledDistance = 0;
        
        while (distanceToTarget > traveledDistance)
        {
            Vector3 currentPosition = transform.position;
            Vector3 moveStep = (targetPosition - transform.position).normalized * 5;
            transform.position += moveStep;
            
            traveledDistance += Vector3.Distance(currentPosition, transform.position);
            
            yield return new WaitForFixedUpdate();
        }
        
        _dragAvailable = true;
        OnTarget?.Invoke();
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        if (_dragAvailable)
        {
            Vector2 touchPos = Input.touches[0].position;
            transform.position = new Vector3(touchPos.x, touchPos.y, 0);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = true;
            
        // transform.SetParent(_defaultParentTransform, true);
        // transform.localPosition = new Vector3(0, 0);
            
        if (eventData != null)
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current) { pointerId = -1, };
            pointerData.position = eventData.position;
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);
            
            foreach (RaycastResult result in results)
            {
                if (result.gameObject.TryGetComponent(out IDropRaycastHandler _dropRaycastHandler))
                {
                    _dropRaycastHandler.OnDropRaycast(gameObject);
                }
            }
        }
    }

    public void InvokeInHolder()
    {
        InHolder?.Invoke();
        _dragAvailable = false;
        MosaicMenHolder.transform.SetAsFirstSibling();
    }
}