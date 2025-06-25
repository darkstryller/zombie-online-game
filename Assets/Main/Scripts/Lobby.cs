using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lobby : MonoBehaviourPunCallbacks // cuando se "conecta" que te lleve a donde se muestran lo nombres de jugadores 
{                                               // y que si hay min 2 y max 4 puedas darle play y cree la room para jugar
                                                //[SerializeField] private GameObject lobbyPanel;
                                                // [SerializeField] private GameObject playersPanel;
    [SerializeField] private Button createRoomButton;
    // [SerializeField] private Button playButton;
 //   [SerializeField] private InputField roomNameInput;
 //   [SerializeField] private TMP_Text roomNameText;
    [SerializeField] private TMP_Text[] playerNickNameTexts;
    [SerializeField] private GameManager manager;

    void Start()
    {
        createRoomButton.onClick.AddListener(OnCreateRoomButtonClicked);
        manager = FindAnyObjectByType<GameManager>();
        ConnectionManager.Instance.OnJoinRoom += UpdatePlayers;
        ConnectionManager.Instance.OnPlayerEnterRoom += UpdatePlayers;
        ConnectionManager.Instance.OnPlayerLeaveRoom += UpdatePlayers;
        /* ConnectionManager.Instance.JoinLobby();
           ConnectionManager.Instance.OnJoinRoom += HandleJoinedRoom;*/
    }
    void OnCreateRoomButtonClicked()
    {
        //   ConnectionManager.Instance.CreateRoom(roomNameInput.text);
        if (PhotonNetwork.IsMasterClient)
        {
            manager.StartGame();
            
        }
        Debug.Log("<color=green>Botón tocado</color>");
    }

    private void HandleJoinedRoom()
    {
        /* lobbyPanel.SetActive(false);
         playersPanel.SetActive(true);*/
   //     roomNameText.text = ConnectionManager.Instance.GetCurrentRoomName(); // le pongo el nombre de la room
    }

    private void UpdatePlayers()
    {
        Dictionary<int, Player> players = ConnectionManager.Instance.GetPlayersInRoom();
        Debug.Log("Players count" + players.Count);
        int index = 0;

        foreach (KeyValuePair<int, Player> player in players) // x cada jugador pongo el texto
        {
            playerNickNameTexts[index].text = player.Value.NickName;
            index++;
        }

        int count = ConnectionManager.Instance.GetPlayersInRoom().Count;
        createRoomButton.interactable = count >= 2 && count <= 4;

    }
   
    
    public void SetUp(Player player) // la idea es que vaya recorriendo la lista de jugadores en la room y vaya poniendo el nombre de cada uno
    {
//     playerNickNameText.text = player.Value.NickName;
        
    }
    
    private void ClearPlayers() { }

   
}