using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI CountDownText;
    private void Start()
    {
       
        KitchenGameManager.Instance.OnSetChange += OnSetChange;
        hide();
    }

    private void OnSetChange(object sender, System.EventArgs e)
    {
       if(KitchenGameManager.Instance.IsStarttoCountdownActive())
        {
            show();
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
    private void Update()
    {
        CountDownText.text=MathF.Ceiling(KitchenGameManager.Instance.GetCountdownToSatrtTimer()).ToString();
    }
}
