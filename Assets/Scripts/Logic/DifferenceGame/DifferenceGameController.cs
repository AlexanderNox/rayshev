using TMPro;
using UnityEngine;

public class DifferenceGameController : MonoBehaviour
{
    [SerializeField] private DifferenceTrigger[] _differenceTriggers;
    [SerializeField] private Animator _completeFormAnimator;
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;
    private int _activeElementsCounter;

    private void Awake()
    {
        _activeElementsCounter = 0;
        foreach (var coloringGamaElement in _differenceTriggers)
        {
            coloringGamaElement.Activated += AddActiveElement;
        }
    }

    private void AddActiveElement()
    {
        _activeElementsCounter++;
        _textMeshProUGUI.text = $"Найдено {_activeElementsCounter} из {_differenceTriggers.Length} отличий";
        
        if (_activeElementsCounter >= _differenceTriggers.Length)
            CompleteGame();
    }

    private void CompleteGame()
    {
        _completeFormAnimator.SetTrigger("Activate");
    }
}