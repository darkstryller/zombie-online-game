using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Photon.Pun;
using UnityEngine;

public class ConnectionManager : Singleton<ConnectionManager>
{
    public PhotonPUNConnectionManager photonManager;

    private Action OnConnectedToServer;

    public Action OnJoinedRoom;

    public Action OnPlayerEnteredRoom;
    public Action OnPlayerLeftRoom;

    private List<RoomInfo> rooms = new List<RoomInfo>();

    private void Start()
    {
        photonManager.Init(HandleJoinedRoom,
            HandleRoomCreated,
            HandleNewPlayerInRoom,
            HandlePlayerLeftRoom);
    }

    public void SetNickName(string nickname)
    { 
        photonManager.SetNickName(nickname);
    }

    public void ConnectToServer(Action connectionCallback = null)
    {
        photonManager.ConnectToServer(HandleConnectionToServer);
        OnConnectedToServer = connectionCallback;
    }

    private void HandleConnectionToServer()
    {
        OnConnectedToServerServer?.Invoke();
    }

    public Dictionary<int, Player> GetPlayersInRoom()
    {
        return photonManager.GetPlayersInRoom();
    }

    public void JoinLobby()
    {
        photonManager.JoinLobby(); 
    }

    private void HandleJoinedRoom()
    {
        OnJoinedRoom?.Invoke();
    }

    public void JoinSelectedRoom(string roomName)
    {
        photonManager.JoinRoom(roomName);
    }

    public void CreateRooms(string roomName)
    {
        photonManager.CreateRoom(roomName);
    }

    private void HandleRoomCreated(List<RoomInfo> rooms)
    {
        this.rooms = rooms;
    }

    public List<RoomInfo> GetAllRooms()
    {
        return rooms;
    }

}
