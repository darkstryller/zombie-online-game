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

    void Start()
    {
        connectButton.onClick.AddListener(OnConnectButtonClicked);
    }

    void OnConnectButtonClicked()
    {
        if (string.IsNullOrEmpty(playerNameInput.text))
        {
            Debug.LogWarning("El nombre de jugador no puede estar vacío.");
            return; // salir del método si está vacío
        }

        ConnectionManager.Instance.SetNickName(playerNameInput.text);
        ConnectionManager.Instance.ConnectToServer(LoadLobby); // callback, cuando se conecta al server de photon quiero que ...... en este caso cree una room y cargue el lobby
    }

    private void LoadLobby() 
    {
        PhotonNetwork.JoinRandomOrCreateRoom();
        SceneManager.LoadScene("Lobby");
    }
}