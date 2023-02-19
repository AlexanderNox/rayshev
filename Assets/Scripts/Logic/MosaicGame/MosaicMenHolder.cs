using UnityEngine;

public class MosaicMenHolder : MonoBehaviour, IDropRaycastHandler
{
    public void OnDropRaycast(GameObject gameObject)
    {
        if (gameObject != null)
        {
            if (gameObject.TryGetComponent(out MosaicMen mosaicMen))
            {
                
                if (mosaicMen.MosaicMenHolder == this)
                {
                    mosaicMen.transform.position = transform.position;
                    mosaicMen.InvokeInHolder();
                }
                else
                {
                    mosaicMen.MoveToHolder();
                }
                  
            }
        }
    }
}