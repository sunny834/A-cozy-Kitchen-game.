using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManger : MonoBehaviour
{
    [SerializeField] private AudioClipRefSo audioClipRefSo;
    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManger_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManger_OnRecipeFailed;
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
    private void playSound(AudioClip audioclip,Vector3 position,float volume=1f)
    {
        AudioSource.PlayClipAtPoint(audioclip, position, volume);
    }
   
}
