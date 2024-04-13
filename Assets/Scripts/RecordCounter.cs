using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RecordCounter : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _scoreText;

    private int _record;

    private readonly string _recordKey = "Record";

    private void Awake()
    {
        if (!PlayerPrefs.HasKey(_recordKey))
            PlayerPrefs.SetInt(_recordKey, 0);

        _record = PlayerPrefs.GetInt(_recordKey);
    }

    private void Start()
    {
        _scoreText.text = _record.ToString();
    }
}