using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuStarter : MonoBehaviourPunCallbacks
{
    [SerializeField] private InputField playerNameInput;
    [SerializeField] private Button connectButton;

    private const string nickNameKey = "PlayerNickname";

    void Start()
    {
        connectButton.onClick.AddListener(OnConnectButtonClicked);
    }

    void OnConnectButtonClicked()
    {
        //  PlayerPrefs.SetString(nickNameKey, playerNameInput.text); // guarda el nombre 

        /* PhotonNetwork.NickName = playerNameInput.text; // lo asigna
         PhotonNetwork.ConnectUsingSettings(); // conecta al servidor de Photon*/

        ConnectionManager.Instance.SetNickName(playerNameInput.text);
        ConnectionManager.Instance.ConnectToServer(LoadLobby); // callback, cuando se conecta al server de photon quiero que ......
    }


    private void LoadLobby() // que directamente cree una room y cargue la escena de lobby, en esta escena solo aparecen los nombres de los jugadres conectados y un boton play qie carga la escne adel juego para todos
    {
        PhotonNetwork.JoinRandomOrCreateRoom();
        SceneManager.LoadScene("Lobby");

        /*    public override void OnConnectedToMaster() // al conectarse al servidor de Photon crea la room muestra a los jugadores y sale el boton de play
            {
                Debug.Log("OnConnectedToMaster");
                PhotonNetwork.LoadLevel("test");  // en vez de cargar el nivel de juego carga la escena del lobby
            }
        */
    }
}