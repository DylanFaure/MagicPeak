using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth = 0f;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float attackDelay = 2f;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackTimer = 0f;
    [SerializeField] private float visionRange = 10f;
    
    [Header("Settings")]
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private PlayerStats playerStats;

    private Transform player;
    private bool isChasing = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        healthBar = this.transform.Find("Health").Find("Health Bar").GetComponent<HealthBar>();
        playerStats = player.GetComponent<PlayerStats>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        ChasePlayer();
        DestroyEnemy();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if enemy has been attacked by player. If yes, lose hp.
        if (collision.CompareTag("Player"))
        {
            TakeDamage(playerStats.GetAttackPlayer());
        }
    }

    private void ChasePlayer()
    {
        // Check the distance between player/enemy
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // If enemy can see player, keep hasing him
        if (distanceToPlayer <= visionRange)
        {
            isChasing = true;
        }

        if (isChasing)
        {
            // Check if player is in attack range
            if (distanceToPlayer <= attackRange)
            {
                // Timer for attacks at every desired time
                if (attackTimer <= 0f)
                {
                    Attack();
                    attackTimer = attackDelay;
                }
                else
                {
                    // Reset the attack timer
                    attackTimer -= Time.deltaTime;
                }
            }
            else
            {
                // Move the enemy to chase the player
                transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            }
        }
    }

    public void Attack()
    {
        player.GetComponent<PlayerStats>().TakeDamage(attackDamage);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    private void DestroyEnemy()
    {
        if (currentHealth <= 0)
        {
            // A chqnger pqr le gqin d'xp de chaque monstre
            playerStats.GainXp(30);
            Destroy(gameObject);
        }
    }
}
