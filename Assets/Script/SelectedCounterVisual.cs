using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] public GameObject Visuals;
    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender,Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.SelectedCounter ==clearCounter)
        {
            show();
        }
        else
        {
            hide();
        }
    }
    private void show()
    {
        Visuals.SetActive(true);
    }
    private void hide()
    {
        Visuals.SetActive(false);
    }
}
