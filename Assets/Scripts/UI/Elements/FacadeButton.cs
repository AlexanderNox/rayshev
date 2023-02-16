using System;
using UnityEngine;
using UnityEngine.UI;

public class FacadeButton : MonoBehaviour
{
    [SerializeField] private Button _choseButton;
    [SerializeField] private Animator _animator;
    private bool _chosen;

    public event Action<FacadeButton> Chosen;

    public void UnChose()
    {
        _chosen = false;
        _animator.SetBool("Chosen", false);
    }
    
    private void Awake()
    {
        _choseButton.onClick.AddListener(Chose);
    }

    private void Chose()
    {
        _chosen = true;
        Chosen.Invoke(this);
        _animator.SetBool("Chosen", true);
    }
}
