using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter,IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedArgs> OnProgressChanged;
    public event EventHandler<OnStateChangeEventArgs>  OnStateChanged;
    public class OnStateChangeEventArgs : EventArgs
    {
       public State state;
    }

    [SerializeField] private FryingRecipeSo[] fryingRecipeSoArray;
    [SerializeField] private BurningRecipeSo[] burningRecipeSoArray;

    private float FriedTimer;
    private FryingRecipeSo fryingRecipeSo;
    private float BurningTimer;
    private BurningRecipeSo BurningRecipeSo;
    public enum State{
        Idle,
        Frying,
        Fried,
        Burned,

    }
    private State state;

    private void Start()
    {
        state = State.Idle;
    }



    private void Update()
    {
        if (HaskitchenObject())
        {
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedArgs
                    {
                        ProgressNormalized = FriedTimer / fryingRecipeSo.FryingTime
                    });

                    FriedTimer += Time.deltaTime;

                    if (FriedTimer > fryingRecipeSo.FryingTime)
                    {
                        //Fried                      
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(fryingRecipeSo.output, this);
                        Debug.Log("Fried!");
                        state = State.Fried;
                        BurningTimer = 0;
                        BurningRecipeSo = GetBurningRecipeWithInput(GetKitchenObject().GetKitchenObjectSo());
                        OnStateChanged?.Invoke(this, new OnStateChangeEventArgs{
                            state = state

                        });
                    }
                   // Debug.Log(FriedTimer);
                    break;
                case State.Fried:
                    BurningTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedArgs
                    {
                        ProgressNormalized = BurningTimer / BurningRecipeSo.burningTimeMax
                    });

                    if (BurningTimer > BurningRecipeSo.burningTimeMax)
                    {
                        //Fried                      
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(BurningRecipeSo.output, this);
                        Debug.Log("Burned!");
                        state = State.Burned;
                        OnStateChanged?.Invoke(this, new OnStateChangeEventArgs
                        {
                            state = state

                        });
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedArgs
                        {
                            ProgressNormalized = 0f
                        });
                    }
                    //  Debug.Log(FriedTimer);
                    break;
                case State.Burned:
                    break;
            }
            Debug.Log(state);


        }
        
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
                    //player is carrying something that can be fried
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    fryingRecipeSo = GetFryingRecipeWithInput(GetKitchenObject().GetKitchenObjectSo());
                    state = State.Frying;
                    FriedTimer = 0;
                    OnStateChanged?.Invoke(this, new OnStateChangeEventArgs
                    {
                        state = state

                    });
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedArgs
                    {
                        ProgressNormalized = FriedTimer / fryingRecipeSo.FryingTime
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
            }
            else
            {
                //player is not carrying something
                GetKitchenObject().SetKitchenObjectParent(player);
                state = State.Idle;
                OnStateChanged?.Invoke(this, new OnStateChangeEventArgs
                {
                    state = state

                });
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedArgs
                {
                    ProgressNormalized = 0f
                });
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
    private BurningRecipeSo GetBurningRecipeWithInput(KitchenObjectSo inputkitchenObjectSo)
    {
        foreach (BurningRecipeSo burningRecipeSo in burningRecipeSoArray)
        {
            if (burningRecipeSo.input == inputkitchenObjectSo)
            {
                return burningRecipeSo;
            }
        }
        return null;

    }

}
