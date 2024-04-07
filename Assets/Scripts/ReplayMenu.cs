using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReplayMenu : MonoBehaviour
{
    public Text scoreText;

    public void Start()
    {
        scoreText.text = "Score: " + PlayerPrefs.GetInt("Score").ToString();
    }
    public void ReplayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
