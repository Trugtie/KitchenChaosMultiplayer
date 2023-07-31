using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockTimerUI : MonoBehaviour
{
    [SerializeField] private Image clockTimerImg;

    private void Update()
    {
        clockTimerImg.fillAmount = GameManager.Instance.GetGamePlayingTimerNormalize();
    }
}
