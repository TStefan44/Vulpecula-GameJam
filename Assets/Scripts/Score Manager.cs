using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
     private Text scoreText;

    private int currentScore;
    private int timeScore;
    private float timer;

    // Start is called before the first frame update
    void Awake()
    {
        GameObject score = GameObject.Find("Score");
        score.Text = currentScore.ToString() + " points";
    }

    void Start()
    {
        currentScore = 0;
        timeScore = 300;
    }

    void Update()
    {

        timer += Time.deltaTime;
        if (timer >= 1f) {
            timeScore -= 1;
            timer = 0f;
                }
    }

    public void LoseScoreDamage(int damage)
    {
        if (damage > 0)
            currentScore -= 5;
    }

    

}
