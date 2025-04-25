using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;  

public class RoomList : MonoBehaviourPunCallbacks
{
    [Header("UI")] public Transform roomListParent;
    public GameObject roomListItemPrefab;

    private List<RoomInfo> cachedRoomList = new List<RoomInfo>();

    IEnumerator Start()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.Disconnect();
        }

        yield return new WaitUntil(() => !PhotonNetwork.IsConnected);


        PhotonNetwork.ConnectUsingSettings();
    }


    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        PhotonNetwork.JoinLobby();
        
    }


    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (cachedRoomList.Count == 0)
        {
            cachedRoomList = roomList;
        }
        else
        {
            foreach (var room in roomList)
            {
                for(int i = 0; 1< cachedRoomList.Count; i++)
                {
                    if (cachedRoomList[i].Name == room.Name)
                    {
                        List<RoomInfo> newList = cachedRoomList;

                        if (room.RemovedFromList)
                        {
                            newList.Remove(roomList[i]);
                        }
                        else
                        {
                            newList[i] = room;
                        }

                        cachedRoomList = newList;

                    }
                }
            }
        }

        updateUI();
    }



    void updateUI()
    {
        foreach (Transform roomitem in roomListParent)
        {
            Destroy(roomitem.gameObject);
        }

        foreach (var room in cachedRoomList)
        {
            GameObject roomitem = Instantiate(roomListItemPrefab, roomListParent);

            roomitem.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = room.Name;
            roomitem.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = room.PlayerCount + "/16";
        }

    }


}
