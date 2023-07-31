using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    const string OPEN_CLOSE = "OpenClose";

    [SerializeField] private ContainerCounter containerCouter;
    private Animator animator;
    private void Awake()
    {
        this.animator = GetComponent<Animator>();
    }
    private void Start()
    {
        containerCouter.OnPlayerGrabbedObject += ContainerCouter_OnPlayerGrabbedObject;
    }

    private void ContainerCouter_OnPlayerGrabbedObject(object sender, System.EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
