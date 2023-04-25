using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GachaManager : MonoBehaviour {
    [Header("Game objects")]
    [SerializeField] private Item[] commonItemList;
    [SerializeField] private Item[] rareItemList;
    [SerializeField] private Item[] epicItemList;
    [SerializeField] private Item[] hybridItemList;
    [SerializeField] private GameObject pullButton;
    [SerializeField] private GameObject card;
    [SerializeField] private GameObject poisonEffectsPrefab;
    [SerializeField] private GameObject iceEffectsPrefab;
    [SerializeField] private GameObject fireEffectsPrefab;
    [SerializeField] private GameObject electricEffectsPrefab;

    [Header("Rate and price")]
    [SerializeField] private int price = 10;
    [SerializeField] private int commonRate = 74;
    [SerializeField] private int rareRate = 20;
    [SerializeField] private int epicRate = 5;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI currentBalanceText;
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private TextMeshProUGUI errorText;

    [Header("Images")]
    [SerializeField] private Image resultImage;

    [Header("Animators")]
    [SerializeField] private Animator animCard;
    [SerializeField] private Animator animPoison;
    [SerializeField] private Animator animIce;
    [SerializeField] private Animator animFire;
    [SerializeField] private Animator animElectric;

    [Header("Settings")]
    [SerializeField] private float animCardTime = 4.0f;
    [SerializeField] private float nextPullDelay = 2.0f;
    [SerializeField] private float displayErrorMessageTime = 5.0f;
    
    private bool pullCoroutineState = false;
    private int balance = 0;

    void Start()
    {
        balance = WalletManager.instance.GetWalletData();
        DisplayCurrentBalance();
        DisplayTextButton();
        SetDefaultAll();
    }

    void Update()
    {
        balance = WalletManager.instance.GetWalletData();
        DisplayCurrentBalance();
        DisplayTextButton();
    }

    public void Pull()
    {
        if (!pullCoroutineState)
        {
            SetDefaultAll();

            if (balance < price)
            {
                StartCoroutine(ShowAndHide());
                return;
            }
            WalletManager.instance.DeductCurrency(price);

            Item itemWon = null;
            int rarityRoll = Random.Range(1, 101);
            
            if (rarityRoll < commonRate)
            {
                int randomIndex = Random.Range(0, commonItemList.Length);
                itemWon = commonItemList[randomIndex];
            }
            else if (rarityRoll < commonRate + rareRate)
            {
                int randomIndex = Random.Range(0, rareItemList.Length);
                itemWon = rareItemList[randomIndex];
            }
            else if (rarityRoll < commonRate + rareRate + epicRate)
            {
                int randomIndex = Random.Range(0, epicItemList.Length);
                itemWon = epicItemList[randomIndex];
            }
            else
            {
                int randomIndex = Random.Range(0, hybridItemList.Length);
                itemWon = hybridItemList[randomIndex];
            }

            resultText.text = itemWon.nameCard;
            resultImage.sprite = itemWon.sprite;
            resultImage.preserveAspect = true;
    
            // Add the item to the user's inventory

            DisableButton();
            DisappearBalanceText();

            StartCoroutine(PullAnimation(itemWon));
            pullCoroutineState = true;
        }
    }

    private void SetDefaultAll()
    {
        resultText.text = "";
        errorText.text = "";
        card.SetActive(false);
        poisonEffectsPrefab.SetActive(false);
        iceEffectsPrefab.SetActive(false);
        fireEffectsPrefab.SetActive(false);
        electricEffectsPrefab.SetActive(false);
    }

    private void DisplayCurrentBalance()
    {
        currentBalanceText.text = balance.ToString() + "$";
    }

    private void DisappearBalanceText()
    {
        currentBalanceText.gameObject.SetActive(false);
    }

    private void AppearBalanceText()
    {
        currentBalanceText.gameObject.SetActive(true);
    }

    private void DisplayTextButton()
    {
        buttonText.text = price.ToString() + "$ PULL";
    }

    private void DisableButton()
    {
        pullButton.SetActive(false);
    }

    private void EnableButton()
    {
        pullButton.SetActive(true);
    }

    private void CardEffects()
    {
        card.SetActive(true);
    }

    private void PoisonEffects()
    {
        poisonEffectsPrefab.SetActive(true);
    }
    
    private void IceEffects()
    {
        iceEffectsPrefab.SetActive(true);
    }

    private void FireEffects()
    {
        fireEffectsPrefab.SetActive(true);
    }

    private void ElectricEffects()
    {
        electricEffectsPrefab.SetActive(true);
    }

    private void BackgroundAnimations(Item item)
    {
        switch (item.firstElement)
        {
            case Item.EnumElements.Poison:
                PoisonEffects();
                break;
            case Item.EnumElements.Ice:
                IceEffects();
                break;
            case Item.EnumElements.Fire:
                FireEffects();
                break;
            case Item.EnumElements.Electric:
                ElectricEffects();
                break;
            default:
                break;
        }

        switch (item.secondeElement)
        {
            case Item.EnumElements.Poison:
                PoisonEffects();
                break;
            case Item.EnumElements.Ice:
                IceEffects();
                break;
            case Item.EnumElements.Fire:
                FireEffects();
                break;
            case Item.EnumElements.Electric:
                ElectricEffects();
                break;
            default:
                break;
        }
    }

    IEnumerator PullAnimation(Item item)
    {
        BackgroundAnimations(item);
        yield return new WaitForSeconds(animCardTime);
        CardEffects();
        yield return new WaitForSeconds(nextPullDelay);
        EnableButton();
        AppearBalanceText();
        pullCoroutineState = false;
    }

    IEnumerator ShowAndHide()
    {
        errorText.text = "Insufficient balance to spin gacha wheel.";
        yield return new WaitForSeconds(displayErrorMessageTime);
        errorText.text = "";
    }
}
