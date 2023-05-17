using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCursorSpell : MonoBehaviour
{
    public float damage;
    public float range;
    public float activeTime;
    public float chargeTime;
    public Vector3 target;
    private Collider2D col;

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
            Destroy(gameObject);
        }
    }

    private void EnableCollider()
    {
        col.enabled = true;
    }
}
