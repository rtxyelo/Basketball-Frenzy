using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    [Tooltip("List of ball select/buy buttons.")]
    [SerializeField]
    private List<Button> buttons = new(5);

    [Tooltip("List of balls prices.")]
    [SerializeField]
    private List<int> _ballsPrices = new(4) { 5, 7, 100, 200 };

    [SerializeField]
    private GameObject buyPanel;

    [SerializeField]
    private TMP_Text _buyPanelText;

    [SerializeField]
    private Button agreeBuyButton;

    [SerializeField]
    private Button disagreeBuyButton;

    private readonly string _ownedBallsKey = "OwnedBalls";

    private readonly string _ballKey = "Ball";

    private readonly string _moneyKey = "Money";

    public UnityEvent<int> ShipEquipped;

    private void Awake()
    {
        ShipEquipped = new UnityEvent<int>();

        if (!PlayerPrefs.HasKey(_ownedBallsKey))
            PlayerPrefs.SetInt(_ownedBallsKey, 1);

        for (int i = 0; i < buttons.Count; i++)
        {
            int buttonIndex = i + 1;
            buttons[i].onClick.AddListener(delegate {

                //ToggleAllButtons(buttons, false);
                CheckUsability(buttonIndex);

            });
        }
    }


    public bool IsShipOwned(BallType ballType, int ownedBalls)
    {
        return ((ownedBalls & (int)ballType) == (int)ballType);
    }

    private void CheckUsability(int ball)
    {
        _buyPanelText.text = "You agree to buy this item?";

        int ownedBalls = PlayerPrefs.GetInt(_ownedBallsKey, 1);

        bool isBallOwned = IsShipOwned((BallType)IndexToShipType(ball), ownedBalls);

        Debug.Log($"Ball {ball} owned status is " + isBallOwned);

        if (isBallOwned)
        {
            Equip(ball);
        }
        else
        {
            buyPanel.SetActive(true);

            agreeBuyButton.onClick.RemoveAllListeners();
            agreeBuyButton.onClick.AddListener(() => BuyAgree((BallType)IndexToShipType(ball), ball, ownedBalls));

            disagreeBuyButton.onClick.AddListener(BuyDisagree);
        }
    }

    private void Equip(int ballIndex)
    {
        // For text change to "Equipped"
        ShipEquipped.Invoke(ballIndex);

        PlayerPrefs.SetInt(_ballKey, ballIndex);

        //ToggleAllButtons(buttons, true);
    }

    private void Update()
    {
        Debug.Log("Current money " + PlayerPrefs.GetInt(_moneyKey, 0));
    }

    private void BuyAgree(BallType ball, int ballIndex, int ownedBalls)
    {
        if (_ballsPrices[ballIndex - 2] <= PlayerPrefs.GetInt(_moneyKey, 0))
        {
            Debug.Log("Ship PRICE " + _ballsPrices[ballIndex - 2]);
            Debug.Log("Current money " + PlayerPrefs.GetInt(_moneyKey, 0));

            int moneyCount = PlayerPrefs.GetInt(_moneyKey, 0) - _ballsPrices[ballIndex - 2];

            Debug.Log("Result money " + moneyCount);

            PlayerPrefs.SetInt(_moneyKey, moneyCount);

            PlayerPrefs.SetInt(_ballKey, ballIndex);

            ownedBalls |= (int)ball;

            PlayerPrefs.SetInt(_ownedBallsKey, ownedBalls);

            //ToggleAllButtons(buttons, true);

            ShipEquipped.Invoke(ballIndex);

            buyPanel.SetActive(false);
        }
        else
        {
            _buyPanelText.text = "You have not money!";
        }
    }

    private void BuyDisagree()
    {
        //ToggleAllButtons(buttons, true);
        buyPanel.SetActive(false);
    }

    private void ToggleAllButtons(List<Button> buttonsList, bool toggleOn = true)
    {
        if (toggleOn)
            buttonsList.ForEach(button => button.enabled = true);
        else
            buttonsList.ForEach(button => button.enabled = false);
    }

    private void OnDestroy()
    {
        ShipEquipped.RemoveAllListeners();
    }

    public int IndexToShipType(int index)
    {
        switch (index)
        {
            case 1: return 1;
            case 2: return 2;
            case 3: return 4;
            case 4: return 8;
            case 5: return 16;
            default: return 0;
        }
    }
}

[Flags]
public enum BallType
{
    // Balls 54321
    // Mask  11111
    None = 0,
    BallOne = 1 << 0,
    BallTwo = 1 << 1,
    BallThree = 1 << 2,
    BallFour = 1 << 3,
    BallFive = 1 << 4,
}