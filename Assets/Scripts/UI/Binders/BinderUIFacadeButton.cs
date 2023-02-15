using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UI.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BinderUIFacadeButton : MonoBehaviour
{
   [SerializeField] private WindowBase _windowPrefab;
   [SerializeField] private Button _button;
   private IWindowService _windowService;

   [Inject]
   private void Construct(IWindowService windowService)
   {
      _windowService = windowService;
   }
   private void OnEnable()
   {
      _button.onClick.AddListener(OpenWindow);
   }

   private void OpenWindow()
   {
      _windowService.Open(_windowPrefab);
   }
}
