using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private MeshRenderer headMeshRenderer;

    [SerializeField] private MeshRenderer bodyMeshRenderer;

    private Material material;

    private void Awake()
    {
        this.material = new Material(this.headMeshRenderer.material);
        this.headMeshRenderer.material = this.material;
        this.bodyMeshRenderer.material = this.material;
    }

    public void SetPlayerColor(Color color)
    {
        this.material.color = color;
    }
}
