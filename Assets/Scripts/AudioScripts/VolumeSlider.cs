using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;
    
    private AudioController _musicController;
    
    public void Initialize()
    {
        //_musicController = AudioController.Instance;
        _musicController = FindObjectOfType<AudioController>();
        _slider.value = _musicController.Volume;
    }
    public void ChangeVolume()
    {
        _musicController.ChangeVolume(_slider.value);
    }
}
