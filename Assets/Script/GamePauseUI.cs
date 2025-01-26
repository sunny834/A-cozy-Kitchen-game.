using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    public static GamePauseUI instance;
    [SerializeField] private Button ResumeButton;
    [SerializeField] private Button MainMenuButton; 
    [SerializeField] private Button OptinsButton;

    private void Awake()
    {
        instance = this;
        ResumeButton.onClick.AddListener(() =>
        {
            KitchenGameManager.Instance.TogglePauseGame();
        });
        MainMenuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.scene.MainMenu); 
        });
        OptinsButton.onClick.AddListener(() =>
        {
            hide();
            OptionUI.Instance.show(show);
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
        ResumeButton.Select();
    }
    private void hide() 
    {
        gameObject.SetActive(false);
    }
}
