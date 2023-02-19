using UnityEngine;
using UnityEngine.UI;

public class MosaicGameController : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private MosaicMen[] _mosaicMens;
    [SerializeField] private Animator _completeFormAnimator;
    private int _countMenInHolder;
    private void Awake()
    {
        _countMenInHolder = 0;
    }
    
    private void OnEnable()
    {
        _startButton.onClick.AddListener(StartGame);
        foreach (var mosaicMen in _mosaicMens)
        {
            mosaicMen.InHolder += AddCountMenInHolder;
        }
    }

    private void StartGame()
    {
        _startButton.gameObject.SetActive(false);
        foreach (var mosaicMen in _mosaicMens)
        {
            mosaicMen.RunAway();
        }
    }
    
    private void AddCountMenInHolder()
    {
        _countMenInHolder++;
        if (_countMenInHolder >= _mosaicMens.Length)
            CompleteGame();
    }

    private void CompleteGame()
    {
        _completeFormAnimator.SetTrigger("Activate");
    }
}