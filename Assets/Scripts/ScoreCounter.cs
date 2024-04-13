using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _scoreText;

    private int _score = 0;

    private bool _pointBonus;

    private readonly string _recordKey = "Record";

    private readonly string _pointBonusKey = "PointBonus";

    private void Awake()
    {
        if (!PlayerPrefs.HasKey(_recordKey))
            PlayerPrefs.SetInt(_recordKey, 0);

        if (!PlayerPrefs.HasKey(_pointBonusKey))
            PlayerPrefs.SetInt(_pointBonusKey, Convert.ToInt32(false));

        _pointBonus = Convert.ToBoolean(PlayerPrefs.GetInt(_pointBonusKey, 0));
    }

    private void Start()
    {
        _scoreText.text = _score.ToString();
    }

    public void IncreaseScore()
    {
        //Debug.Log("Score increase!");

        _pointBonus = Convert.ToBoolean(PlayerPrefs.GetInt(_pointBonusKey, 0));

        _score = _pointBonus == true ? _score + 20 : _score + 10;

        _scoreText.text = _score.ToString();

        if (_score > PlayerPrefs.GetInt(_recordKey, 0))
            PlayerPrefs.SetInt(_recordKey, _score);
    }
}