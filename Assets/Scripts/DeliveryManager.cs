using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class DeliveryManager : NetworkBehaviour
{
    public event EventHandler<OnDeliveredSuccessArgs> OnDeliveredSuccess;
    public event EventHandler<OnDeliveredFailArgs> OnDeliveredFail;
    public event EventHandler OnSpawnedRecipe;
    public event EventHandler OnCompletedRecipe;

    public class OnDeliveredSuccessArgs : EventArgs
    {
        public float timePlus;
    }

    public class OnDeliveredFailArgs : EventArgs
    {
        public float timePlus;
    }

    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSO recipeListSO;
    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer = 4f;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipesMax = 4;
    private int recipesDeliveredSuccessCount;

    private void Awake()
    {
        Instance = this;

        this.waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        this.spawnRecipeTimer -= Time.deltaTime;
        if (this.spawnRecipeTimer <= 0)
        {
            this.spawnRecipeTimer = spawnRecipeTimerMax;

            if (GameManager.Instance.IsGamePlaying() && this.waitingRecipeSOList.Count < waitingRecipesMax)
            {
                int waitingRecipeListSOIndex = UnityEngine.Random.Range(0, this.recipeListSO.recipeSOList.Count);
                SpawnNewWaitingRecipeClientRpc(waitingRecipeListSOIndex);

            }
        }

    }

    [ClientRpc]
    private void SpawnNewWaitingRecipeClientRpc(int waitingRecipeListSOIndex)
    {
        RecipeSO waitingRecipeSO = this.recipeListSO.recipeSOList[waitingRecipeListSOIndex];
        this.waitingRecipeSOList.Add(waitingRecipeSO);
        OnSpawnedRecipe?.Invoke(this, EventArgs.Empty);
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < this.waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = this.waitingRecipeSOList[i];
            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                //Has the same number of ingredients
                bool isPlateIngredientMatchesRecipe = true;
                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {
                    //Cycles through all ingredients in the recipe
                    bool isIngerdientFound = false;
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        //Cycles through all ingredients in the plate
                        if (recipeKitchenObjectSO == plateKitchenObjectSO)
                        {
                            //Ingredient matches!
                            isIngerdientFound = true;
                            break;
                        }
                    }
                    if (!isIngerdientFound)
                    {
                        //The recipe ingredient was not found on the plate
                        isPlateIngredientMatchesRecipe = false;
                        break;
                    }
                }
                if (isPlateIngredientMatchesRecipe)
                {
                    Debug.Log("Player delivered correct recipe");
                    DeliveredCorrectRecipeServerRpc(i);
                    return;
                }
            }
        }
        //No matches found
        Debug.Log("Player delivered wrong recipe !");
        DeliveredInCorrectRecipeServerRpc();

    }

    [ServerRpc(RequireOwnership = false)]
    private void DeliveredInCorrectRecipeServerRpc()
    {
        DeliveredInCorrectRecipeClientRpc();
    }

    [ClientRpc]
    private void DeliveredInCorrectRecipeClientRpc()
    {
        OnDeliveredFail?.Invoke(this, new OnDeliveredFailArgs { timePlus = 0f });
    }

    [ServerRpc(RequireOwnership = false)]
    private void DeliveredCorrectRecipeServerRpc(int waitingRecipeSOListIndex)
    {
        RecipeSO waitingRecipeSO = this.waitingRecipeSOList[waitingRecipeSOListIndex];
        GameManager.Instance.AddMorePlayTime(waitingRecipeSO.cookingTime);
        DeliveredCorrectRecipeClientRpc(waitingRecipeSOListIndex);
    }

    [ClientRpc]
    private void DeliveredCorrectRecipeClientRpc(int waitingRecipeSOListIndex)
    {
        this.recipesDeliveredSuccessCount++;
        RecipeSO waitingRecipeSO = this.waitingRecipeSOList[waitingRecipeSOListIndex];
        this.waitingRecipeSOList.RemoveAt(waitingRecipeSOListIndex);
        OnCompletedRecipe?.Invoke(this, EventArgs.Empty);
        OnDeliveredSuccess?.Invoke(this, new OnDeliveredSuccessArgs { timePlus = waitingRecipeSO.cookingTime });
    }


    public List<RecipeSO> GetWaitingRecipeListSO()
    {
        return this.waitingRecipeSOList;
    }

    public int GetRecipesDeliveredSuccessCount()
    {
        return this.recipesDeliveredSuccessCount;
    }
}
