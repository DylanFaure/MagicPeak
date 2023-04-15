using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Cards : MonoBehaviour
{
    public CardData card;

    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI nameCharacter;

    void Start()
    {
        if (card)
        {
            image.sprite = card.imageCard;
            nameCharacter.text = card.nameCard;
        }
    }

    void Update()
    {
        Start();
    }
}
