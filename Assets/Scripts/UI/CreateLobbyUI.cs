using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateLobbyUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField lobbyNameInputField;
    [SerializeField] private Button createPrivateButton;
    [SerializeField] private Button createPublicButton;
    [SerializeField] private Button closeButton;

    private Action OnReselectButton;

    private void Awake()
    {
        this.createPrivateButton.onClick.AddListener(() =>
        {
            KitchenGameLobby.Instance.CreateLobby(lobbyNameInputField.text, true);
        });

        this.createPublicButton.onClick.AddListener(() =>
        {
            KitchenGameLobby.Instance.CreateLobby(lobbyNameInputField.text, false);
        });

        this.closeButton.onClick.AddListener(() =>
        {
            Hide();
            OnReselectButton();
        });
    }

    private void Start()
    {
        Hide();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show(Action OnReselectButton)
    {
        gameObject.SetActive(true);
        createPublicButton.Select();
        this.OnReselectButton = OnReselectButton;
    }

}
