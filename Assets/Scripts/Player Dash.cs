using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDash : MonoBehaviour
{
    private bool canDash = true;
    private bool isDashing = false;
    public float dashingPower = 25f;
    public float dashingTime = 0.2f;
    public float dashingCooldown = 1f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private TrailRenderer tr;
    //public Text dashType;


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
        float horizontal = gameInput.getHorizontalMovement();
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        //rb.velocity = new Vector2(10000, 0.01f);
        if (rb.position.x + dashingPower * horizontal > 24.155)
        {
            rb.position = new Vector2(24.155f, rb.position.y);
        }
        else if (rb.position.x + dashingPower * horizontal < -23.905)
        {
            rb.position = new Vector2(-23.905f, rb.position.y);
        }
        else
            rb.position = new Vector2(rb.position.x + dashingPower * horizontal, rb.position.y);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;

    }
}