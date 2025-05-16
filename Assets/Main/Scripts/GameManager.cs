using System.Collections;
using System.Collections.Generic;
using Photon;
using Photon.Pun;
using Unity.Mathematics;
using UnityEditor.SearchService;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private PhotonView playerPrefab;
    [SerializeField] private Vector3 spawnPoint;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
        PhotonNetwork.JoinRandomOrCreateRoom();
        PhotonNetwork.LoadLevel("test");
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint, quaternion.Euler(spawnPoint));
        print(playerPrefab.ViewID);
        
      //  print(PhotonNetwork.NickName);
    }

}
