using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] CuttingRecipeSo[] cutkitchenObjectSoArray;
    private int cutttingProgress;

    public override void Interact(Player player)
    {
        if (!HaskitchenObject())
        { //there is no kitchen object
            if (player.HaskitchenObject())
            {
                //player is carrying something
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSo()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cutttingProgress = 0;
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
    public override void InteractAlternate(Player player)
    {
        if (HaskitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSo()))
        {
            //Cut the gameobject
            cutttingProgress++;
            CuttingRecipeSo cuttingRecipeSo = GetCuttingRecipeWithInput(GetKitchenObject().GetKitchenObjectSo());
            if (cutttingProgress > cuttingRecipeSo.cuttingProgressMax)
            {
                KitchenObjectSo OutputKitchenObjectSo = GetOutputForInput(GetKitchenObject().GetKitchenObjectSo());
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(OutputKitchenObjectSo, this);
            }
        }
    }

    private KitchenObjectSo GetOutputForInput(KitchenObjectSo inputKitchenObjectSo)
    {
        CuttingRecipeSo cuttingRecipeSo = GetCuttingRecipeWithInput(inputKitchenObjectSo);
        if (cuttingRecipeSo != null)
        {
            return cuttingRecipeSo.output;
        }
        else
        {
            return null;
        }

    }

    private bool HasRecipeWithInput(KitchenObjectSo inputKitchenObjectSo)
    {
        CuttingRecipeSo cuttingRecipeSo = GetCuttingRecipeWithInput(inputKitchenObjectSo);
        return cuttingRecipeSo != null;

    }

    private CuttingRecipeSo GetCuttingRecipeWithInput(KitchenObjectSo inputkitchenObjectSo)
    {
        foreach (CuttingRecipeSo cuttingRecipeSo in cutkitchenObjectSoArray)
        {
            if (cuttingRecipeSo.input == inputkitchenObjectSo)
            {
                return cuttingRecipeSo;
            }
        }
        return null;

    }
}
