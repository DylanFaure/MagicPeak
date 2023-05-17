using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfcastSpell : MonoBehaviour
{
    [HideInInspector] public float activeTime;

    private void Awake()
    {
        Destroy(gameObject, activeTime);
    }
}
