using System;
using UnityEngine;

public class ColoringGameCompleteController : MonoBehaviour
{
    [SerializeField] private ColoringGamaElement[] _coloringGamaElements;
    [SerializeField] private Animator _completeFormAnimator;
    private int _activeElementsCounter;

    private void Awake()
    {
        _activeElementsCounter = 0;
        foreach (var coloringGamaElement in _coloringGamaElements)
        {
            coloringGamaElement.Activated += AddActiveElement;
        }
    }

    private void AddActiveElement()
    {
        _activeElementsCounter++;
        if (_activeElementsCounter >= _coloringGamaElements.Length)
            CompleteGame();
    }

    private void CompleteGame()
    {
        _completeFormAnimator.SetTrigger("Activate");
    }
    
}
