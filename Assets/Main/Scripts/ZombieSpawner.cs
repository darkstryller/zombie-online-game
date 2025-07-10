using Photon.Pun;
using UnityEngine;
using Unity.Mathematics;
using System.Collections.Generic;
using TMPro;

public class ZombieSpawner : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private int maxWaves = 5;

    [Header("Configuración de zombis por ronda")]
    [SerializeField] private int baseZombies = 5;        // Cantidad base en ronda 1
    [SerializeField] private int zombiesPerRound = 2;    // Cantidad que se suma cada ronda extra

    [SerializeField] private TMP_Text waveText;
    [SerializeField] private TMP_Text aliveText;

    private int currentWave = 0;
    private int zombiesAlive;

    private readonly List<int> ids = new();

    void Start()
    {
        if (PhotonNetwork.IsMasterClient) StartNextWave();
    }

    void StartNextWave()
    {
        currentWave++;
        if (currentWave > maxWaves) return;

        bool bossWave = currentWave % 5 == 0;
        int amount;

        if (bossWave)
        {
            amount = 1;
        }
        else
        {
            amount = baseZombies + (currentWave - 1) * zombiesPerRound;
        }

        zombiesAlive = amount;

        for (int i = 0; i < amount; i++)
        {
            string prefab = bossWave ? "boss" : "zombie";
            Vector3 pos = spawnPoints[i % spawnPoints.Length].position;

            GameObject go = PhotonNetwork.Instantiate(prefab, pos, quaternion.identity);
            ids.Add(go.GetComponent<PhotonView>().ViewID);
        }

        photonView.RPC(nameof(RPC_SetWaveUI), RpcTarget.AllBuffered, currentWave, zombiesAlive);
    }

    public void OnZombieDied(int viewID)
    {
        if (!PhotonNetwork.IsMasterClient) return;

        zombiesAlive--;
        ids.Remove(viewID);

        photonView.RPC(nameof(RPC_SetAliveUI), RpcTarget.All, zombiesAlive);

        if (zombiesAlive <= 0)
        {
            StartNextWave();
        }
    }

    [PunRPC]
    void RPC_SetWaveUI(int wave, int alive)
    {
        waveText.text = $"Round {wave}";
        aliveText.text = alive.ToString();
    }

    [PunRPC]
    void RPC_SetAliveUI(int alive)
    {
        aliveText.text = alive.ToString();
    }
}
