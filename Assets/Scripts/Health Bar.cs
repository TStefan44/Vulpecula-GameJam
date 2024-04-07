using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private Image healthImage;
    public int maxHealth = 100;
    public int currentHealth;

    private void Awake()
    {
        GameObject HealthBar = GameObject.Find("HealthBar");
        Transform imageTransform = HealthBar.transform.Find("Health");
        healthImage = imageTransform.GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthImage.fillAmount = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Health = " + healthImage.fillAmount);
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth - damage > 0)
        {
            currentHealth -= damage;
        }
        else
        {
            currentHealth = 0;
        }

        healthImage.fillAmount = (float)currentHealth / 100f;
    }
    public void heal(int heal)
    {
        if (currentHealth + heal > maxHealth)
        {
            currentHealth += heal;
        }
        else 
        {
            currentHealth = maxHealth; 
        }

        healthImage.fillAmount = (float)currentHealth / 100f;
    }

}