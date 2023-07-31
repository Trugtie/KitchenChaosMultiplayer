using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnAddedIngredientEventArgs> OnAddedIngredient;
    public class OnAddedIngredientEventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }

    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;
    private List<KitchenObjectSO> kitchenObjectSOList;

    private void Awake()
    {
        this.kitchenObjectSOList = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if (!this.validKitchenObjectSOList.Contains(kitchenObjectSO))
        {
            //Not valid ingredient
            return false;
        }
        if (this.kitchenObjectSOList.Contains(kitchenObjectSO))
        {
            //Already has this type
            return false;
        }
        else
        {
            this.kitchenObjectSOList.Add(kitchenObjectSO);
            OnAddedIngredient?.Invoke(this, new OnAddedIngredientEventArgs
            {
                kitchenObjectSO = kitchenObjectSO
            });
            return true;
        }
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return this.kitchenObjectSOList;
    }
}
