using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GachaManager : MonoBehaviour {
    [SerializeField] private int balance = 100;
    [SerializeField] private int price = 10;
    [SerializeField] private Item[] itemList;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private Image resultImage;

    void Start()
    {
        resultText.text = "";
    }

    public void Pull()
    {
        if (balance < price)
        {
            resultText.text = "Insufficient balance to spin gacha wheel.";
            return;
        }

        balance -= price;

        Item itemWon = null;
        int rarityRoll = Random.Range(1, 101);
        
        foreach (Item item in itemList)
        {
            if (rarityRoll <= item.dropRate)
            {
                itemWon = item;
                break;
            }
            rarityRoll -= item.dropRate;
        }

        resultText.text = "Congratulations! You won " + itemWon.nameCard + "!";
        resultImage.sprite = itemWon.sprite;

        // Add the item to the user's inventory
    }
}
