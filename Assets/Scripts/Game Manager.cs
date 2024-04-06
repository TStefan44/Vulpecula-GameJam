using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private float playerSpeed = 8f;
    [SerializeField] private float playerJumpingPower = 20f;
    [SerializeField] private float playerFallFactor = 0.5f;
    [SerializeField] private float playerGroundCapsule_w = 0.2f;
    [SerializeField] private bool playerCanDJump = true;

    public static GameManager instance;


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than 1 game manager instance");
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
        
    }

    public float PlayerSpeed => playerSpeed;
    public float PlayerJumpingPower => playerJumpingPower;
    public float PlayerFallFactor => playerFallFactor;
    public float PlayerGroundCapsuleWidth => playerGroundCapsule_w;
    public bool PlayerCanDJump => playerCanDJump;
}
