using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryMessageUI : MonoBehaviour
{
    private const string POPUP = "Popup";

    [SerializeField] private Image BackgroundImage; 
    [SerializeField] private Image Iconimage;
    [SerializeField] private TextMeshProUGUI Message;
    [SerializeField] private Color Success;
    [SerializeField] private Color Failure;
    [SerializeField] private Sprite SuccessSprite;
    [SerializeField] private Sprite FailureSprite;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += OnRecipeFailed;
        gameObject.SetActive(false);
    }

    private void OnRecipeFailed(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
        _animator.SetTrigger(POPUP);
        BackgroundImage.color=Failure;
        Iconimage.sprite=FailureSprite;
        Message.text = "DELIVERU\nFAILED";

    }

    private void OnRecipeSuccess(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true) ;
        _animator.SetTrigger(POPUP);
        BackgroundImage.color = Success;
        Iconimage.sprite = SuccessSprite;
        Message.text = "DELIVERU\nSUCCESS";
    }
}
