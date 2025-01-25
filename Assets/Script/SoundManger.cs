using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManger : MonoBehaviour
{
    private const string PLayer_Pref_Sounds_Effects = "Sound";
    public static SoundManger Instance { get; private set; }
    [SerializeField] private AudioClipRefSo audioClipRefSo;

    private float volume = 1f;
    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManger_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManger_OnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickSomething += OnPickSomething;
        BaseCounter.OnDrop += OnDrop;
        TrashCounter.OnTrashDrop += OnTrashDrop;
    }

    private void Awake()
    {
        Instance = this;
        volume=PlayerPrefs.GetFloat(PLayer_Pref_Sounds_Effects,1f);

    }

    private void OnTrashDrop(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        playSound(audioClipRefSo.trash, trashCounter.transform.position);
    }

    private void OnDrop(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        playSound(audioClipRefSo.objectDrop,baseCounter.transform.position);

    }

    private void OnPickSomething(object sender, System.EventArgs e)
    {
        playSound(audioClipRefSo.objectPick, Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter= sender as CuttingCounter;
        playSound(audioClipRefSo.Chop, cuttingCounter.transform.position);
    }

    private void DeliveryManger_OnRecipeFailed(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        playSound(audioClipRefSo.DeliveryFail,deliveryCounter.transform.position);
    }

    private void DeliveryManger_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        playSound(audioClipRefSo.DeliverySuccess, deliveryCounter.transform.position);
    }

    private void playSound(AudioClip[] audioclipArray, Vector3 position, float volume = 1f)
    {
        playSound(audioclipArray[Random.Range(0, audioclipArray.Length)], position, volume);

    }
    private void playSound(AudioClip audioclip,Vector3 position,float volumeMultiplayer=1f)
    {
        AudioSource.PlayClipAtPoint(audioclip, position, volumeMultiplayer*volume);
    }
    public void PlayFootSound(Vector3 position,float volume)
    {
        playSound(audioClipRefSo.FootSteps, position,volume);
    }
    public void ChangeVolume() {
        {
            volume += .1f;
            if (volume > 1f)
            {
                volume = 0f;
            }
            PlayerPrefs.SetFloat(PLayer_Pref_Sounds_Effects, volume);
            PlayerPrefs.Save();
        }
    }
    public float GetVolume() {
        return volume;
    }
   
}
