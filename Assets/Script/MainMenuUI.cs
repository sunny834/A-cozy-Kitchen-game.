using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] Button Play;
    [SerializeField] Button Quit;

    private void Awake()
    {
        Play.onClick.AddListener(() =>
        {
            Loader.Load(Loader.scene.Kitchen);
        });
        Quit.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
   
    public void quit()
    {

    }
}
