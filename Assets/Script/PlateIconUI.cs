using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconUI : MonoBehaviour
{
    [SerializeField] PlateKitchenObject platekitchenObject;
    [SerializeField] private Transform iconTemplate;

    private void Awake()
    {
         iconTemplate.gameObject.SetActive(false);
    }
    private void Start()
    {
        platekitchenObject.OnIngredient += PlatekitchenObject_OnIngredient;
    }

    private void PlatekitchenObject_OnIngredient(object sender, PlateKitchenObject.OnIngredientEventArgs e)
    {
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        foreach(Transform child in transform)
        {
            if(child==iconTemplate) continue;
            Destroy(child.gameObject);
        }
        foreach(KitchenObjectSo kitchenObjectSo in platekitchenObject.GetKitchenObjectSoList())
        {
            Transform IconTransform= Instantiate(iconTemplate, transform);
            IconTransform.gameObject.SetActive(true);
            IconTransform.GetComponent<PlateIconSingleUI>().SetKitchenObjectSo(kitchenObjectSo);    
        }
    }
}
