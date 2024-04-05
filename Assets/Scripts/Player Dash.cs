using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{   
    private bool canDash=true;
    private bool isDashing=false;
    public float dashingPower=25f;
    public float dashingTime=0.2f;
    public float dashingCooldown=1f; 
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private TrailRenderer tr;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if(Input.GetKeyDown(KeyCode.U))
        {   
            Console.WriteLine("U");
            dashingPower=50f;
            dashingTime=0.5f;
            dashingCooldown=2f;
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
           Console.WriteLine("I");
            dashingPower = 25f;
            dashingTime = 0.2f;
            dashingCooldown = 1f;
        }


        if (isDashing)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
        
    }
    private IEnumerator Dash()
    {
        canDash=false;
        isDashing=true; 
        float originalGravity=rb.gravityScale;
        rb.gravityScale= 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting=true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting=false;
        rb.gravityScale=originalGravity;
        isDashing=false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash=true;

    }   
}
