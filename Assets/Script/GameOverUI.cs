using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameOverUI : MonoBehaviour 
{ 
    [SerializeField] private TextMeshProUGUI RecipeDeliveredText;
    private void Start()
    {

        KitchenGameManager.Instance.OnSetChange += OnSetChange;
        hide();
    }

    private void OnSetChange(object sender, System.EventArgs e)
    {
        if (KitchenGameManager.Instance.IsGameOver())
        {
            show();
            RecipeDeliveredText.text = DeliveryManager.Instance.GetSuccessfulRecipeAmount().ToString();
        }
        else
            hide();
    }

    private void show()
    {
        gameObject.SetActive(true);
    }
    private void hide()
    {
        gameObject.SetActive(false);
    }
    
}
