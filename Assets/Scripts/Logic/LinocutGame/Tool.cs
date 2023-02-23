using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class Tool : MonoBehaviour, IBeginDragHandler, 
    IDragHandler, IEndDragHandler
{
    // [SerializeField] private Transform _dragParentTransform;
    [SerializeField] private Transform _defaultPosition;
    [SerializeField] private Transform _dragParentTransform;
    [field:SerializeField] public int StepId { get; private set; }
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
         
        transform.SetParent(_dragParentTransform);
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 touchPos = Input.touches[0].position;
        transform.position = new Vector3(touchPos.x, touchPos.y, 0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = true;
            
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
        
        transform.SetParent(_defaultPosition, true);
        transform.localPosition = new Vector3(0, 0);
    }
}
