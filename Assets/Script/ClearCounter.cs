using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour,IkitchenObject
{
    [SerializeField] private KitchenObjectSo kitchenObjectSo;
    [SerializeField] private Transform TableTop;
   
   

    private KitchenObject KitchenObject;

  
    public void Interact(Player player)
    {
        if (KitchenObject==null)
        {
            Debug.Log("Interact");
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSo.prefab, TableTop);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
          
        }
        else
        {
            KitchenObject.SetKitchenObjectParent(player);
           // Debug.Log(KitchenObject.GetKitchenObjectParent());
        }

    }
    public Transform GetKitchenObjectFollowTransform()
    {
        return TableTop;
    }
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.KitchenObject = kitchenObject;
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
