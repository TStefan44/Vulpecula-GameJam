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

    private bool playerWantsToJump;
    private bool playerJumped;
    private bool isGrounded;
    private bool dJumpCharge;
    private float horizontal;
    private bool wasHit = false;
    private float hitTimer = 1f;
    private float currentHitTimer = 0f;

    public static PlayerMovement instance;
    private GameManager gameManager;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than 1 player instance");
        }
        instance = this;
        gameManager = GameManager.instance;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        enemyInteract();

        if (wasHit)
        {
            currentHitTimer += Time.deltaTime;
            if (currentHitTimer >= hitTimer)
            {
                currentHitTimer = 0f;
                wasHit = false;
            }
        }

        horizontal = gameInput.getHorizontalMovement();
        playerWantsToJump = gameInput.PlayerWantsToJump();
        playerJumped = gameInput.PlayerJumped();
        isGrounded = IsGrounded();

        if (playerWantsToJump && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, gameManager.PlayerJumpingPower);
            dJumpCharge = true;
        }

        if (playerJumped && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * gameManager.PlayerFallFactor);
        }

        if (gameManager.PlayerCanDJump && !isGrounded && playerWantsToJump)
        {
            if (dJumpCharge)
            {
                rb.velocity = new Vector2(rb.velocity.x, gameManager.PlayerJumpingPower);
                dJumpCharge = false;
            }
        }
    }

    private void enemyInteract()
    {
        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        Vector2 direction = new Vector2(horizontal, 0).normalized;
        RaycastHit2D hit = new RaycastHit2D();

        hit = Physics2D.CircleCast(position, gameManager.PlayerCastRadius, direction, 0f, enemyLayer);
        if (hit && !wasHit) {
            wasHit = true;
            Debug.Log("Player was hit");
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
}
