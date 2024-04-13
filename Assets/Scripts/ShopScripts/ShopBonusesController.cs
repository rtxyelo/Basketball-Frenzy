using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopBonusesController : MonoBehaviour
{
    [SerializeField]
    private List<TMP_Text> _bonusTexts = new(2);

    [SerializeField]
    private List<TMP_Text> _bonusPrisesTexts = new(2);

    [SerializeField]
    private List<Image> _bonusIcons = new(2);

    private readonly string _moneyKey = "Money";

    private readonly string _moneyBonusKey = "MoneyBonus";

    private readonly string _pointBonusKey = "PointBonus";

    private int _moneyBonusCount = 50;

    private int _pointBonusCount = 35;

    [SerializeField]
    private GameObject buyPanel;

    [SerializeField]
    private TMP_Text _buyPanelText;

    [SerializeField]
    private Button agreeBuyButton;

    [SerializeField]
    private Button disagreeBuyButton;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey(_moneyBonusKey))
            PlayerPrefs.SetInt(_moneyBonusKey, Convert.ToInt32(false));

        if (!PlayerPrefs.HasKey(_pointBonusKey))
            PlayerPrefs.SetInt(_pointBonusKey, Convert.ToInt32(false));

        if (PlayerPrefs.GetInt(_moneyBonusKey) == 0)
        {
            _bonusTexts[0].text = "";
            _bonusPrisesTexts[0].text = _moneyBonusCount.ToString();
            _bonusIcons[0].enabled = true;
        }
        else
        {
            _bonusTexts[0].text = "Equipped";
            _bonusPrisesTexts[0].text = "";
            _bonusIcons[0].enabled = false;
        }


        if (PlayerPrefs.GetInt(_pointBonusKey) == 0)
        {
            _bonusTexts[1].text = "";
            _bonusPrisesTexts[1].text = _pointBonusCount.ToString();
            _bonusIcons[1].enabled = true;
        }
        else
        {
            _bonusTexts[1].text = "Equipped";
            _bonusPrisesTexts[1].text = "";
            _bonusIcons[1].enabled = false;
        }
    }

    private void BuyDisagree()
    {
        buyPanel.SetActive(false);
    }

    private void BuyMoneyBonus()
    {
        if (PlayerPrefs.GetInt(_moneyKey) >= _moneyBonusCount)
        {
            PlayerPrefs.SetInt(_moneyBonusKey, 1);
            PlayerPrefs.SetInt(_moneyKey, PlayerPrefs.GetInt(_moneyKey) - _moneyBonusCount);
            _bonusTexts[0].text = "Equipped";
            _bonusPrisesTexts[0].text = "";
            _bonusIcons[0].enabled = false;
            buyPanel.SetActive(false);
        }
        else
        {
            _buyPanelText.text = "You have not money!";
        }
    }

    private void BuyPointBonus()
    {
        if (PlayerPrefs.GetInt(_moneyKey) >= _pointBonusCount)
        {
            PlayerPrefs.SetInt(_pointBonusKey, 1);
            PlayerPrefs.SetInt(_moneyKey, PlayerPrefs.GetInt(_moneyKey) - _pointBonusCount);
            _bonusTexts[1].text = "Equipped";
            _bonusPrisesTexts[1].text = "";
            _bonusIcons[1].enabled = false;
            buyPanel.SetActive(false);
        }
        else
        {
            _buyPanelText.text = "You have not money!";
        }
    }

    public void TryBuyBonus(int bonusIndex)
    {
        switch (bonusIndex)
        {
            case 0:
                {
                    if (PlayerPrefs.GetInt(_moneyBonusKey) == 0)
                    {
                        _buyPanelText.text = "You agree to buy this item?";

                        buyPanel.SetActive(true);

                        agreeBuyButton.onClick.RemoveAllListeners();
                        agreeBuyButton.onClick.AddListener(() => BuyMoneyBonus());

                        disagreeBuyButton.onClick.AddListener(BuyDisagree);
                    }
                    break;
                }

            case 1:
                if (PlayerPrefs.GetInt(_pointBonusKey) == 0)
                {
                    _buyPanelText.text = "You agree to buy this item?";

                    buyPanel.SetActive(true);

                    agreeBuyButton.onClick.RemoveAllListeners();
                    agreeBuyButton.onClick.AddListener(() => BuyPointBonus());

                    disagreeBuyButton.onClick.AddListener(BuyDisagree);
                }
                break;

            default:
                break;
        }
    }
}
