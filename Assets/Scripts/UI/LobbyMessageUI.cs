using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Netcode;

public class LobbyMessageUI : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI messageText;

    private void Awake()
    {
        closeButton.onClick.AddListener(Hide);
    }

    private void Start()
    {
        KitchenGameMultiplayer.Instance.OnFailedToJoinGame += KitchenGameMultiplayer_OnFailedToJoinGame;
        KitchenGameLobby.Instance.OnCreateFailed += KitchenGameLobby_OnCreateFailed;
        KitchenGameLobby.Instance.OnCreateStarted += KitchenGameLobby_OnCreateStarted;
        KitchenGameLobby.Instance.OnJoinedFail += KitchenGameLobby_OnJoinedFail;
        KitchenGameLobby.Instance.OnJoinedStarted += KitchenGameLobby_OnJoinedStarted;
        KitchenGameLobby.Instance.OnQuickJoinedFail += KitchenGameLobby_OnQuickJoinedFail;

        Hide();
    }

    private void KitchenGameLobby_OnQuickJoinedFail(object sender, System.EventArgs e)
    {
        ShowMessage("Could not find a Lobby to Quick Join!");
    }

    private void KitchenGameLobby_OnJoinedStarted(object sender, System.EventArgs e)
    {
        ShowMessage("Joining Lobby...");
    }

    private void KitchenGameLobby_OnJoinedFail(object sender, System.EventArgs e)
    {
        ShowMessage("Failed to join Lobby");
    }

    private void KitchenGameLobby_OnCreateStarted(object sender, System.EventArgs e)
    {
        ShowMessage("Creating Lobby...");
    }

    private void KitchenGameLobby_OnCreateFailed(object sender, System.EventArgs e)
    {
        ShowMessage("Failed to create Lobby!");
    }

    private void KitchenGameMultiplayer_OnFailedToJoinGame(object sender, System.EventArgs e)
    {
        if (NetworkManager.Singleton.DisconnectReason == "")
        {
            ShowMessage("Failed to connenct");
        }
        else
        {
            ShowMessage(NetworkManager.Singleton.DisconnectReason);
        }
    }

    private void ShowMessage(string message)
    {
        Show();
        this.messageText.text = message;
    }

    private void Show()
    {
        gameObject.SetActive(true);
        closeButton.Select();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        KitchenGameMultiplayer.Instance.OnFailedToJoinGame -= KitchenGameMultiplayer_OnFailedToJoinGame;
        KitchenGameLobby.Instance.OnCreateFailed -= KitchenGameLobby_OnCreateFailed;
        KitchenGameLobby.Instance.OnCreateStarted -= KitchenGameLobby_OnCreateStarted;
        KitchenGameLobby.Instance.OnJoinedFail -= KitchenGameLobby_OnJoinedFail;
        KitchenGameLobby.Instance.OnJoinedStarted -= KitchenGameLobby_OnJoinedStarted;
        KitchenGameLobby.Instance.OnQuickJoinedFail -= KitchenGameLobby_OnQuickJoinedFail;
    }
}
