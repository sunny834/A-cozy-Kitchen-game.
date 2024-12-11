using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    private Animator animator;
    private const string IS_WALKING = "IsWalking";
    [SerializeField] private Player player;
    private void Awake()
    {
        animator = GetComponent<Animator>();    
    }

    private void Update()
    {
        animator.SetBool(IS_WALKING,player.IsWalking());
    }
}
