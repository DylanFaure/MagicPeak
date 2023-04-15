using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new card", menuName = "Character")]

public class CardData : ScriptableObject
{
    public Sprite imageCard;
    public string nameCard;
}
