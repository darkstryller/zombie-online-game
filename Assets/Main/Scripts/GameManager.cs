using System.Collections;
using System.Collections.Generic;
using Photon;
using Photon.Pun;
using Unity.Mathematics;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private PhotonView playerPrefab;
    [SerializeField] private Vector3 spawnPoint;

    void Start()
    {
       // PhotonNetwork.JoinRandomOrCreateRoom();
    }
    /*
        public override void OnConnectedToMaster()
        {
            Debug.Log("OnConnectedToMaster");
            PhotonNetwork.JoinRandomOrCreateRoom();
            PhotonNetwork.LoadLevel("test");
        }*/

    public override void OnJoinedRoom() // cuando se une a la room
    {
        GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint, quaternion.Euler(spawnPoint));
        player.GetComponent<PhotonView>().RPC("RPC_SetPlayerName", RpcTarget.AllBuffered, PlayerPrefs.GetString("PlayerNickname")); // rpc para poner nombre al jugador

      //  print(playerPrefab.ViewID);
        print(PhotonNetwork.NickName);
    }

    // para cambiar de oleada se puede usar un rpc
}
