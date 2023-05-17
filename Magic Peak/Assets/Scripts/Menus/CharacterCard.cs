using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterCard : MonoBehaviour
{
    [SerializeField] private Image characterImage;
    [SerializeField] private TextMeshProUGUI characterName;

    void Awake()
    {
        characterImage = this.transform.Find("CharacterSprite").GetComponent<Image>();
        characterName = this.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt(gameObject.name) == 0)
        {
            characterImage.color = new Color(0f, 0f, 0f, 1f);
            characterName.text = "???";
        }
        else
        {
            characterImage.color = new Color(1f, 1f, 1f, 1f);
            characterName.text = gameObject.name;
        }
    }

    public void SelectCharacter()
    {
        if (PlayerPrefs.GetInt(gameObject.name) == 0)
        {
            return;
        }
        PlayerPrefs.SetString("CharacterSelected", gameObject.name);
        PlayerPrefs.Save();
    }
}
