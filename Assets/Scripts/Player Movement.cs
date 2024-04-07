using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameInput gameInput;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask projectileLayer;
    [SerializeField] private Health health;
    [SerializeField] private Animator animator;

    private bool playerWantsToJump;
    private bool playerJumped;
    private bool isGrounded;
    private bool dJumpCharge;
    private float horizontal;
    private bool iFrame = false;
    private float hitTimer = 1f;
    private float currentHitTimer = 0f;

    public static PlayerMovement instance;
    private GameManager gameManager;
    private PlayerDash dashInstace;

    private AudioManager audioManager;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than 1 player instance");
        }
        instance = this;
        gameManager = GameManager.instance;
        dashInstace = PlayerDash.instance;

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        enemyInteract();
        projectileInteract();

        if (iFrame)
        {
            currentHitTimer += Time.deltaTime;
            if (currentHitTimer >= hitTimer)
            {
                currentHitTimer = 0f;
                iFrame = false;
            }
        }

        horizontal = gameInput.getHorizontalMovement();

        if (horizontal != 0)
        {
            animator.SetBool("isRun", true);
            if (horizontal < 0)
            {
                transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);
            }
        }
        else
        {
            animator.SetBool("isRun", false);
        }

        playerWantsToJump = gameInput.PlayerWantsToJump();
        playerJumped = gameInput.PlayerJumped();
        isGrounded = IsGrounded();

        if (isGrounded)
        {
            animator.SetBool("isJumping", false);
        }

        if (playerWantsToJump && isGrounded)
        {
            animator.SetBool("isJumping", true);
            rb.velocity = new Vector2(rb.velocity.x, gameManager.PlayerJumpingPower);
            dJumpCharge = true;
        }

        if (playerJumped && rb.velocity.y > 0f)
        {
            animator.SetBool("isJumping", true);
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * gameManager.PlayerFallFactor);
        }

        if (gameManager.PlayerCanDJump && !isGrounded && playerWantsToJump)
        {
            if (dJumpCharge)
            {
                animator.SetBool("isJumping", true);
                rb.velocity = new Vector2(rb.velocity.x, gameManager.PlayerJumpingPower);
                dJumpCharge = false;
            }
        }
    }

    private void projectileInteract()
    {
        Vector2 position = new Vector2(rb.position.x, rb.position.y);
        Vector2 direction = new Vector2(horizontal, 0).normalized;
        Collider2D hit = new Collider2D();

        hit = Physics2D.OverlapCircle(position, gameManager.PlayerCastRadius, projectileLayer);
        if (hit && !iFrame)
        {
            Destroy(hit.transform.gameObject);
            if (dashInstace.PlayerIsDashing)
            {
                Debug.Log("Player destroyed the Projectile");
                health.heal(gameManager.HealProjectile);
            }
            else
            {
                iFrame = true;
                Debug.Log("Player was hit by Projectile");
                health.TakeDamage(gameManager.DamageProjectile);
                ScoreManager.instance.LoseScoreDamage();
                audioManager.PlaySFX(audioManager.damageTaken);
            }
        }
    }

    private void enemyInteract()
    {
        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        Vector2 direction = new Vector2(horizontal, 0).normalized;
        RaycastHit2D hit = new RaycastHit2D();

        hit = Physics2D.CircleCast(position, gameManager.PlayerCastRadius, direction, 0f, enemyLayer);
        if (hit && !iFrame) {
            if (!dashInstace.PlayerIsDashing)
            {
                iFrame = true;
                Debug.Log("Player was hit");
                health.TakeDamage(gameManager.DamageEnemyCollision);
                audioManager.PlaySFX(audioManager.damageTaken);
                ScoreManager.instance.LoseScoreDamage();
            }
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * gameManager.PlayerSpeed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, gameManager.PlayerGroundCapsuleWidth, groundLayer);
    }

    public void activateIFrame()
    {
        iFrame = true;
    }
}
