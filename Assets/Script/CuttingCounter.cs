using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] KitchenObjectSo cutkitchenObjectSo;
    public override void Interact(Player player)
    {
        if (!HaskitchenObject())
        { //there is no kitchen object
            if (player.HaskitchenObject())
            {
                //player is carrying something
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                //there is already a kitchen object here
            }
        }
        else
        { //there is kitchen object
            if (player.HaskitchenObject())
            {
                //player  is carrying a kitchen object
            }
            else
            {
                //player is not carrying something
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }

    
    }
    public override void InteractAlternate(Player player)
    {
        if (HaskitchenObject())
        {
            //Cut the gameobject
            GetKitchenObject().DestroySelf();
           KitchenObject.SpawnKitchenObject(cutkitchenObjectSo,this);
        }
    }

}
