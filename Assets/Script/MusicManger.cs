using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManger : MonoBehaviour
{
    public const string Player_Pref_Sound_Effects = "MusicVolume";
    public static MusicManger Instance {  get; private set; } 
    private float volume=.3f;
    private AudioSource audioSource;
     
    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        volume=PlayerPrefs.GetFloat(Player_Pref_Sound_Effects, 0.3f);
        audioSource.volume = volume;
    }
    public void ChangeVolume()
    {
        {
            volume += .1f;
            if (volume > 1f)
            {
                volume = 0f;
            }
            audioSource.volume = volume;
            PlayerPrefs.SetFloat(Player_Pref_Sound_Effects, volume);
            PlayerPrefs.Save();
        }
    }
    public float GetVolume()
    {
        return volume;
    }
}
