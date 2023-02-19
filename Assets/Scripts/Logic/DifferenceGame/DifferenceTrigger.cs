using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button),typeof(Animator))]
public class DifferenceTrigger : MonoBehaviour
{
    private Animator _animator;
    private Button _button;
    private static readonly int Activate1 = Animator.StringToHash("Activate");
    private bool _isActive;

    public event Action Activated; 

    private void Awake()
    {
        _isActive = false;
        _animator = GetComponent<Animator>();
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(Activate);
    }

    private void Activate()
    {
        if (_isActive == false)
        {
            _animator.SetTrigger(Activate1);
            _isActive = true;
            Activated?.Invoke();
        }
    }
}