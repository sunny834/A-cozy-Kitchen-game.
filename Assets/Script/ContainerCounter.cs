using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject; 
    [SerializeField] private KitchenObjectSo kitchenObjectSo; 
    public override void Interact(Player player)
    {
        if (!player.HaskitchenObject())
        {
            //Player has not carrying anything 
            Debug.Log("Interact");
            KitchenObject.SpawnKitchenObject(kitchenObjectSo, player);
           
            OnPlayerGrabbedObject.Invoke(this,EventArgs.Empty);

        }
      
    }
   
}
