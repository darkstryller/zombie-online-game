using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RoomSelectionPanel : MonoBehaviour
{
    [SerializeField] private Transform contentTransform;
    [SerializeField] private VerticalLayoutGroup layoutGroup;
    [SerializeField] private RoomItemUI roomUIPrefab;

    private List<RoomItemUI> roomsUI = new List<RoomItemUI>();

    private void Start()
    {
        InvokeRepeating(nameof(PopulateRoomsList), 0f, 5f);
    }

    public void PopulateRoomsList()
    {
        ClearRoomsList();

        List<RoomInfo> allRooms = ConnectionManager.instance.GetAllRooms();
        foreach (RoomInfo room in allRooms)
        {
            RoomItemUI roomUI = Instantiate(roomUIPrefab, contentTransform);
            roomUI.Setup(room.Name, HandleJoinRoomRequest);
            roomsUI.Add(roomUI);
        }
    }


    private void ClearRoomsList()
    {
        foreach (RoomItemUI room in roomsUI)
        {
            Destroy(room.gameObject);
        }
        roomsUI.Clear();
    }

    private void HandleJoinRoomRequest (string roomName)
    {
        ConnectionManager.instance.JoinSelectedRoom(roomName);
    }
}
