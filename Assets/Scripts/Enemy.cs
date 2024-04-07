using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] protected Animator animator;
    [SerializeField] private ExplodeEffect explodeEffect;

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

    protected AudioManager audioManager;

    protected void Awake()
    {
        gameManager = GameManager.instance;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    protected void Start()
    {
        player = PlayerMovement.instance;
    }

    // Update is called once per frame
    protected void Update()
    {
        if (dirToPlayer.x < 0)
        {
            transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);
        }

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
                animator.SetBool("isHit", false);
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
                animator.SetBool("isWalking", false);
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
            animator.SetBool("isHit", true);

            if (health <= 0f)
            {
                Destroy(gameObject);
                Instantiate(explodeEffect, rb.position, Quaternion.identity);
                audioManager.PlaySFX(audioManager.enemyDeath);
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
        animator.SetBool("isWalking", true);
    }

    virtual public void Attack() {

    }

}
