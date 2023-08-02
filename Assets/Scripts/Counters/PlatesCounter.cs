using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

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
        if (!IsServer) return;

        this.spawnPlateTimer += Time.deltaTime;
        if (this.spawnPlateTimer > spawnPlateTimerMax)
        {
            this.spawnPlateTimer = 0f;
            if (GameManager.Instance.IsGamePlaying() && this.platesSpawnedAmount < this.platesSpawnedAmountMax)
            {
                SpawnPlatesServerRpc();
            }
        }
    }

    [ServerRpc]
    private void SpawnPlatesServerRpc()
    {
        SpawnPlatescClientRpc();
    }

    [ClientRpc]
    private void SpawnPlatescClientRpc()
    {
        this.platesSpawnedAmount++;
        OnSpawnedPlate?.Invoke(this, EventArgs.Empty);
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            //Player is empty hand
            if (this.platesSpawnedAmount > 0)
            {
                //There's at least one plate here
                KitchenObject.SpawnKitchenObject(this.plateKitchenObjectSO, player);
                InteractLogicServerRpc();
            }
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void InteractLogicServerRpc()
    {
        InteractLogicClientRpc();
    }

    [ClientRpc]
    private void InteractLogicClientRpc()
    {
        this.platesSpawnedAmount--;
        OnRemovedPlate?.Invoke(this, EventArgs.Empty);
    }
}
