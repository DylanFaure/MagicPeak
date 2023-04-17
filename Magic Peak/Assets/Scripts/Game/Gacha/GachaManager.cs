using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GachaManager : MonoBehaviour {
    [Header("Game objects")]
    [SerializeField] private Item[] itemList;
    [SerializeField] private GameObject pullButton;
    [SerializeField] private GameObject card;
    [SerializeField] private GameObject poisonEffectsPrefab;
    [SerializeField] private GameObject iceEffectsPrefab;
    [SerializeField] private GameObject fireEffectsPrefab;
    [SerializeField] private GameObject electricEffectsPrefab;

    [Header("Rate and price")]
    [SerializeField] private int balance = 100;
    [SerializeField] private int price = 10;

    [Header("Texts")]
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

    void Start()
    {
        SetDefaultAll();
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

            resultText.text = itemWon.nameCard;
            resultImage.sprite = itemWon.sprite;

            // Add the item to the user's inventory

            DisableButton();

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
        pullCoroutineState = false;
    }

    IEnumerator ShowAndHide()
    {
        errorText.text = "Insufficient balance to spin gacha wheel.";
        yield return new WaitForSeconds(displayErrorMessageTime);
        errorText.text = "";
    }
}
