using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryMangerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;

    private void Awake()
    {
        this.recipeTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnSpawnedRecipe += DeliveryManager_OnSpawnedRecipe;
        DeliveryManager.Instance.OnCompletedRecipe += DeliveryManager_OnCompletedRecipe;
        UpdateVisual();
    }

    private void DeliveryManager_OnSpawnedRecipe(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void DeliveryManager_OnCompletedRecipe(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in container)
        {
            if (child == recipeTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeListSO())
        {
            Transform recipeTransform = Instantiate(recipeTemplate, container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipeSO);
        }
    }
}
