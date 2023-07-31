using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }

    [SerializeField] private Button sfxButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAltButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button interactButtonPad;
    [SerializeField] private Button interactAltButtonPad;
    [SerializeField] private Button pauseButtonPad;
    [SerializeField] private TextMeshProUGUI sfxText;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAltText;
    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private TextMeshProUGUI interactPadText;
    [SerializeField] private TextMeshProUGUI interactAltPadText;
    [SerializeField] private TextMeshProUGUI pausePadText;
    [SerializeField] private Transform pressKeyToRebind;

    private Action onCloseButtonAction;


    private void Awake()
    {
        Instance = this;

        this.sfxButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();

            UpdateVisual();
        });

        this.musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        this.closeButton.onClick.AddListener(() =>
        {
            this.Hide();
            onCloseButtonAction();
        });
        this.moveUpButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Move_Up);
        });
        this.moveDownButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Move_Down);
        });
        this.moveLeftButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Move_Left);
        });
        this.moveRightButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Move_Right);
        });
        this.interactButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Interact);
        });
        this.interactAltButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.InteractAlt);
        });
        this.pauseButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Pause);
        });
        this.interactButtonPad.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.InteractPad);
        });
        this.interactAltButtonPad.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.InteractAltPad);
        });
        this.pauseButtonPad.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.PausePad);
        });
    }

    private void Start()
    {
        GameManager.Instance.OnGameUnPaused += GameManager_OnGameUnPaused;
        UpdateVisual();
        this.Hide();
        this.HidePressKeyToRebind();
    }

    private void GameManager_OnGameUnPaused(object sender, System.EventArgs e)
    {
        this.Hide();
    }

    private void UpdateVisual()
    {
        sfxText.text = $"Sound Effect: {Mathf.Round(10f * SoundManager.Instance.GetVolume()).ToString()}";
        musicText.text = $"Music: {Mathf.Round(10f * MusicManager.Instance.GetVolume()).ToString()}";
        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        interactAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlt);
        pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
        interactPadText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractPad);
        interactAltPadText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAltPad);
        pausePadText.text = GameInput.Instance.GetBindingText(GameInput.Binding.PausePad);
    }

    public void Show(Action onCloseButtonAction)
    {
        this.onCloseButtonAction = onCloseButtonAction;
        gameObject.SetActive(true);
        sfxButton.Select();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ShowPressKeyToRebind()
    {
        this.pressKeyToRebind.gameObject.SetActive(true);
    }

    private void HidePressKeyToRebind()
    {
        this.pressKeyToRebind.gameObject.SetActive(false);
    }

    private void RebindBinding(GameInput.Binding binding)
    {
        ShowPressKeyToRebind();
        GameInput.Instance.RebindingKey(binding, () =>
        {
            HidePressKeyToRebind();
            UpdateVisual();
        });
    }
}
