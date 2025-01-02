using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler <OnIngredientEventArgs> OnIngredient;
    public class OnIngredientEventArgs : EventArgs
    {
        public KitchenObjectSo KitchenObjectSo;
    }

    [SerializeField] private List<KitchenObjectSo> ValidKitchenOBject;
    private List<KitchenObjectSo> KitchenObjectsList;
    private void Awake()
    {
        KitchenObjectsList = new List<KitchenObjectSo>();
    }
    public bool TryAddIngredient(KitchenObjectSo kitchenObjectSo)
    {
        if (!ValidKitchenOBject.Contains(kitchenObjectSo))
        {
            return false;
        }
        if (KitchenObjectsList.Contains(kitchenObjectSo)) return false;
        else
        {
            KitchenObjectsList.Add(kitchenObjectSo);
            OnIngredient?.Invoke(this, new OnIngredientEventArgs
            {
                KitchenObjectSo = kitchenObjectSo
            });
            return true;
        }

    }

    public List<KitchenObjectSo> GetKitchenObjectSoList()
    {
        return KitchenObjectsList;
    }
}
