using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader 
{
    private static scene targetScene;

    public enum scene{
        MainMenu,
        Kitchen,
        LoadingScene,
    }

    public static void Load(scene targetScene)
    {
        Loader.targetScene = targetScene;
        SceneManager.LoadScene(scene.LoadingScene.ToString());
    }
       
    public static void Callback()
    {
        SceneManager.LoadScene(targetScene.ToString());
    }
}


