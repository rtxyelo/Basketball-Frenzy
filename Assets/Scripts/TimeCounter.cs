using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    [SerializeField]
    private GameController _gameController;

    [SerializeField]
    private TMP_Text _timeText;

    private void Update()
    {
        int time = _gameController.GameTime;
        if (time >= 0)
            _timeText.text = time.ToString();
    }
}