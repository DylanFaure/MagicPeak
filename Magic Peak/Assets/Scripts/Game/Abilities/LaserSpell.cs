using UnityEngine;

public class LaserSpell : MonoBehaviour
{
    private Collider2D col;

    [HideInInspector] public float damage;
    [HideInInspector] public float chargeTime;
    [HideInInspector] public float activeTime;
    [HideInInspector] public float range;
    [HideInInspector] public Vector3 direction;

    private float timer;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        if (chargeTime > 0f)
        {
            col.enabled = false;
            Invoke("EnableCollider", chargeTime);
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= activeTime)
        {
            Destroy(gameObject);
        }
    }

    public void SetTarget(Vector3 mousePos)
    {
        direction = mousePos - transform.position;

        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);

        // laser sprite pivot is at the center, so we need to move it to the end of the laser sprite
        transform.position += direction.normalized * range / 2f;
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
