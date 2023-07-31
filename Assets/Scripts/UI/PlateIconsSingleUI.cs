using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateIconsSingleUI : MonoBehaviour
{
    [SerializeField] private Image iconSprite;

    public void SetKitchenObjectSO(KitchenObjectSO kitchenObjectSO)
    {
        this.iconSprite.sprite = kitchenObjectSO.icon;
    }
}
