using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New card", menuName = "Character")]
public class Item : ScriptableObject
{
    public string nameCard;
    public Sprite sprite;

    [Range(1, 100)]
    public int dropRate;
}
