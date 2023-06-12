using UnityEngine;

public class ProjectileSpell : MonoBehaviour
{
    private Rigidbody2D rb;

    [HideInInspector] public float speed;
    [HideInInspector] public float damage;
    [HideInInspector] public float range;
    [HideInInspector] public float rotationOffset;
    [HideInInspector] public Vector3 direction;

    private float distanceTraveled = 0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(direction.x, direction.y).normalized * speed * Time.fixedDeltaTime;
        distanceTraveled += speed * Time.fixedDeltaTime;
        if (distanceTraveled >= range)
        {
            Destroy(gameObject);
        }
    }

    public void SetTarget(Vector3 mousePos)
    {
        direction = mousePos - transform.position;

        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyAI>().TakeDamage(damage);
            Destroy(gameObject);
        } else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
