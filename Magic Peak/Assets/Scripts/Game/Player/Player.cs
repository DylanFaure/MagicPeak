using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed = 3.0f;
    public float spellCastDistance = 1.0f;
    public GameObject[] spells;
    public float[] cooldowns;
    private float[] currentCooldowns;
    public Image[] cooldownImages;
    public Image healthBar;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 mousePosition;
    private Vector2 direction;
    private Camera cam;

    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        currentCooldowns = new float[cooldowns.Length];
        currentHealth = maxHealth;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(10);
        }
    }


    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

        for (int i = 0; i < spells.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i) && currentCooldowns[i] <= 0)
            {
                CastSpell(i);
                currentCooldowns[i] = cooldowns[i];
            }

            currentCooldowns[i] -= Time.deltaTime;
            cooldownImages[i].fillAmount = Mathf.Clamp01(currentCooldowns[i] / cooldowns[i]);
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * speed * Time.fixedDeltaTime);
        Vector2 lookDirection = mousePosition - rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    void CastSpell(int spellIndex)
    {
        Vector2 spellSpawnPosition = rb.position + (mousePosition - rb.position).normalized * spellCastDistance;
        GameObject spell = Instantiate(spells[spellIndex], spellSpawnPosition, Quaternion.identity);
        // Set the spell's direction and other properties here
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.fillAmount = (float)currentHealth / (float)maxHealth;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Handle character death here
    }

}
