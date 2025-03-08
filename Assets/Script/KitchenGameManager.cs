using System;
using System.Collections;
using System.Collections.Generic;
//using System.Drawing.Printing;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{
    public static KitchenGameManager Instance {  get; private set; }
    public event EventHandler OnSetChange;
    public event EventHandler OnPause;
    public event EventHandler OnResume;
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
    private float GamePlayingTimer; 
    private float GamePlayingTimerMax=1000f;
    private bool IsGamePause = false;
    

    private void Awake()
    {
        Instance = this;
        state = State.WaitingToSatrt;
    }

    private void Start()
    {
        NewInputSystem.Instance.OnPauseAction += OnPauseAction;
    }

    private void OnPauseAction(object sender, EventArgs e)
    {
        TogglePauseGame();
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
                    GamePlayingTimer =GamePlayingTimerMax;
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
        //Debug.Log(state);
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
    public float GetPlayingTimerNormalized()
    {
        return 1-( GamePlayingTimer/GamePlayingTimerMax);
    }
    public void TogglePauseGame()
    {
        IsGamePause = !IsGamePause;

        if (IsGamePause)
        {

            Time.timeScale = 0f;
            OnPause?.Invoke(this,EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            OnResume?.Invoke(this, EventArgs.Empty);
        }
    }
}
