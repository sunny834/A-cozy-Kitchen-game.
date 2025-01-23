using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter 
{
    public static event EventHandler OnTrashDrop;

    new public static void ResetStaticData()
    {
        OnTrashDrop = null;
    }

    public override void Interact(Player player)
    {
        if(player.HaskitchenObject())
        {
            player.GetKitchenObject().DestroySelf();
            OnTrashDrop?.Invoke(this,EventArgs.Empty);
        }
    }
}
