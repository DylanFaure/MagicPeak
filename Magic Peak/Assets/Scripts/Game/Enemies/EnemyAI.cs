using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    [SerializeField] private float xpGiven = 30f;
    [SerializeField] private int moneyGiven = 30;
    
    [Header("Settings")]
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private PlayerStats playerStats;

    private Transform player;
    private bool isChasing = false;

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        healthBar = this.transform.Find("Health").Find("Health Bar").GetComponent<HealthBar>();
        playerStats = player.GetComponent<PlayerStats>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        UpgradeStatEnemy();
        ChasePlayer();
        DestroyEnemy();
    }

    public float GetAttackDamageEnemy()
    {
        return attackDamage;
    }

    private void UpgradeStatEnemy()
    {
        if (playerStats.AbleToUpdateStatsEnemy())
        {
            maxHealth += 10;
            attackDamage += 5;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spell"))
        {
            if (collision.TryGetComponent<ProjectileSpell>(out ProjectileSpell projectileSpell))
            {
                TakeDamage(projectileSpell.damage * player.GetComponent<PlayerStats>().GetAttackPlayer());
                Debug.Log("Enemy took " + projectileSpell.damage * player.GetComponent<PlayerStats>().GetAttackPlayer() + " damage from " + projectileSpell.name);
                Destroy(collision.gameObject);
            }
            else if (collision.TryGetComponent<OnCursorSpell>(out OnCursorSpell aoeSpell))
            {
                TakeDamage(aoeSpell.damage * player.GetComponent<PlayerStats>().GetAttackPlayer());
                Debug.Log("Enemy took " + aoeSpell.damage * player.GetComponent<PlayerStats>().GetAttackPlayer() + " damage from " + aoeSpell.name);
            }
            else if (collision.TryGetComponent<LaserSpell>(out LaserSpell onPlayerSpell))
            {
                TakeDamage(onPlayerSpell.damage * player.GetComponent<PlayerStats>().GetAttackPlayer());
                Debug.Log("Enemy took " + onPlayerSpell.damage * player.GetComponent<PlayerStats>().GetAttackPlayer() + " damage from " + onPlayerSpell.name);
            }
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
            if (distanceToPlayer <= attackRange)
            {
                if (attackTimer <= 0f)
                {
                    Attack();
                    attackTimer = attackDelay;
                }
                else
                {
                    attackTimer -= Time.deltaTime;
                }
            }
            else
            {
                agent.SetDestination(player.position);
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
            playerStats.GainXp(xpGiven);
            playerStats.GainMoney(moneyGiven);
            Destroy(gameObject);
        }
    }
}
