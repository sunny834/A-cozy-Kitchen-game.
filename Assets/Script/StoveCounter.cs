using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter
{
    [SerializeField] private FryingRecipeSo[] fryingRecipeSoArray;

    private float FriedTimer;

    public override void Interact(Player player)
    {
        if (!HaskitchenObject())
        { //there is no kitchen object
            if (player.HaskitchenObject())
            {
                //player is carrying something
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSo()))
                {
                    //player is carrying something that can be fried
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                   
                }
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
    private KitchenObjectSo GetOutputForInput(KitchenObjectSo inputKitchenObjectSo)
    {
        FryingRecipeSo FryingRecipeSo = GetFryingRecipeWithInput(inputKitchenObjectSo);
        if (FryingRecipeSo != null)
        {
            return FryingRecipeSo.output;
        }
        else
        {
            return null;
        }

    }

    private bool HasRecipeWithInput(KitchenObjectSo inputKitchenObjectSo)
    {
        FryingRecipeSo FryingRecipeSo = GetFryingRecipeWithInput(inputKitchenObjectSo);
        return FryingRecipeSo != null;

    }

    private FryingRecipeSo GetFryingRecipeWithInput(KitchenObjectSo inputkitchenObjectSo)
    {
        foreach (FryingRecipeSo fryingRecipeSo in fryingRecipeSoArray)
        {
            if (fryingRecipeSo.input == inputkitchenObjectSo)
            {
                return fryingRecipeSo;
            }
        }
        return null;

    }

}
