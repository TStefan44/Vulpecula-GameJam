using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Rigidbody2D rb;

    private int health = 100;

    private float circleRadius = 10f;
    private GameManager gameManager;

    protected PlayerMovement player;
    protected float enemySpeed = 5f;
    protected float rangeAttack = 5f;
    protected Vector3 dirToPlayer;

    protected bool takenDamage = false;
    protected float IFramdeDamge = 0.5f;
    protected float currentIFrame = 0f;

    protected void Awake()
    {
        gameManager = GameManager.instance;
    }

    protected void Start()
    {
        player = PlayerMovement.instance;
    }

    // Update is called once per frame
    protected void Update()
    {
        if (takenDamage)
        {
            if (currentIFrame < IFramdeDamge)
            {
                currentIFrame += Time.deltaTime;
            }
            else
            {
                currentIFrame = 0f;
                takenDamage = false;
            }
        }

        bool detect = detectPlayer();
        if (detect)
        {
            // Debug.Log("Player Detect");

            if (seePlayer())
            {
                // Debug.Log("Enemy can see player");
                goToPlayer();

                if (Vector3.Distance(player.transform.position, rb.position) <= rangeAttack)
                {
                    Attack();
                }
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
    }

    public void takeDamage()
    {

        if (takenDamage == false)
        {
            Debug.Log("Enemy damaged");
            takenDamage = true;
            health -= gameManager.PlayerDamage;

            if (health <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 2f, groundMask); ;
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

    virtual public void goToPlayer()
    {
        Vector2 direction;
        if (IsGrounded() == false)
        {
            direction = new Vector2(dirToPlayer.x, -1f).normalized;
        }
        else
        {
            direction = new Vector2(dirToPlayer.x, rb.velocity.y).normalized;
        }
        rb.velocity = new Vector2(direction.x * enemySpeed, rb.velocity.y);
    }

    virtual public void Attack() { }

}
