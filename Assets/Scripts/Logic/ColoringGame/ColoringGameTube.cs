using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Logic.ColoringGame
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ColoringGameTube : MonoBehaviour, IBeginDragHandler, 
        IDragHandler, IEndDragHandler
    {
        [field:SerializeField] public Color Color { get; private set; }
        [SerializeField] private Transform _defaultParentTransform;
        [SerializeField] private Transform _dragParentTransform;
        private CanvasGroup _canvasGroup;
        public bool Ready;

        private void Awake()
        {
            Ready = true;
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
            
            transform.SetParent(_defaultParentTransform, true);
            transform.localPosition = new Vector3(0, 0);
            Ready = true;
        }
    }
}