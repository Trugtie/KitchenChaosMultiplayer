using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private Player player;
    private float footstepTimer;
    private float footstepTimerMax = .1f;

    private void Awake()
    {
        this.player = GetComponent<Player>();
    }
    private void Update()
    {
        this.footstepTimer -= Time.deltaTime;
        if (this.footstepTimer < 0)
        {
            this.footstepTimer = this.footstepTimerMax;
            if (player.IsWalking())
            {
                float volume = 1f;
                SoundManager.Instance.PlayFootStepSound(player.transform.position, volume);
            }
        }
    }

}
