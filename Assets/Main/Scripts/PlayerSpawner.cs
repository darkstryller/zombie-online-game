using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerSpawner : MonoBehaviourPunCallbacks
{
    [SerializeField] private PhotonView playerPrefab;
    [SerializeField] private Transform[] spawnPoints;   // Debe tener al menos tantos puntos como MaxPlayers

    void Start()
    {
        if (!PhotonNetwork.InRoom) return;

        int index = PhotonNetwork.LocalPlayer.ActorNumber - 1;       // ActorNumber empieza en 1
        Vector3 spawnPos = spawnPoints[index % spawnPoints.Length].position;

        PhotonNetwork.Instantiate(playerPrefab.name, spawnPos, Quaternion.identity);
    }
}
