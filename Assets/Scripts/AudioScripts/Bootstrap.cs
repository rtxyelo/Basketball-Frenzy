using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private AudioController _controller;
    [SerializeField] private VolumeSlider _slider;
    void Start()
    {
        _controller.Initialize();
        _slider.Initialize();
    }
}
