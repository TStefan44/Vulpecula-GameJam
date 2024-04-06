using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float Speed = 3f;
    [SerializeField] private float xOffset = 10f;
    [SerializeField] private float yOffset = 3f;
    [SerializeField] private Transform target;
    

    void Update()
    {
        Vector3 newPost = new Vector3(target.position.x + xOffset, target.position.y + yOffset, -10f);
        transform.position = Vector3.Slerp(transform.position, newPost, Speed * Time.deltaTime);
    }
}
