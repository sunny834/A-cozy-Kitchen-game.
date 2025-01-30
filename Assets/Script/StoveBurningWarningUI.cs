using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurningWarningUI : MonoBehaviour
{
    [SerializeField] private StoveCounter StoveCounter;

    private void Start()
    {
        StoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
        hide();
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedArgs e)
    {
        float burnShowAmount = 0.5f;
        bool show= StoveCounter.isFried() && e.ProgressNormalized >= burnShowAmount;
        if (show)
        {
            Show();
        }
        else
            hide();

    }

    private void Show()
    {
        gameObject .SetActive(true);
    }
    private void hide()
    {
        gameObject.SetActive(false);
    }
}
