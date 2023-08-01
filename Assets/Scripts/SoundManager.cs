using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private const string PLAYER_REFS_SOUND_EFFECT_VOLUME = "SoundEffectVolume";
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioSoundRefsSO audioSoundRefsSO;
    private float volume = 1f;

    private void Awake()
    {
        Instance = this;
        this.volume = PlayerPrefs.GetFloat(PLAYER_REFS_SOUND_EFFECT_VOLUME, 1f);
    }
    private void Start()
    {
        DeliveryManager.Instance.OnDeliveredSuccess += DeliveryManager_OnDeliveredSuccess;
        DeliveryManager.Instance.OnDeliveredFail += DeliveryManager_OnDeliveredFail;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.OnAnyPickupSomething += Player_OnPickupSomething;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(audioSoundRefsSO.trash, trashCounter.transform.position);
    }

    private void BaseCounter_OnAnyObjectPlacedHere(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioSoundRefsSO.objectDrop, baseCounter.transform.position);
    }

    private void Player_OnPickupSomething(object sender, System.EventArgs e)
    {
        Player player = sender as Player;
        PlaySound(audioSoundRefsSO.objectPickup, player.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioSoundRefsSO.chop, cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnDeliveredFail(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioSoundRefsSO.deliveryFail, deliveryCounter.transform.position);
    }

    private void DeliveryManager_OnDeliveredSuccess(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioSoundRefsSO.deliverySuccess, deliveryCounter.transform.position);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMutiply = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMutiply * this.volume);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }

    public void PlayFootStepSound(Vector3 position, float volume)
    {
        PlaySound(audioSoundRefsSO.footStep, position, volume);
    }

    public void PlayCountDownSound()
    {
        PlaySound(audioSoundRefsSO.warning, Vector3.zero);
    }

    public void PlayWarningSound(Vector3 position)
    {
        PlaySound(audioSoundRefsSO.warning, position);
    }

    public void ChangeVolume()
    {
        this.volume += 0.1f;
        if (this.volume > 1f)
        {
            this.volume = 0f;
        }
        PlayerPrefs.SetFloat(PLAYER_REFS_SOUND_EFFECT_VOLUME, this.volume);
    }

    public float GetVolume()
    {
        return this.volume;
    }
}
