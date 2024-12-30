using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlateCompleteVisuals : MonoBehaviour
{
    [Serializable]
    public struct kitchenObjectSo_GameObject
    {
        public KitchenObjectSo KitchenObjectSo;
        public GameObject GameObject;
    }
    [SerializeField] private PlateKitchenObject platekitchenObject;
    [SerializeField] private List<kitchenObjectSo_GameObject> KitchenObjectSo_GameObjectList ; 
    private void Start()
    {
        platekitchenObject.OnIngredient += PlatekitchenObject_OnIngredient;
        foreach (kitchenObjectSo_GameObject kitchenObjectSo_GameObject in KitchenObjectSo_GameObjectList)
        {
            kitchenObjectSo_GameObject.GameObject.SetActive(false);
        }
    }

    private void PlatekitchenObject_OnIngredient(object sender, PlateKitchenObject.OnIngredientEventArgs e)
    {
        foreach (kitchenObjectSo_GameObject kitchenObjectSo_GameObject in KitchenObjectSo_GameObjectList)
        {
            if (kitchenObjectSo_GameObject.KitchenObjectSo == e.KitchenObjectSo)
            {
                kitchenObjectSo_GameObject.GameObject.SetActive(true);
            }
        }
    }
}
