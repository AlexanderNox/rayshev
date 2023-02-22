using System;
using System.Collections.Generic;
using UnityEngine;

namespace Logic.PairingGame
{
    public class PairingGameController : MonoBehaviour
    {
        [SerializeField] private Animator _completeFormAnimator;
        [SerializeField] private Picture[] _pictures;
        private int _pairsFoundCounter; 
        public List<Picture> Chosens { get; private set; }
        
        private void Awake()
        {
            Chosens = new List<Picture>();
            
            foreach (var picture in _pictures)
            {
                picture.PairingGameController = this;
            }
        }

        private void OnEnable()
        {
            foreach (var picture in _pictures)
            {
                picture.Chosen += OnChosen;
            }
        }

        private void OnChosen(Picture picture)
        {
            if (Chosens.Count < 2)
            {
                Chosens.Add(picture);
                if (Chosens.Count == 2)
                {
                    if (Chosens[0].ID == Chosens[1].ID)
                    {
                        foreach (var chosen in Chosens)
                        {
                            chosen.Off();
                        }

                        _pairsFoundCounter++;
                       
                        if (_pairsFoundCounter >= _pictures.Length / 2)
                        {
                            EndGame();
                        }
                    }
                    else
                    {
                        foreach (var chosen in Chosens)
                        {
                            StartCoroutine(chosen.UnChose());
                        }
                    }
                    Chosens.Clear();
                }
            }
        }

        private void EndGame()
        {
            Debug.Log("EndGame");
            _completeFormAnimator.SetTrigger("Activate");
        }
    }
}