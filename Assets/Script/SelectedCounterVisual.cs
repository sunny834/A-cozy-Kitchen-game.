using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter BaseCounter;
    [SerializeField] public GameObject[] VisualsArray;
    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender,Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.SelectedCounter ==BaseCounter)
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
        foreach (GameObject Visuals in VisualsArray ) {
            Visuals.SetActive(true);
        }
    }
    private void hide()
    {
        foreach (GameObject Visuals in VisualsArray)
        {
            Visuals.SetActive(false);
        }
    }
}
