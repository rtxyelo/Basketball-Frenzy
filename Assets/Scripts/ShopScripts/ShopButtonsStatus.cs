using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.SplashScreen;


public class ShopButtonsStatus : MonoBehaviour
{
    [Tooltip("List of shop buttons prices texts.")]
    [SerializeField]
    private List<TMP_Text> shopButtonsPricesTexts = new(4);

    [Tooltip("List of shop buttons prices coins.")]
    [SerializeField]
    private List<Image> shopButtonsPricesCoins = new(4);

    [Tooltip("List of shop buttons texts.")]
    [SerializeField]
    private List<TMP_Text> shopButtonsTexts = new(5);

    private ShopController shopBehaviour;

    private readonly string _ownedBallsKey = "OwnedBalls";

    private readonly string _ballKey = "Ball";

    private void Awake()
    {
        shopBehaviour = GetComponent<ShopController>();
        shopBehaviour.ShipEquipped.AddListener(UpdateButtonsStatus);
    }

    private void Start()
    {
        Debug.Log("Current ship " + PlayerPrefs.GetInt(_ballKey, 1));
        UpdateButtonsStatus(PlayerPrefs.GetInt(_ballKey, 1));

        BallType shipTypeIndex = (BallType)IndexToShipType(PlayerPrefs.GetInt(_ballKey, 1));

        Debug.Log("ShipType " + shipTypeIndex);
        Debug.Log("ShipType " + (int)shipTypeIndex);
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

    private void UpdateButtonsStatus(int shipIndex)
    {
        for (int i = 0; i < shopButtonsTexts.Count; i++)
        {
            bool isShipOwned = shopBehaviour.IsShipOwned((BallType)IndexToShipType(i + 1), PlayerPrefs.GetInt(_ownedBallsKey, 1));

            //Debug.Log($"Ship {i + 1} owned status is " + isShipOwned);

            if (i + 1 == shipIndex)
            {
                shopButtonsTexts[i].text = "EQUIPPED";

                if (i > 0 && isShipOwned)
                {
                    shopButtonsPricesCoins[i - 1].enabled = false;
                    shopButtonsPricesTexts[i - 1].text = "";
                }
            }
            else if (isShipOwned && shopButtonsTexts[i].text == "EQUIPPED")
            {
                shopButtonsTexts[i].text = "EQUIP";
                if (i > 0)
                {
                    shopButtonsPricesCoins[i - 1].enabled = false;
                    shopButtonsPricesTexts[i - 1].text = "";
                }
            }
            else if (i > 0 && isShipOwned)
            {
                shopButtonsTexts[i].text = "EQUIP";
                shopButtonsPricesCoins[i - 1].enabled = false;
                shopButtonsPricesTexts[i - 1].text = "";
            }
        }
    }
}