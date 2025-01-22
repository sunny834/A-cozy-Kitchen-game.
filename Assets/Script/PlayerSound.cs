using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
   
    private Player Player;
    private float TimeMax = .1f;
    private float FootStepTimer;

    private void Start()
    {
        Player = GetComponent<Player>();
    }
    private void Update()
    {
        FootStepTimer -= Time.deltaTime;
        if( FootStepTimer <0f )
        {
            FootStepTimer=TimeMax;
            if (Player.IsWalking())
            {
                float volume = 1f;
                SoundManger.Instance.PlayFootSound(Player.transform.position, volume);
            }
        }

    }
}
