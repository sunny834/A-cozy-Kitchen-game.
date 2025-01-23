using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CuttingCounter : BaseCounter,IHasProgress
{
    public static event EventHandler OnAnyCut;
    public event EventHandler <IHasProgress.OnProgressChangedArgs>OnProgressChanged;
   
    public event EventHandler OnCut;
    [SerializeField] CuttingRecipeSo[] cutkitchenObjectSoArray;
    private int cutttingProgress;

    new public static void ResetStaticData()
    {
        OnAnyCut = null;
    }
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
                    CuttingRecipeSo cuttingRecipeSo = GetCuttingRecipeWithInput(GetKitchenObject().GetKitchenObjectSo());
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedArgs
                    {
                        ProgressNormalized = (float)cutttingProgress/cuttingRecipeSo.cuttingProgressMax
                    });
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
                //player  is carrying a kitchen object
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject1))
                {
                    //player is holding a plate
                    PlateKitchenObject plateKitchenObject = player.GetKitchenObject() as PlateKitchenObject;
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSo()))
                        GetKitchenObject().DestroySelf();

                }
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
            OnCut?.Invoke(this, EventArgs.Empty); OnAnyCut?.Invoke(this, EventArgs.Empty);
            CuttingRecipeSo cuttingRecipeSo = GetCuttingRecipeWithInput(GetKitchenObject().GetKitchenObjectSo());
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedArgs
            {
                ProgressNormalized = (float)cutttingProgress / cuttingRecipeSo.cuttingProgressMax
            });
           
            if (cutttingProgress >= cuttingRecipeSo.cuttingProgressMax)
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
