using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnSpawnedPlate;
    public event EventHandler OnRemovedPlate;

    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;

    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 4f;
    private int platesSpawnedAmount;
    private int platesSpawnedAmountMax = 4;

    private void Update()
    {
        this.spawnPlateTimer += Time.deltaTime;
        if (this.spawnPlateTimer > spawnPlateTimerMax)
        {
            this.spawnPlateTimer = 0f;
            if (GameManager.Instance.IsGamePlaying() && this.platesSpawnedAmount < this.platesSpawnedAmountMax)
            {
                this.platesSpawnedAmount++;
                OnSpawnedPlate?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            //Player is empty hand
            if (this.platesSpawnedAmount > 0)
            {
                //There's at least one plate here
                this.platesSpawnedAmount--;

                KitchenObject.SpawnKitchenObject(this.plateKitchenObjectSO, player);

                OnRemovedPlate?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
