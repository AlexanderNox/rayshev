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
        private Curtain _curtain;

        [Inject]
        private void Construct(WindowRoot windowRoot, DiContainer diContainer, Curtain curtain)
        {
            _windowParent = windowRoot.transform;
            _diContainer = diContainer;
            _curtain = curtain;
        }

        private void Start()
        {
             Open(_startWindowBase);
        }

        public void Open(WindowBase window)
        {
            _curtain.Show();
            _curtain.WindowHidden += () => SwitchWindow(window);
        }

        private void SwitchWindow(WindowBase window)
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

