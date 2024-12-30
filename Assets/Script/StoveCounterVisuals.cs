using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;

public class StoveCounterVisuals : MonoBehaviour
{
    [SerializeField] GameObject StoveOnObject;
    [SerializeField] GameObject ParticleGameObject;

    [SerializeField] private StoveCounter StoveCounter;

    private void Start()
    {
        StoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender,StoveCounter.OnStateChangeEventArgs e)
    {
        bool ShowVisual =e.state == StoveCounter.State.Frying || e.state==StoveCounter.State.Fried;
        StoveOnObject.SetActive(ShowVisual);
        ParticleGameObject.SetActive(ShowVisual);
    }
}
