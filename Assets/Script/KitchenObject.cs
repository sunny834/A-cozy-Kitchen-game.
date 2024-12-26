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

    public void DestroySelf()
    {
        KitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }

   public static KitchenObject SpawnKitchenObject(KitchenObjectSo kitchenObjectSo, IkitchenObject KitchenObjectparent )
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSo.prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(KitchenObjectparent);
        return kitchenObject;
    }
}
