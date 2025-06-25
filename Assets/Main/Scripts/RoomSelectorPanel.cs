using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RoomSelectorPanel : MonoBehaviour
{
/*
    [SerializeField] private Transform contentTransform;
    [SerializeField] private VerticalLayoutGroup layoutGroup;
    [SerializeField] private RoomItemUI roomUIPrefab;

    private List<RoomItemUI> roomsUI = new List<RoomItemUI>;

    public void PopulateRoomsList()
    {
        ClearRoomsList();
        List<RoomInfo> allRooms = ConnectionManager.Instance.GetAllRooms();
        foreach (RoomInfo room in allRooms)
        {
            RoomItemUI roomUI = Instantiate(roomUIPrefab, contentTransform)
            roomsUI.SetUp(room.Name, HandleJoinRoomRequest);
            roomsUI.Add(roomUI);
        }
    }

    private void ClearRoomsList()
    {
        foreach (RoomItemUI room in roomsUI)
        {
            Destroy(room.gameObject);
        }
    }

    private void HandleJoinRoomRequest(string roomName)
    {
        ConnectionManager.Instance.JoinSelectedRoom(roomName);
    }
*/
}
