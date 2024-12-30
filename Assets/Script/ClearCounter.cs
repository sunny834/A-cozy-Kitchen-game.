using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSo kitchenObjectSo;

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
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //player is holding a plate
                   // PlateKitchenObject plateKitchenObject = player.GetKitchenObject() as PlateKitchenObject;
                    if(plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSo()))
                    GetKitchenObject().DestroySelf();

                }
                else
                {
                    //player is carrying something not plate
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        //Counter is holding a plate
                       if( plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSo()))
                       {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }

                }
            }

            else
            {
                //player is not carrying something
                GetKitchenObject().SetKitchenObjectParent(player);
            }

        }
       

    }
   
}
