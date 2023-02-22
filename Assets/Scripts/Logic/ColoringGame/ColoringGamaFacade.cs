using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Logic.ColoringGame
{
    public class ColoringGamaFacade : MonoBehaviour, IDropRaycastHandler
    {
        [SerializeField] private Image _image;
        private bool _active;

        private void Awake()
        {
            _active = false;
        }

        private IEnumerator Coloring(Color targetColor, float time)
        {
            float currTime = 0f;

            do 
            {
                _image.color = Color.Lerp (_image.color, targetColor, currTime/time);
                currTime += Time.deltaTime;
                yield return null;
            } 
            while (currTime<=time);
            // yield return new WaitForEndOfFrame();
        }
        public void OnDropRaycast(GameObject gameObject)
        {
            if (gameObject.TryGetComponent(out ColoringGameTube coloringGameTube))
            {
                if (_active == false  && coloringGameTube.Ready)
                {
                    _active = true;
                    StartCoroutine(Coloring(coloringGameTube.Color, 2.5f));
                    coloringGameTube.Ready = false;
                }
            }
        }
    }
}