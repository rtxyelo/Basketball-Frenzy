using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MoneyCounter : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _moneyText;

    private int _money;

    private bool _moneyBonus;

    private readonly string _moneyBonusKey = "MoneyBonus";

    private readonly string _moneyKey = "Money";

    private void Awake()
    {
        if (!PlayerPrefs.HasKey(_moneyKey))
            PlayerPrefs.SetInt(_moneyKey, 0);

        if (!PlayerPrefs.HasKey(_moneyBonusKey))
            PlayerPrefs.SetInt(_moneyBonusKey, Convert.ToInt32(false));

        _moneyBonus = Convert.ToBoolean(PlayerPrefs.GetInt(_moneyBonusKey, 0));
        _money = PlayerPrefs.GetInt(_moneyKey, 0);
    }

    private void Start()
    {
        _moneyText.text = _money.ToString();
    }

    private void Update()
    {
        _money = PlayerPrefs.GetInt(_moneyKey, 0);
        _moneyText.text = _money.ToString();
    }

    public void IncreaseMoney()
    {
        //Debug.Log("Money increase!");

        _moneyBonus = Convert.ToBoolean(PlayerPrefs.GetInt(_moneyBonusKey, 0));

        _money = _moneyBonus == true ? _money + 2 : _money + 1;
        _moneyText.text = _money.ToString();

        PlayerPrefs.SetInt(_moneyKey, _money);
    }
}
