using System;
using UnityEngine;
using Zenject;

namespace UI.Services
{
    public class WindowService : MonoBehaviour, IWindowService
    {
        [SerializeField] private WindowBase _startWindowBase;
        private Transform _windowParent;
        private WindowBase _currentWindow;
        private DiContainer _diContainer;

        [Inject]
        private void Construct(WindowRoot windowRoot, DiContainer diContainer)
        {
            _windowParent = windowRoot.transform;
            _diContainer = diContainer;
        }

        private void Start()
        {
             Open(_startWindowBase);
        }

        public void Open(WindowBase window)
        {
            if (_currentWindow != null) 
                Close();

            _currentWindow = _diContainer.InstantiatePrefab(window, _windowParent).GetComponent<WindowBase>();
        }
        

        private void Close()
        {
            Destroy(_currentWindow.gameObject);
        }
    }
    
}

