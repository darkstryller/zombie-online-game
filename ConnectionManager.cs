using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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


}
