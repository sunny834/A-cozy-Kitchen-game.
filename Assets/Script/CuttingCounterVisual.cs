using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    private const string CUT = "Cut";
    private Animator animator;
    [SerializeField] private CuttingCounter CuttingCounter;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        CuttingCounter.OnCut += CuttingCounter_OnCut;
    }
    private void CuttingCounter_OnCut(object sender,System.EventArgs e)
    {
        animator.SetTrigger(CUT);
    }
}
