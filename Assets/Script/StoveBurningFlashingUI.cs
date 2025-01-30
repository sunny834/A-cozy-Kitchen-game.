using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurningFlashingUI : MonoBehaviour
{
    private const string IS_FLASH = "isFlash";
    [SerializeField] private StoveCounter StoveCounter;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void Start()
    {
        StoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
        _animator.SetBool(IS_FLASH, false);
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedArgs e)
    {
        float burnShowAmount = 0.5f;
        bool show = StoveCounter.isFried() && e.ProgressNormalized >= burnShowAmount;
        
        _animator.SetBool(IS_FLASH, show);

    }

   
}
