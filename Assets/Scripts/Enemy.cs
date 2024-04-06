using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Rigidbody2D rb;
    private PlayerMovement player;
    private float circleRadius = 10f;
    private float fielPlayerSee = 5f;
    private Vector3 startPosition;
    private Vector3 dirToPlayer;
    

    // Start is called before the first frame update
    void Start()
    {
        startPosition = this.transform.position;
        player = PlayerMovement.instance
    }

    // Update is called once per frame
    void Update()
    {
        if (detectPlayer())
        {
            Debug.Log("Player Detect");

            if (seePlayer())
            {
                Debug.Log("Enemy can see player");
            }
        }
    }

    private bool seePlayer()
    {
        bool seePlayer = false;
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, fielPlayerSee, dirToPlayer, playerMask | groundMask);
        if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            seePlayer = true;
        }
        return seePlayer;
    }

    private bool detectPlayer()
    {
        bool detectPlayer = false;
        Collider2D playerHit = Physics2D.OverlapCircle(this.transform.position, circleRadius, playerMask);
        if (playerHit)
        {
            detectPlayer = true;
            dirToPlayer = playerHit.transform.position - transform.position;
        }
        return detectPlayer;
    }

    private void goToPlayer()
    {

    }
}
