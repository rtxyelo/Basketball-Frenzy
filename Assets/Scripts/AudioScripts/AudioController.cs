using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource _musicSource;

    private readonly string _volumeKey = "Volume";

    public static AudioController Instance;

    public float Volume;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);

        Instance = this;
        //Debug.Log("Instance create! " + Instance);
        
        DontDestroyOnLoad(gameObject);
    }
    private void OnDisable()
    {
        PlayerPrefs.SetFloat(_volumeKey, Volume);
    }
    public void Initialize()
    {
        _musicSource = GetComponent<AudioSource>();

        if (!PlayerPrefs.HasKey(_volumeKey))
            PlayerPrefs.GetFloat(_volumeKey, 0);

        Volume = PlayerPrefs.GetFloat(_volumeKey);
    }
    public void ChangeVolume(float newVolume)
    {
        Volume = newVolume;
        PlayerPrefs.SetFloat(_volumeKey, Volume);
        _musicSource.volume = Volume;
    }
}
