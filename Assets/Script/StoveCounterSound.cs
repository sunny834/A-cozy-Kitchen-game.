using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private StoveCounter StoveCounter;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        StoveCounter.OnStateChanged += OnStateChanged;
    }

    private void OnStateChanged(object sender, StoveCounter.OnStateChangeEventArgs e)
    {
        bool playsound= e.state==StoveCounter.State.Fried || e.state==StoveCounter.State.Frying;
        if (playsound)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }
}
