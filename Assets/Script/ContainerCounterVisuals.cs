using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisuals : MonoBehaviour
{
    private const string OPEN_CLOSE = "OpenClose";
    private Animator animator;
    [SerializeField] private ContainerCounter ContainerCounter;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        ContainerCounter.OnPlayerGrabbedObject += ContainerCounter_OnPlayergrabbedObject;
    }
    private void ContainerCounter_OnPlayergrabbedObject(object sender,System.EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
