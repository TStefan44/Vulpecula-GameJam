using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerDash : MonoBehaviour
{
    public static PlayerDash instance;

    private bool canDash = true;
    private bool isDashing = false;
    public float dashingPower = 25f;
    public float dashingTime = 0.2f;
    public float dashingCooldown = 1f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private LayerMask platformLayer;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask endLayer;
    private float hitDashPower;
    //public Text dashType;
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        if (instance != null)
        {
            Debug.LogError("More than 1 dash instance");
        }
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.U))
        {
            //dashType.text = "Long Dash";
            UnityEngine.Debug.Log("U");
            dashingPower = 15f;
            dashingTime = 0.2f;
            dashingCooldown = 1f;
            tr.startColor = Color.red;
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            //dashType.text = "Normal Dash";
            UnityEngine.Debug.Log("I");
            dashingPower = 5f;
            dashingTime = 0.2f;
            dashingCooldown = 1f;
            tr.startColor = Color.blue;
        }


        if (isDashing)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

    }
    private IEnumerator Dash()
    {
        audioManager.PlaySFX(audioManager.dash);
        float horizontal = gameInput.getHorizontalMovement();
        checkPlatformCollision(horizontal);
        checkEnemyCollision(horizontal);
        float currentDashPower = Mathf.Min(hitDashPower, dashingPower);
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        //rb.velocity = new Vector2(10000, 0.01f);
        rb.position = new Vector2(rb.position.x + currentDashPower * horizontal, rb.position.y);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }


    private void checkEnemyCollision(float horizontal)
    {
        Vector2 dir = new Vector2(horizontal * dashingPower, 0f);
        bool enemyDamaged = false;

        RaycastHit2D[] platformHits = Physics2D.RaycastAll(rb.position, dir, Mathf.Abs(dashingPower * horizontal), enemyLayer | endLayer);

        foreach (RaycastHit2D hit in platformHits)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.takeDamage();
                    enemyDamaged = true;
                }
            } else if (hit.collider.CompareTag("Collectible"))
            {
                Debug.Log("ENd hit");
                EndCollision endCollision = hit.collider.GetComponent<EndCollision>();
                if (endCollision != null)
                {
                    endCollision.DestroyCrystal();
                }
            }
        } 

        if (enemyDamaged)
        {
            PlayerMovement.instance.activateIFrame();
        }
    }

    private void checkPlatformCollision(float horizontal)
    {
        Vector2 dir = new Vector2(horizontal * dashingPower, 0f);

        RaycastHit2D[] platformHits = Physics2D.RaycastAll(rb.position, dir, Mathf.Abs(dashingPower * horizontal), platformLayer);
        float minDistance = Mathf.Infinity;
        Vector2 closestHitPoint = Vector2.zero;

        foreach (RaycastHit2D hit in platformHits)
        {
            float distance = Vector2.Distance(rb.position, hit.point);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestHitPoint = hit.point;
            }
        }

        if (platformHits.Length > 0)
        {
            hitDashPower = minDistance;
        }
        else
        {
            hitDashPower = dashingPower;
        }
    }

    public bool PlayerIsDashing => isDashing;
}