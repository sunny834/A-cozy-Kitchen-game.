using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private StoveCounter StoveCounter;
    private AudioSource audioSource;
    private float WarningSoundTimer;
    private bool PlayWarningSound;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        StoveCounter.OnStateChanged += OnStateChanged;
        StoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
        
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedArgs e)
    {
        float burnShowAmount = 0.5f;
        PlayWarningSound = StoveCounter.isFried() && e.ProgressNormalized >= burnShowAmount;
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
    private void Update()
    {
        if (PlayWarningSound)
        {
            WarningSoundTimer -= Time.deltaTime;
            if (WarningSoundTimer <= 0)
            {
                float warningSoundTimerMax = .2f;
                WarningSoundTimer = warningSoundTimerMax;

                SoundManger.Instance.PlayWarningsound(StoveCounter.transform.position);
            }
        }
    }
}
