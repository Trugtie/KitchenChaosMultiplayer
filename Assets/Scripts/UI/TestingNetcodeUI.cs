using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class TestingNetcodeUI : MonoBehaviour
{
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;

    private void Awake()
    {
        hostButton.onClick.AddListener(() =>
        {
            Debug.Log("HOST");
            KitchenGameNetworkObject.Instance.StartHost();
            Hide();
        });
        clientButton.onClick.AddListener(() =>
        {
            Debug.Log("CLIENT");
            KitchenGameNetworkObject.Instance.StartClient();
            Hide();
        });
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
