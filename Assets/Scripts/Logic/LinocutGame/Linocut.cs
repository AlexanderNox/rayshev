using System;
using UnityEngine;


namespace Logic.LinocutGame
{
    [RequireComponent(typeof(Animator))]
    public class Linocut : MonoBehaviour, IDropRaycastHandler
    {
        [SerializeField] private Animator _animator;
        private int _currentStep;
        private bool _animationPlaying; 

        private void Awake()
        {
            _currentStep = 0;
            _animator = GetComponent<Animator>();
        }

        public void OnDropRaycast(GameObject gameObject)
        {
            if (gameObject.TryGetComponent(out Tool tool))
            {
                if (_currentStep + 1 == tool.StepId)
                {
                    _currentStep++;
                    _animator.SetInteger("Step", _currentStep);
                }
            }
        }
    }
}