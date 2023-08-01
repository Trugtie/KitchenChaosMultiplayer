using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerAnim : NetworkBehaviour
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
        if (!IsOwner) return;
        animator.SetBool(IS_WALKING, player.IsWalking());
    }
}
