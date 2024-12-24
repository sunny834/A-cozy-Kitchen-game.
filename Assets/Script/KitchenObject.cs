using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSo KitchenObjectSo;
    private IkitchenObject KitchenObjectParent;
    public KitchenObjectSo GetKitchenObjectSo() { return KitchenObjectSo; }

    public void SetKitchenObjectParent(IkitchenObject KitchenObjectParent)
    {
        if(this.KitchenObjectParent!=null)
        {
            this.KitchenObjectParent.ClearKitchenObject();
        }
        this.KitchenObjectParent = KitchenObjectParent;
        if(KitchenObjectParent.HaskitchenObject())
        {
            Debug.LogError("Another onject already there!");
        }
        KitchenObjectParent.SetKitchenObject(this);
        transform.parent=KitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;

    }
    public IkitchenObject GetKitchenObjectParent() { return KitchenObjectParent; }

   
}
