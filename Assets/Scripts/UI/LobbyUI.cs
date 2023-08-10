using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button createLobbyButton;
    [SerializeField] private Button quickJoinButton;
    [SerializeField] private Button joinWithCodeButton;

    [SerializeField] private TMP_InputField codeInputField;
    [SerializeField] private TMP_InputField playerNameInputField;

    [SerializeField] private CreateLobbyUI createLobbyUI;

    [SerializeField] private Transform lobbyListContainer;
    [SerializeField] private Transform lobbyListTemplate;


    private void Awake()
    {
        this.mainMenuButton.onClick.AddListener(() =>
        {
            KitchenGameLobby.Instance.LeaveLobby();
            Loader.Load(Loader.Scene.MainMenuScene);
        });

        this.createLobbyButton.onClick.AddListener(() =>
        {
            createLobbyUI.Show();
        });

        this.quickJoinButton.onClick.AddListener(() =>
        {
            KitchenGameLobby.Instance.QuickJoin();
        });

        this.joinWithCodeButton.onClick.AddListener(() =>
        {
            KitchenGameLobby.Instance.JoinLobbyWithCode(codeInputField.text);
        });

        this.lobbyListTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        this.playerNameInputField.text = KitchenGameMultiplayer.Instance.GetPlayerName();
        this.playerNameInputField.onValueChanged.AddListener((string newText) =>
        {
            KitchenGameMultiplayer.Instance.SetPlayerName(newText);
        });

        KitchenGameLobby.Instance.OnLobbyListChanged += KitchenGameLobby_OnLobbyListChanged;
        UpdateLobbyList(new List<Lobby>());
    }

    private void KitchenGameLobby_OnLobbyListChanged(object sender, KitchenGameLobby.OnLobbyListChangedEventArgs e)
    {
        UpdateLobbyList(e.lobbyList);
    }

    private void UpdateLobbyList(List<Lobby> lobbyList)
    {
        foreach (Transform child in lobbyListContainer)
        {
            if (child == lobbyListTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (Lobby lobby in lobbyList)
        {
            Transform lobbyTransform = Instantiate(lobbyListTemplate, lobbyListContainer);
            lobbyTransform.GetComponent<LobbyListSingleUI>().SetLobby(lobby);
            lobbyTransform.gameObject.SetActive(true);

        }
    }
}
