using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : BaseCounter
{
    public event EventHandler OnPlateSpawn;
    public event EventHandler OnPlateRemove;

    private float SpawnPlateTimer;
    private float SpawnTimerMax=4f;
    [SerializeField] KitchenObjectSo PlateObjectSo;
    private int SpawnPlateAmountMax=4;
    private int SpawnPlateAmount;

    private void Update()
    {
        SpawnPlateTimer += Time.deltaTime;
        if(SpawnPlateTimer >SpawnTimerMax )
        {
            SpawnPlateTimer = 0f;
            if (SpawnPlateAmount < SpawnPlateAmountMax)
            {
                SpawnPlateAmount++;
                OnPlateSpawn?.Invoke( this,EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HaskitchenObject())
        {
            //player is empty handed
            if(SpawnPlateAmount>0)
            {
                //there is atleast one plate is there
                SpawnPlateAmount--;
                KitchenObject.SpawnKitchenObject(PlateObjectSo,player);
                OnPlateRemove?.Invoke( this,EventArgs.Empty);
            }
        }
    }
}
