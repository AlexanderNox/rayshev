using System;
using UnityEngine;

namespace UI.Elements
{
    public class FacadeButtonsController : MonoBehaviour
    {
        [SerializeField] private FacadeButton[] _facadeButtons;
        private FacadeButton _chosenFacadeButton;

        private void Awake()
        {
            foreach (var facadeButton in _facadeButtons)
            {
                facadeButton.Chosen += SetChosenButton;
            }
        }

        private void SetChosenButton(FacadeButton facadeButton)
        {
            if(_chosenFacadeButton != null)
                _chosenFacadeButton.UnChose();
            
            _chosenFacadeButton = facadeButton;
            _chosenFacadeButton.transform.SetAsLastSibling();
        }
    }
}