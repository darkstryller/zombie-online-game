using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonPUNConnectionManager : MonoBehaviourPunCallbacks
{
    public Action OnConnectedToServer;
    public Action OnJoinedRoomEvent;
    public Action<List<RoomInfo>> OnNewRoomCreated;

    public Action OnPlayerEnteredRoomEvent;
    public Action OnPlayerLeftRoomEvent;

    public void Init(Action onJoinRoom, Action<List<RoomInfo>> onRoomCreated,
        Action onPlayerEnterRoomCallback,Action onPlayerLeftCallback)
    {
        OnJoinedRoomEvent += onJoinRoom;
        OnNewRoomCreated += onRoomCreated;

        OnPlayerEnteredRoomEvent += onPlayerEnterRoomCallback;
        OnPlayerLeftRoomEvent += onPlayerLeftCallback;
    }

    public void SetNickname(string nickname)
    {
        PhotonNetwork.NickName = nickname;
    }

    public void ConnectToServer(Action onConnect = null)
    {
        PhotonNetwork.ConnectUsingSettings();
        OnConnectedToServer += onConnect;
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
        OnConnectedToServer?.Invoke();
      
    }

    public void JoinLobby()
    {
        PhotonNetwork.JoinLobby();
    }

    public void CreateRoom(string roomName)
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        print("Rooms amount" + roomList.Count);

        OnNewRoomCreated?.Invoke(roomList);
    }

    public Dictionary<int, Player> GetPlayersInRoom()
    {
        return PhotonNetwork.CurrentRoom.Players;
    }

}
