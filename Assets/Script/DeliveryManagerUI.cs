using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform Container;
    [SerializeField] private Transform RecipeTemplate;

    private void Awake()
    {
        RecipeTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSpawn += DeliveryManager_OnRecipeSpawn;
        DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;
        UpdateVisuals();
    }

    private void DeliveryManager_OnRecipeCompleted(object sender, System.EventArgs e)
    {
        UpdateVisuals();
    }

    private void DeliveryManager_OnRecipeSpawn(object sender, System.EventArgs e)
    {
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        foreach(Transform child in Container)
        {
            if (child==RecipeTemplate)continue;
            Destroy(child.gameObject);
        }
        foreach(RecipeSo recipeSo in  DeliveryManager.Instance.GetWaitingRecipeSoList())
        {
            Transform recipeTransform = Instantiate(RecipeTemplate, Container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSo(recipeSo);
        }
    }
}
