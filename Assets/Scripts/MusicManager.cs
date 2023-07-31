using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private const string PLAYER_REFS_MUSIC_VOLUME = "MusicVolume";
    public static MusicManager Instance { get; private set; }

    private AudioSource audioSrc;
    private float volume = 0.3f;
    private void Awake()
    {
        Instance = this;
        this.audioSrc = GetComponent<AudioSource>();
        this.volume = PlayerPrefs.GetFloat(PLAYER_REFS_MUSIC_VOLUME, 0.3f);
        audioSrc.volume = this.volume;
    }

    public void ChangeVolume()
    {
        this.volume += 0.1f;
        if (this.volume > 1f)
        {
            this.volume = 0f;
        }
        audioSrc.volume = this.volume;
        PlayerPrefs.SetFloat(PLAYER_REFS_MUSIC_VOLUME, this.volume);
    }

    public float GetVolume()
    {
        return this.volume;
    }
}
