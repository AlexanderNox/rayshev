using System;
using Logic.ColoringGame;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Animator))]
public class ColoringGamaElement : MonoBehaviour, IDropRaycastHandler
{
    [SerializeField] private ColoringGameColors _coloringGameColor;
    private Animator _animator;
    private bool _active;

    public event Action Activated;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _active = false;
    }

    public void Activate()
    {
        if (_active == false)
        {
            _animator.SetTrigger("Activate");
            _active = true;
            Activated?.Invoke();
        }
    }

    public void OnDropRaycast(GameObject gameObject)
    {
        if (gameObject != null)
        {
            if (gameObject.TryGetComponent(out ColoringGameTube coloringGameTube))
            {
                if (coloringGameTube.ColoringGameColor == _coloringGameColor)
                {
                    Activate();
                }
            }
        }
    }
}