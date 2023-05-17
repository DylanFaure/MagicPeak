using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCursorSpell : MonoBehaviour
{
    private Collider2D col;

    [HideInInspector] public float damage;
    [HideInInspector] public float range;
    [HideInInspector] public float activeTime;
    [HideInInspector] public float chargeTime;
    [HideInInspector] public Vector3 target;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        if (chargeTime > 0f)
        {
            col.enabled = false;
            Invoke("EnableCollider", chargeTime);
        }
        transform.position = target;
        Destroy(gameObject, activeTime);
    }

    public void SetTarget(Vector3 mousePos)
    {
        target = mousePos - transform.position;

        transform.position = mousePos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyAI>().TakeDamage(damage);
        }
    }

    private void EnableCollider()
    {
        col.enabled = true;
    }
}
