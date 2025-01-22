using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{
    public static KitchenGameManager Instance {  get; private set; }
    private enum State
    {
        WaitingToSatrt,
        CountdownToStart,
        GamePlaying,
        GameOver,

    }
    private State state;
    private float WaitingToStartTimer=1f; 
    private float CountdownToStartTimer=3f;
    private float GamePlayingTimer=10f;
    

    private void Awake()
    {
        Instance = this;
        state = State.WaitingToSatrt;
    }
    private void Update()
    {
        switch (state)
        {
            case State.WaitingToSatrt:
                WaitingToStartTimer -= Time.deltaTime;
                if (WaitingToStartTimer < 0f)
                {
                    state=State.CountdownToStart;
                }
                break;
            case State.CountdownToStart:
               CountdownToStartTimer -= Time.deltaTime;
                if (CountdownToStartTimer< 0f)
                {
                    state = State.GamePlaying;
                }
                break;
            case State.GamePlaying:
                GamePlayingTimer -= Time.deltaTime;
                if (GamePlayingTimer < 0f)
                {
                    state = State.GameOver;
                }
                break;
            case State.GameOver:
                
                break;
        }
        Debug.Log(state);
    }
    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }
}
