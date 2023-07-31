using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject stoveOnParticle;
    [SerializeField] private GameObject stoveOnGameObject;

    private void Start()
    {
        this.stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool isShowVisual = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        this.stoveOnParticle.SetActive(isShowVisual);
        this.stoveOnGameObject.SetActive(isShowVisual);
    }
}
