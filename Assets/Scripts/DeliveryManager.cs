using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
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
    private List<RecipeSO> waitinggRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipesMax = 4;
    private int recipesDeliveredSuccessCount;

    private void Awake()
    {
        Instance = this;

        this.waitinggRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        this.spawnRecipeTimer -= Time.deltaTime;
        if (this.spawnRecipeTimer <= 0)
        {
            this.spawnRecipeTimer = spawnRecipeTimerMax;

            if (GameManager.Instance.IsGamePlaying() && this.waitinggRecipeSOList.Count < waitingRecipesMax)
            {
                RecipeSO waitingRecipeSO = this.recipeListSO.recipeSOList[UnityEngine.Random.Range(0, this.recipeListSO.recipeSOList.Count)];
                this.waitinggRecipeSOList.Add(waitingRecipeSO);
                OnSpawnedRecipe?.Invoke(this, EventArgs.Empty);
            }
        }

    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < this.waitinggRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = this.waitinggRecipeSOList[i];
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
                    this.recipesDeliveredSuccessCount++;
                    this.waitinggRecipeSOList.RemoveAt(i);
                    GameManager.Instance.AddMorePlayTime(waitingRecipeSO.cookingTime);
                    OnCompletedRecipe?.Invoke(this, EventArgs.Empty);
                    OnDeliveredSuccess?.Invoke(this, new OnDeliveredSuccessArgs { timePlus = waitingRecipeSO.cookingTime });
                    return;
                }
            }
        }
        //No matches found
        Debug.Log("Player delivered wrong recipe !");
        OnDeliveredFail?.Invoke(this, new OnDeliveredFailArgs { timePlus = 0f });
    }

    public List<RecipeSO> GetWaitingRecipeListSO()
    {
        return this.waitinggRecipeSOList;
    }

    public int GetRecipesDeliveredSuccessCount()
    {
        return this.recipesDeliveredSuccessCount;
    }
}
