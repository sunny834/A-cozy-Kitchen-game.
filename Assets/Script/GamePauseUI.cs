using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button ResumeButton;
    [SerializeField] private Button MainMenuButton;

    private void Awake()
    {
        ResumeButton.onClick.AddListener(() =>
        {
            KitchenGameManager.Instance.TogglePauseGame();
        });
        MainMenuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.scene.MainMenu); 
        });
    }
    private void Start()
    {
        KitchenGameManager.Instance.OnPause += GameOnPause;
        KitchenGameManager.Instance.OnResume += GameOnResume;
        hide();
    }

    private void GameOnResume(object sender, System.EventArgs e)
    {
        hide();
    }

    private void GameOnPause(object sender, System.EventArgs e)
    {
       show();
    }
    private void show()
    {
        gameObject.SetActive(true);
    }
    private void hide() 
    {
        gameObject.SetActive(false);
    }
}
