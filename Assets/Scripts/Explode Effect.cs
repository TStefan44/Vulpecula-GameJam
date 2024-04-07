using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeEffect : MonoBehaviour
{
    [SerializeField] private float timeLive = 1f;
    private float currentTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > timeLive)
        {
            Destroy(gameObject);
        }
    }
}
