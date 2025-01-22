using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{
    public static KitchenGameManager Instance {  get; private set; }
    public event EventHandler OnSetChange;
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
    private float GamePlayingTimer=60f;
    

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
                    OnSetChange?.Invoke(this, new EventArgs());
                }
                break;
            case State.CountdownToStart:
               CountdownToStartTimer -= Time.deltaTime;
                if (CountdownToStartTimer< 0f)
                {
                    state = State.GamePlaying;
                    OnSetChange?.Invoke(this, new EventArgs());
                }
                break;
            case State.GamePlaying:
                GamePlayingTimer -= Time.deltaTime;
                if (GamePlayingTimer < 0f)
                {
                    state = State.GameOver;
                    OnSetChange?.Invoke(this, new EventArgs());
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

    public bool IsStarttoCountdownActive()
    {
        return state == State.CountdownToStart;
            
    }
    public float GetCountdownToSatrtTimer()
    {
        return CountdownToStartTimer;
    }
    public bool IsGameOver()
    {
        return state == State.GameOver;
    }
}
