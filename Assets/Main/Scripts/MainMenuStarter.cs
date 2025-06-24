using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuStarter : MonoBehaviourPunCallbacks
{
    [SerializeField] private InputField playerNameInput;
    [SerializeField] private Button conectButton;
    // [SerializeField] private Button startButton;
 
    private const string nickNameKey = "PlayerNickname"; // Nombre por defecto si no se ingresa uno
    private string nickName;

    void Start()
    {
        conectButton.onClick.AddListener(OnConectButtonClicked);
      // playerNameInput.onValueChanged.AddListener(OnPlayerNameChanged);
    }

    void OnConectButtonClicked() // al apretar el boton se "conecta" al lobby
    {
        PlayerPrefs.SetString(nickNameKey, playerNameInput.text); // guarda el nombre del jugador en PlayerPrefs
        PhotonNetwork.NickName = playerNameInput.text; // asigna el nombre al jugador
        PhotonNetwork.ConnectUsingSettings(); // conecta al servidor de Photon
    }

    public override void OnConnectedToMaster() // al conectarse al servidor de Photon crea la room muestra a los jugadores y sale el boton de play
    {
        Debug.Log("OnConnectedToMaster");
        PhotonNetwork.LoadLevel("test");
    }

    void OnPlayerNameChanged(string newName) // al poner nombre al conectarse el jugador se queda con ese nombre
    {
        if (string.IsNullOrEmpty(newName))
        {
            Debug.LogWarning("Player name is empty!");
        }
        else
        {
            Debug.Log($"Player name changed to: {newName}");
        }
    }
}

