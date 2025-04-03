using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject zombiePrefab;

    void Start()
    {
        Spawn();
    }

    void Spawn()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Instantiate(zombiePrefab, spawnPoints[i].position, quaternion.identity);
        }
    }
}
