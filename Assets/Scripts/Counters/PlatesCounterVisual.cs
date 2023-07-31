using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisualPrefab;

    private List<GameObject> plateVisualList;
    private void Awake()
    {
        this.plateVisualList = new List<GameObject>();
    }

    private void Start()
    {
        platesCounter.OnSpawnedPlate += PlatesCounter_OnSpawnedPlate;
        platesCounter.OnRemovedPlate += PlatesCounter_OnRemovedPlate;
    }

    private void PlatesCounter_OnSpawnedPlate(object sender, System.EventArgs e)
    {
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);

        float plateOffsetY = 0.1f;
        plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * this.plateVisualList.Count, 0);

        this.plateVisualList.Add(plateVisualTransform.gameObject);
    }

    private void PlatesCounter_OnRemovedPlate(object sender, System.EventArgs e)
    {
        GameObject plateGameObject = this.plateVisualList[this.plateVisualList.Count - 1];
        this.plateVisualList.Remove(plateGameObject);
        Destroy(plateGameObject);
    }
}
