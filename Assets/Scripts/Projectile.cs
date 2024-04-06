using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    private Vector3 direction = Vector3.zero;
    private float speed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = direction * speed;
    }

    public void setDirection(Vector3 direction)
    {
        this.direction = direction; 
    }

    public void setSpeed(float speed) 
    { 
        this.speed = speed; 
    }
}
