using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Rigidbody2D rb;

    private float enemySpeed = 5f;
    private float circleRadius = 10f;

    private Vector3 dirToPlayer;

    // Update is called once per frame
    void Update()
    {
        bool detect = detectPlayer();
        if (detect)
        {
            Debug.Log("Player Detect");

            if (seePlayer())
            {
                Debug.Log("Enemy can see player");
                goToPlayer();
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
    }

    private bool seePlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(rb.position, dirToPlayer, circleRadius, playerMask | groundMask);
        if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            return true;
        }
        return false;
    }

    private bool detectPlayer()
    {
        Collider2D playerHit = Physics2D.OverlapCircle(rb.position, circleRadius, playerMask);
        if (playerHit)
        {

            dirToPlayer = playerHit.transform.position - transform.position;
            return true;
        }
        dirToPlayer = Vector3.zero;
        return false;
    }

    private void goToPlayer()
    {
        Vector2 direction = new Vector2(dirToPlayer.x, 0).normalized;
        rb.velocity = direction * enemySpeed;
    }

}
