using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCursorSpell : MonoBehaviour
{
    private Collider2D col;

    [HideInInspector] public float damage;
    [HideInInspector] public float range;
    [HideInInspector] public float chargeTime;
    [HideInInspector] public Vector3 target;
    [HideInInspector] public float castOffset;

    [HideInInspector] public float activeTime;
    private float timer;
    private bool isCharging = false;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        transform.position = target;
    }

    private void Update()
    {
        if (chargeTime > 0 && !isCharging)
        {
            isCharging = true;
            Invoke("EnableCollider", chargeTime);
        }

        timer += Time.deltaTime;
        if (timer >= activeTime)
        {
            Destroy(gameObject);
        }
    }

    public void SetTarget(Vector3 mousePos)
    {
        target = mousePos - transform.position;

        transform.position = mousePos;
        if (castOffset > 0f)
        {
            transform.position += transform.up * castOffset;
        }
    }

    private void EnableCollider()
    {
        col.enabled = true;
    }
}
