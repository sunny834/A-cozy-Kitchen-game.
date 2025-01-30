using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI CountDownText;

    private const string NUMBER_POPUP = "NumberPopup";

    private Animator animator;
    private int previousCountdownNumber;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
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
        int countdownNumber = (int)MathF.Ceiling(KitchenGameManager.Instance.GetCountdownToSatrtTimer());
        CountDownText.text=countdownNumber.ToString();
        if (previousCountdownNumber != countdownNumber)
        {
            previousCountdownNumber = countdownNumber;
            animator.SetTrigger(NUMBER_POPUP);
            SoundManger.Instance.PlayCountdownSound();
        }
    }
}
