using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    public static OptionUI Instance { get; private set; }
    [SerializeField] private Button SoundButton;
    [SerializeField] private Button MusicButton;
    [SerializeField] private Button CloseButton;
    [SerializeField] private Button MoveUpButton;
    [SerializeField] private Button MoveDownButton;
    [SerializeField] private Button MoveLeftButton;
    [SerializeField] private Button MoveRightButton;
    [SerializeField] private Button InteractButton;
    [SerializeField] private Button InteractAltButton;
    [SerializeField] private Button PauseButton;

    [SerializeField] private TextMeshProUGUI SoundVolume;
    [SerializeField] private TextMeshProUGUI MusicVolume;
    [SerializeField] private TextMeshProUGUI MoveUp;
    [SerializeField] private TextMeshProUGUI MoveDown;
    [SerializeField] private TextMeshProUGUI MoveLeft;
    [SerializeField] private TextMeshProUGUI MoveRight;
    [SerializeField] private TextMeshProUGUI Interact;
    [SerializeField] private TextMeshProUGUI InteractAlt;
    [SerializeField] private TextMeshProUGUI Pause;
    [SerializeField] private Transform PressKeyToRebindTransform;

    private Action OnCloseButtonAction;

    public void Awake()
    {
        Instance = this;
        SoundButton.onClick.AddListener(() =>
        {
            SoundManger.Instance.ChangeVolume();
            UpdateVisual();

        });

        MusicButton.onClick.AddListener(() =>
        {
            MusicManger.Instance.ChangeVolume();
            UpdateVisual();

        });
        CloseButton.onClick.AddListener(() =>
        {
            hide();
            OnCloseButtonAction();

        });
        MoveUpButton.onClick.AddListener(() =>
        {
            RebindBinding(NewInputSystem.Binding.Move_Up);
        });
        MoveDownButton.onClick.AddListener(() =>
        {
            RebindBinding(NewInputSystem.Binding.Move_Down);
        });
        MoveLeftButton.onClick.AddListener(() =>
        {
            RebindBinding(NewInputSystem.Binding.Move_Left);
        });
        MoveRightButton.onClick.AddListener(() =>
        {
            RebindBinding(NewInputSystem.Binding.Move_Right);
        });
        InteractButton.onClick.AddListener(() =>
        {
            RebindBinding(NewInputSystem.Binding.Interact);
        });
        InteractAltButton.onClick.AddListener(() =>
        {
            RebindBinding(NewInputSystem.Binding.InteractAlt);
        }); 
        PauseButton.onClick.AddListener(() =>
        {
            RebindBinding(NewInputSystem.Binding.Pause);
            
        }); 

    }
    private void Start()
    {
        KitchenGameManager.Instance.OnPause += OnPause;
        UpdateVisual();
        HidePressToRebindKey();
        hide();
        KitchenGameManager.Instance.OnResume += OnResume;
    }

    private void OnResume(object sender, EventArgs e)
    {
       gameObject.SetActive(false);
    }

    private void OnPause(object sender, System.EventArgs e)
    {
        hide();
    }

    private void UpdateVisual()
    {
        SoundVolume.text = "SoundEffects:" + Mathf.Round(SoundManger.Instance.GetVolume() * 10);
        MusicVolume.text = "MusicEffects:" + Mathf.Round(MusicManger.Instance.GetVolume() * 10);

        MoveUp.text = NewInputSystem.Instance.GetBindingText(NewInputSystem.Binding.Move_Up);
        MoveDown.text = NewInputSystem.Instance.GetBindingText(NewInputSystem.Binding.Move_Down);
        MoveRight.text = NewInputSystem.Instance.GetBindingText(NewInputSystem.Binding.Move_Right);
        MoveLeft.text = NewInputSystem.Instance.GetBindingText(NewInputSystem.Binding.Move_Left);
        Interact.text = NewInputSystem.Instance.GetBindingText(NewInputSystem.Binding.Interact);
        InteractAlt.text = NewInputSystem.Instance.GetBindingText(NewInputSystem.Binding.InteractAlt);
        Pause.text = NewInputSystem.Instance.GetBindingText(NewInputSystem.Binding.Pause);

    }

    public void show(Action OnCloseButtonAction)
    {
        this.OnCloseButtonAction = OnCloseButtonAction;
        gameObject.SetActive(true);
        SoundButton.Select();
    }
    public void hide()
    {
        gameObject.SetActive(false);
    }

    private void ShowPressToRebindKey()
    {
        PressKeyToRebindTransform.gameObject.SetActive(true);
    } 
    private void HidePressToRebindKey()
    {
        PressKeyToRebindTransform.gameObject.SetActive(false);
    }
    private void RebindBinding(NewInputSystem.Binding binding)
    {
        ShowPressToRebindKey();
        NewInputSystem.Instance.RebindBinding(binding, () => {
            HidePressToRebindKey();
            UpdateVisual();
            }
        );
    }
}
