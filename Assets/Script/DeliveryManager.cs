using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawn; 
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;

    public static DeliveryManager Instance{ get; private set; }
    [SerializeField] private RecipeListSo RecipeListSo;                         
    private List<RecipeSo> WaitingRecipeSoList;
    private float SpawnRecipeTimer;
    private float SpawnTimerMax=4f;
    private int WaitingRecipeMax = 5;

    private void Awake()
    {

        Instance = this;
        WaitingRecipeSoList = new List<RecipeSo>();
    }

    private void Update()
    {
        SpawnRecipeTimer -= Time.deltaTime;
        if(SpawnRecipeTimer <=0f )
        {
            SpawnRecipeTimer=SpawnTimerMax;
            if (WaitingRecipeSoList.Count < WaitingRecipeMax)
            {

                RecipeSo WaitingRecipeSo = RecipeListSo.RecipeSoList[UnityEngine.Random.Range(0, RecipeListSo.RecipeSoList.Count)];
               // Debug.Log(WaitingRecipeSo.RecipeName);
                WaitingRecipeSoList.Add(WaitingRecipeSo);

                OnRecipeSpawn?.Invoke(this,EventArgs.Empty);
            }
        }

    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < WaitingRecipeSoList.Count; i++)
        {
            RecipeSo WaitingRecipeSo=WaitingRecipeSoList[i];
            if(WaitingRecipeSo.KitchenObjectSo.Count==plateKitchenObject.GetKitchenObjectSoList().Count)
            {
                //Has Same number of ingredient
                bool PlateContentMatchesRecipe =true;
                foreach (KitchenObjectSo recipeKitcheObjectSo in WaitingRecipeSo.KitchenObjectSo)
                {
                    //Cycling through all the ingrident in recipe
                    bool ingridentFound = false;
                    foreach (KitchenObjectSo PlateKitchenObjectSo in plateKitchenObject.GetKitchenObjectSoList())
                    {
                        //cycling through all the ingrident in the plate
                        if (PlateKitchenObjectSo == recipeKitcheObjectSo)
                        {
                            ingridentFound=true;
                            break;
                        }

                    }
                    if (!ingridentFound)
                    {
                        //recipee not found in the plate
                        PlateContentMatchesRecipe=false;
                    }

                }

                if (PlateContentMatchesRecipe)
                {
                    //player fail to deliver correct recipe
                    Debug.Log("Correct Order");
                    WaitingRecipeSoList.RemoveAt(i);
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }

            Debug.Log("Wrong Recipe");
            OnRecipeFailed?.Invoke(this, EventArgs.Empty);
        }
    }

    public List<RecipeSo> GetWaitingRecipeSoList()
    {

    return WaitingRecipeSoList; 
    }
}
