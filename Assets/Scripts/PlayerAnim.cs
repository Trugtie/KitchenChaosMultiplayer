using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    const string IS_WALKING = "IsWalking";

    [SerializeField] private Player player;
    private Animator animator;
    private void Awake()
    {
        this.animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool(IS_WALKING, player.IsWalking());
    }
}
