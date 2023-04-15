using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GatchaRate
{
    public string rarity;

    [Range(1,100)]
    public int rate;

    public CardData[] reward;
}
