using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lobby : MonoBehaviourPunCallbacks // cuando se "conecta" que te lleve a donde se muestran lo nombres de jugadores 
{                                               // y que si hay min 2 y max 4 puedas darle play y cree la room para jugar
    [SerializeField] private GameObject lobbyPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private Button startGameButton;

    void Start()
    {
        startGameButton.onClick.AddListener(OnStartGameButtonClicked);
    }

    void OnStartGameButtonClicked()
    {
        PhotonNetwork.LoadLevel("test");
    }

}
