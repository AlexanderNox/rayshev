using System;
using System.Collections;
using Logic.PairingGame;
using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Picture : MonoBehaviour, IPointerClickHandler
{
   public PairingGameController PairingGameController;
   [field:SerializeField] public int ID { get; private set; }
   [SerializeField] private Animator _animator;
   private bool _chose;

   public event Action<Picture> Chosen;
   
   public void Off()
   {
      _animator.SetTrigger("Off");
      // Destroy(this);
   }
   
   private void Chose()
   {
      if (_chose == false)
      {
         _chose = true;
         _animator.SetBool("Chose", true);
         Chosen?.Invoke(this);
      }
   }

   public IEnumerator UnChose()
   {
      if (_chose)
      {
         _chose = false;
         yield return new WaitForSeconds(0.7f);
         _animator.SetBool("Chose", false);
      }
   }

   public void OnPointerClick(PointerEventData eventData)
   {
      if (PairingGameController.Chosens.Count < 2)
      {
         Chose();
      }
   }
}
