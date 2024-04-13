using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGameSounds : MonoBehaviour
{
    [SerializeField]
    private AudioSource _ballSound;

    [SerializeField]
    private AudioSource _coinSound;

    private readonly string _volumeKey = "Volume";

    public void PlayBallSound()
    {
        _ballSound.volume = PlayerPrefs.GetFloat(_volumeKey);
        _ballSound.Play();
    }

    public void PlayCoinSound()
    {
        _coinSound.volume = PlayerPrefs.GetFloat(_volumeKey);
        _coinSound.Play();
    }
}
