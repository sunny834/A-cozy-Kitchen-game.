using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSo kitchenObjectSo;
    [SerializeField] private Transform TableTop;

    private KitchenObject KitchenObject;
  public void Interact()
    {
        if (KitchenObject==null)
        {
            Debug.Log("Interact");
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSo.prefab, TableTop);
            kitchenObjectTransform.localPosition = Vector3.zero;
            Debug.Log(kitchenObjectTransform.GetComponent<KitchenObject>().GetKitchenObjectSo().objectName);
            KitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        }
        
    }
}
