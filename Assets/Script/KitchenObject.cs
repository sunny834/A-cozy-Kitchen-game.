using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSo KitchenObjectSo;
    private ClearCounter clearCounter;
    public KitchenObjectSo GetKitchenObjectSo() { return KitchenObjectSo; }

    public void SetClearCounter(ClearCounter clearCounter)
    {
        this.clearCounter = clearCounter;
        transform.parent=clearCounter.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;

    }
    public ClearCounter GetClearCounter() { return clearCounter; }
}
