using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private const float PLAYER_DISTANCE_SPAWN_PLATFORM = 200f;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject levelStart;
    [SerializeField] private GameObject level1;

    private Transform lastLevelPartTransform;
    private Vector3 PLATFORM_GENERATION_OFFSET = new Vector3(0, 3, 0);

    private Transform SpawnLevelPart()
    {
        lastLevelPartTransform = SpawnLevelPart(lastLevelPartTransform.position);
        return lastLevelPartTransform;
    }

    private Transform SpawnLevelPart(Vector3 spawnPosition)
    {
        GameObject levelPartTransform = Instantiate(level1, spawnPosition - PLATFORM_GENERATION_OFFSET, Quaternion.identity);
        return levelPartTransform.transform.GetChild(1);
    }

    private void Awake()
    {
        lastLevelPartTransform = SpawnLevelPart(levelStart.transform.Find("EndPosition").position);

        int startingLevelParts = 2;
        for (int i = 0; i < startingLevelParts; i++)
        {
            SpawnLevelPart();
        }
    }

    private void Update()
    {
        Debug.Log(lastLevelPartTransform.transform.GetChild(1).position);
        if (Vector3.Distance(player.transform.position, lastLevelPartTransform.transform.GetChild(1).position) < PLAYER_DISTANCE_SPAWN_PLATFORM)
        {
            SpawnLevelPart();
        }    
    }
}












