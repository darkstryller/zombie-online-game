using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject zombiePrefab;
    [SerializeField] private float maxTime;
    [SerializeField] private float currentTime;
    [SerializeField] private int maxWaves;
    [SerializeField] private int waveCount;
    private bool canSpawn = true;

    // "sistema de oleadas" super simple 

    void Start()
    {
        StartSpawn();
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        if (canSpawn)
        {
            if (currentTime >= maxTime)
            {
                Spawn();
                currentTime = 0;
            }
        }

        if (waveCount == maxWaves)
        {
            canSpawn = false;
        }
    }

    void StartSpawn()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Instantiate(zombiePrefab, spawnPoints[i].position, quaternion.identity);
        }
    }

    void Spawn()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Instantiate(zombiePrefab, spawnPoints[i].position, quaternion.identity);
        }

        waveCount++;
    }
}
