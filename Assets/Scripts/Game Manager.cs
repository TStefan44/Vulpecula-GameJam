using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] private float playerSpeed = 10f;
    [SerializeField] private float playerJumpingPower = 20f;
    [SerializeField] private float playerFallFactor = 0.5f;
    [SerializeField] private float playerGroundCapsule_w = 0.2f;
    [SerializeField] private float playerCastRadius = 1f;
    [SerializeField] private bool playerCanDJump = true;
    [SerializeField] private int playerDamage = 50;
    [SerializeField] private int damageEnemyCollision = 10;
    [SerializeField] private int damageProjectile = 15;
    [SerializeField] private int healProjectile = 20;

    public static GameManager instance;


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than 1 game manager instance");
        }
        instance = this;
    }

    public void endGame()
    {
        int score = ScoreManager.instance.GetScore;
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.Save();


        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Start is called before the first frame update
    void Start()
    {
        playerCastRadius = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public float PlayerSpeed => playerSpeed;
    public float PlayerJumpingPower => playerJumpingPower;
    public float PlayerFallFactor => playerFallFactor;
    public float PlayerGroundCapsuleWidth => playerGroundCapsule_w;
    public bool PlayerCanDJump => playerCanDJump;
    public float PlayerCastRadius => playerCastRadius;
    public int DamageEnemyCollision => damageEnemyCollision;
    public int DamageProjectile => damageProjectile;
    public int HealProjectile => healProjectile;
    public int PlayerDamage => playerDamage;
}
