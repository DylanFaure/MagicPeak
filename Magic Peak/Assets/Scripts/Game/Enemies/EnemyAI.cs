using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float attackDistance = 5f;
    public float moveSpeed = 2f;
    public float attackDelay = 2f;

    private GameObject player;
    private float timeSinceLastAttack;
    private bool isChasing = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        timeSinceLastAttack = attackDelay;
    }

    void Update()
    {
        ChasePlayer();
    }

    private void ChasePlayer()
    {
        if (player != null)
        {
            Vector3 playerDirection = player.transform.position - transform.position;
            float distanceToPlayer = playerDirection.magnitude;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, playerDirection, distanceToPlayer);

            if (isChasing)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
            }
            if (distanceToPlayer <= attackDistance && hit.collider != null && hit.collider.tag == "Player")
            {
                isChasing = true;
                timeSinceLastAttack += Time.deltaTime;
                if (timeSinceLastAttack >= attackDelay)
                {
                    Attack();
                    timeSinceLastAttack = 0f;
                }
            }
            
        }
    }

    void Attack()
    {
        Debug.Log("Enemy attacks!");
    }
}
