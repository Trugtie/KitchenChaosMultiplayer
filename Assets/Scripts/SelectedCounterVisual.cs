using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualSelectedObjectArray;
    private void Start()
    {
        if (Player.LocalInstance != null)
        {
            Player.LocalInstance.OnSeletecedCounterChanged += Player_OnSeletecedCounterChanged;
        }
        else
        {
            Player.OnAnyPlayerSpawned += Player_OnAnyPlayerSpawned;
        }

    }

    private void Player_OnAnyPlayerSpawned(object sender, System.EventArgs e)
    {
        if (Player.LocalInstance != null)
        {
            Player.LocalInstance.OnSeletecedCounterChanged -= Player_OnSeletecedCounterChanged;
            Player.LocalInstance.OnSeletecedCounterChanged += Player_OnSeletecedCounterChanged;
        }
    }

    private void Player_OnSeletecedCounterChanged(object sender, Player.OnSeletecedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == baseCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        foreach (GameObject visualSelectedObject in visualSelectedObjectArray)
        {
            visualSelectedObject.SetActive(true);
        }

    }

    private void Hide()
    {
        foreach (GameObject visualSelectedObject in visualSelectedObjectArray)
        {
            visualSelectedObject.SetActive(false);
        }
    }
}
