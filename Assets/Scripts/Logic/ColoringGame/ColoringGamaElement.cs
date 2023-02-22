using System;
using Logic.ColoringGame;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Animator),(typeof(Image)))]
public class ColoringGamaElement : MonoBehaviour, IDropRaycastHandler
{
    private Image _image;
    private Animator _animator;
    private bool _active;

    public event Action Activated;
    private void Awake()
    {
        _image = GetComponent<Image>();
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
                if (_active == false && coloringGameTube.Ready)  
                {
                    _image.color = coloringGameTube.Color;
                    Activate();
                    coloringGameTube.Ready = false;
                }
            }
        }
    }
}