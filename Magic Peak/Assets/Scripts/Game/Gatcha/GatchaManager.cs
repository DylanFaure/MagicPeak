using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatchaManager : MonoBehaviour
{
    [SerializeField] private GatchaRate[] gatcha;
    [SerializeField] private Transform parent, pos;
    [SerializeField] private GameObject characterCardGO;

    GameObject characterCard;
    Cards card;

    public void Gatcha()
    {
        if (!characterCard)
        {
            characterCard = Instantiate(characterCardGO, pos.position, Quaternion.identity) as GameObject;
            characterCard.transform.SetParent(parent);
            characterCard.transform.localScale = new Vector3(1, 1, 1);
            card = characterCard.GetComponent<Cards>();
        }

        int random = UnityEngine.Random.Range(1, 101);
        Debug.Log(random);
        for(int i = 0; random < gatcha.Length; i++)
        {
            if (random <= gatcha[i].rate)
            {
                card.card = Reward(gatcha[i].rarity);
                return;
            }
        }
    }

    CardData Reward(string rarity)
    {
        GatchaRate gr = Array.Find(gatcha, rt => rt.rarity == rarity);
        CardData[] reward = gr.reward;

        int random = UnityEngine.Random.Range(1, reward.Length);

        return reward[random];
    }
}
