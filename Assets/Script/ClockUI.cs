using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockUI : MonoBehaviour
{
    [SerializeField] private Image TimerImage;

    private void Update()
    {
       TimerImage.fillAmount= KitchenGameManager.Instance.GetPlayingTimerNormalized();
    }
}
