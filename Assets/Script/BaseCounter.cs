using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour,IkitchenObject
{
    public static event EventHandler OnDrop;
   // [SerializeField] private KitchenObjectSo kitchenObjectSo;
    [SerializeField] private Transform TableTop;



    private KitchenObject KitchenObject;

    public static void ResetStaticData()
    {
        OnDrop = null;
    }
    public virtual void Interact(Player player)
    {
        Debug.LogError("BaseCounter.Interact();");
    }
    public virtual void InteractAlternate(Player player)
    {
       // Debug.LogError("BaseCounter.InteractAlternate();");
    }
    public Transform GetKitchenObjectFollowTransform()
    {
        return TableTop;
    }
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.KitchenObject = kitchenObject;
        if (KitchenObject != null)
        {
            OnDrop?.Invoke(this,EventArgs.Empty);
        }
    }
    public KitchenObject GetKitchenObject()
    {
        return KitchenObject;
    }
    public void ClearKitchenObject()
    { KitchenObject = null; }
    public bool HaskitchenObject()
    {
        return KitchenObject != null;
    }
}
