using System.Collections;
using System.Collections.Generic;
using Photon;
using Photon.Pun;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private PhotonView playerPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] public GameObject roomCam;
    [SerializeField] public GameObject login;
    [SerializeField] public GameObject loading;

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Debug.Log("Connecting...");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
        PhotonNetwork.JoinRandomOrCreateRoom();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("We are in the Lobby");
        PhotonNetwork.JoinOrCreateRoom("test", null, null);
    }

    public override void OnJoinedRoom()
    {
        roomCam.SetActive(false);
        login.SetActive(true);
        RespawnPlayer();
    }

    public void RespawnPlayer()
    {
        GameObject _player = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, spawnPoint.rotation);
        _player.GetComponent<PlayerMovement>().Look();
        print(playerPrefab.ViewID);
    }
}
