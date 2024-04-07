using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;

    private int currentScore;
    private int timeScore;
    private float timer;

    public static ScoreManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        scoreText.text = "Score: " + 300.ToString();
        instance = this;
    }

    void Start()
    {
        currentScore = 0;
        timeScore = 300;
    }

    void Update()
    {

        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            timeScore -= 1;
            timer = 0f;
        }

        scoreText.text = "Score: " + timeScore.ToString();
    }

    public void LoseScoreDamage()
    {
            currentScore -= 5;
    }

    public void GainScore(int value) {
        currentScore += value; }

    

}
