using System.Collections;
using System.Collections.Generic;
using Photon;
using Photon.Pun;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private PhotonView playerPrefab;
    [SerializeField] private Transform spawnPoint;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
        PhotonNetwork.JoinRandomOrCreateRoom();
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, spawnPoint.rotation);
        print(playerPrefab.ViewID);
      //  print(PhotonNetwork.NickName);
    }

}
