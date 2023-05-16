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
    [SerializeField] private GameObject rarityDisplay;
    [SerializeField] private GameObject[] yellowStarsRarity;
    [SerializeField] private GameObject[] purpleStarsRarity;

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

    [Header("Settings")]
    [SerializeField] private float animRarityTime = 1f;
    [SerializeField] private float animDelayStarsTime = 0.5f;
    [SerializeField] private float animCardTime = 4.0f;
    [SerializeField] private float nextPullDelay = 2.0f;
    [SerializeField] private float displayErrorMessageTime = 5.0f;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip threeStarPull;
    [SerializeField] private AudioClip fourStarPull;
    [SerializeField] private AudioClip fiveStarPull;
    [SerializeField] private AudioClip[] purpleStarsPull;
    [SerializeField] private float defaultSFXVolume = 0.5f;
    
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

    private void DisableStars()
    {
        for (int i = 0; i < yellowStarsRarity.Length; i++) {
            yellowStarsRarity[i].SetActive(false);
        }

        for (int i = 0; i < purpleStarsRarity.Length; i++) {
            purpleStarsRarity[i].SetActive(false);
        }
    }

    IEnumerator DisplayYellowStarsRarity(int rarity)
    {
        if (audioSource.volume <= 0)
        {
            audioSource.volume = defaultSFXVolume;
        }
        float previousAudioSourceVolume = audioSource.volume;
        audioSource.volume = previousAudioSourceVolume * 0.5f;
        for (int i = 0; i < rarity; i++)
        {
            if (i < 3)
            {
                audioSource.PlayOneShot(threeStarPull);
            }
            else if (i < 4)
            {
                audioSource.volume = audioSource.volume + (audioSource.volume / 1.2f);
                audioSource.PlayOneShot(fourStarPull);
            }
            else
            {
                audioSource.volume = previousAudioSourceVolume;
                audioSource.PlayOneShot(fiveStarPull);
            }
            yellowStarsRarity[i].SetActive(true);
            yield return new WaitForSeconds(animDelayStarsTime);
        }
        audioSource.volume = previousAudioSourceVolume;
    }

    IEnumerator DisplayPurpleStarsRarity()
    {
        if (audioSource.volume <= 0)
        {
            audioSource.volume = defaultSFXVolume;
        }
        float previousAudioSourceVolume = audioSource.volume;
        for (int i = 0; i < purpleStarsRarity.Length; i++)
        {
            audioSource.volume = previousAudioSourceVolume * (i * 0.1f + 0.4f);
            purpleStarsRarity[i].SetActive(true);
            audioSource.PlayOneShot(purpleStarsPull[i]);
            yield return new WaitForSeconds(animDelayStarsTime);
        }
        audioSource.volume = previousAudioSourceVolume;
    }

    private void DisplayRarityStars(int rarity)
    {
        if (rarity == 6)
        {
            StartCoroutine(DisplayPurpleStarsRarity());
        }
        else
        {
            StartCoroutine(DisplayYellowStarsRarity(rarity));
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
        rarityDisplay.SetActive(false);
        DisableStars();
    }

    private void DisplayRarity()
    {
        rarityDisplay.SetActive(true);
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
        DisplayRarity();
        yield return new WaitForSeconds(animRarityTime);
        DisplayRarityStars(item.rarity);
        yield return new WaitForSeconds(animCardTime);
        CardEffects();
        yield return new WaitForSeconds(nextPullDelay);
        EnableButton();
        AppearBalanceText();
        pullCoroutineState = false;
    }

    IEnumerator YellowStarsEffects(int i)
    {
        yellowStarsRarity[i].SetActive(true);
        yield return new WaitForSeconds(animDelayStarsTime);
    }

    IEnumerator PurpleStarsEffects(int i)
    {
        purpleStarsRarity[i].SetActive(true);
        yield return new WaitForSeconds(animDelayStarsTime);
    }

    IEnumerator ShowAndHide()
    {
        errorText.text = "Insufficient balance to spin gacha wheel.";
        yield return new WaitForSeconds(displayErrorMessageTime);
        errorText.text = "";
    }
}
