using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCollision : MonoBehaviour
{
    [SerializeField] private ExplodeEffect explodeEffect;
    private bool isDestroyed = false;
    static public EndCollision instance;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Debug.LogError("More than one EndCrystal");
        }
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDestroyed)
        {
            Debug.Log("End Game");
            ExplodeEffect effect = Instantiate(explodeEffect, transform.position, Quaternion.identity);
            effect.transform.localScale = Vector3.one * 5f;
            Destroy(gameObject);
        }
    }

    public void DestroyCrystal() {
        isDestroyed = true;
    }
}
