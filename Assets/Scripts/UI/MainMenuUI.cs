using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playMultiplayerButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button playSinglePlayerButton;
    private void Awake()
    {
        playMultiplayerButton.onClick.AddListener(() =>
        {
            KitchenGameMultiplayer.isPlayMultiplayer = true;
            Loader.Load(Loader.Scene.LobbyScene);
        });

        playSinglePlayerButton.onClick.AddListener(() =>
        {
            KitchenGameMultiplayer.isPlayMultiplayer = false;
            Loader.Load(Loader.Scene.LobbyScene);
        });

        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
        Time.timeScale = 1f;
    }
}
