using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public enum Sounds
    {
        BuildingPlaced,
        BuildingDamaged,
        BuildingDestroyed,
        EnemyDie,
        EnemyHit,
        GameOver,
    }

    private AudioSource audioSource;
    private Dictionary<Sounds, AudioClip> soundsAudioClipDictionary;
    private float volume = .5f;
    

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();

        volume = PlayerPrefs.GetFloat("soundVolume", .5f);

        soundsAudioClipDictionary = new Dictionary<Sounds, AudioClip>();

        foreach(Sounds sounds in System.Enum.GetValues(typeof(Sounds)))
        {
            soundsAudioClipDictionary[sounds] = Resources.Load<AudioClip>(sounds.ToString());
        }
    }

    public void PlaySound(Sounds sounds)
    {
        audioSource.PlayOneShot(soundsAudioClipDictionary[sounds], volume);
    }

    public void IncreaseVolume()
    {
        volume += .1f;
        volume = Mathf.Clamp01(volume);
        PlayerPrefs.SetFloat("soundVolume", volume);
    }

    public void DecreaseVolume()
    {
        volume -= .1f;
        volume = Mathf.Clamp01(volume);
        PlayerPrefs.SetFloat("soundVolume", volume);
    }

    public float GetVolume()
    {
        return volume;
    }
}
