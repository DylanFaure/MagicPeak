using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemie : MonoBehaviour
{
    public float speed;
    public float visionRange;
    public float attackRange;
    public int attackDamage;
    public Transform player;
    
    private bool canSeePlayer = false;
    
    void Update()
    {
        if (CanSeePlayer())
        {
            if (IsPlayerInAttackRange())
            {
                AttackPlayer();
            }
            else
            {
                MoveTowardsPlayer();
            }
        }
    }
    
    bool CanSeePlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (player.position - transform.position).normalized, visionRange);
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            canSeePlayer = true;
        }
        else
        {
            canSeePlayer = false;
        }
        return canSeePlayer;
    }
    
    bool IsPlayerInAttackRange()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        Debug.Log("Player in Attack Range !");
        return distanceToPlayer <= attackRange;
    }
    
    void AttackPlayer()
    {
        // player.GetComponent<Player>().TakeDamage(attackDamage);
    }
    
    void MoveTowardsPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }
}
