using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Curtain : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Image _image;
    [SerializeField] private Sprite[] _loadingScreens;
    
    public event Action WindowHidden;
    
    public void Show()
    {
        _image.sprite = _loadingScreens[Random.Range(0, _loadingScreens.Length - 1)];
        _animator.SetTrigger("Show");
    }

    public void InvokeWindowHidden()
    {
        WindowHidden?.Invoke();
    }
   
}
