using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSo kitchenObjectSo;
    [SerializeField] private Transform TableTop;
    [SerializeField] private ClearCounter secondClearCounter;
    [SerializeField] private bool testing;

    private KitchenObject KitchenObject;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T) && testing)
        {
            if (KitchenObject != null) {
                KitchenObject.SetClearCounter(secondClearCounter);
                Debug.Log(KitchenObject.GetClearCounter());
            }
        }
    }
    public void Interact()
    {
        if (KitchenObject==null)
        {
            Debug.Log("Interact");
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSo.prefab, TableTop);
            kitchenObjectTransform.localPosition = Vector3.zero;
           // Debug.Log(kitchenObjectTransform.GetComponent<KitchenObject>().GetKitchenObjectSo().objectName);
            KitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
            KitchenObject.SetClearCounter(this);
        }
        else
        {
            Debug.Log(KitchenObject.GetClearCounter());
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
}
