using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpell : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public float damage;
    public float range;
    public Vector3 direction;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, range);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(direction.x, direction.y).normalized * speed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyAI>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    public void SetTarget(Vector3 mousePos)
    {
        direction = mousePos - transform.position;

        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + 90f);
    }
}
