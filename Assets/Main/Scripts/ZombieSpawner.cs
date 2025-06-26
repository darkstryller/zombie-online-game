using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Photon.Pun;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject zombiePrefab;
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private float maxTime;
    [SerializeField] private float currentTime;
    [SerializeField] private int maxWaves;
    [SerializeField] private int waveCount;
    private bool canSpawn = true;

    // "sistema de oleadas" super simple 

    void Start()
    {
       // Spawn();
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

    void Spawn()
    {
        if(PhotonNetwork.IsMasterClient)
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            PhotonNetwork.Instantiate("zombie", spawnPoints[i].position, quaternion.identity);
            //Instantiate(zombiePrefab, spawnPoints[i].position, quaternion.identity);
        }

        waveCount++;

        if (waveCount % 5 == 0)
        {
            SpawnBoss();
        }
    }

    void SpawnBoss()
    {
        int randomIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
        Vector3 bossSpawnPos = spawnPoints[randomIndex].position;

        PhotonNetwork.Instantiate("boss", bossSpawnPos, quaternion.identity);
    }
}
