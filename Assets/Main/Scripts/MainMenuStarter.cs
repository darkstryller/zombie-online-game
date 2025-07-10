using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuStarter : MonoBehaviourPunCallbacks
{
    public static bool hasRequestedJoinRoom = false; // 🔹 STATIC para que persista al cambiar escenas

    [SerializeField] private InputField playerNameInput;
    [SerializeField] private Button connectButton;

    void Awake()
    {
        Debug.Log($"[MainMenuStarter] Awake. hasRequestedJoinRoom = {hasRequestedJoinRoom}");
    }

    void Start()
    {
        PhotonNetwork.IsMessageQueueRunning = true; 
        hasRequestedJoinRoom = false;

        PhotonNetwork.NickName = "";
        playerNameInput.text = "";

        connectButton.onClick.AddListener(OnConnectButtonClicked);
    }

    void OnConnectButtonClicked()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        string playerName = playerNameInput.text.Trim();

        if (string.IsNullOrEmpty(playerName)) // chequeo que no pueda concectarse sin nombre
        {
            Debug.LogWarning("sin nickname no entras.");
            return;
        }


        if (!PhotonNetwork.IsConnected)
        {
            ConnectionManager.Instance.SetNickName(playerNameInput.text);
            ConnectionManager.Instance.ConnectToServer(() =>
            {
                hasRequestedJoinRoom = true;
                PhotonNetwork.JoinRandomOrCreateRoom();
            });
        }
        else
        {
            // Ya conectado, PERO igual seteamos el nombre antes de unirse
            ConnectionManager.Instance.SetNickName(playerNameInput.text);
            hasRequestedJoinRoom = true;
            PhotonNetwork.JoinRandomOrCreateRoom();
        }
    }

    public override void OnJoinedRoom()
    {
        Debug.Log($"[MainMenuStarter] OnJoinedRoom. Flag = {hasRequestedJoinRoom}");

        if (hasRequestedJoinRoom)
        {
            hasRequestedJoinRoom = false;
            SceneManager.LoadScene("Lobby");
        }
        else
        {
            Debug.LogWarning("Se entró a una Room sin haberlo pedido. No hago nada.");
        }
    }
}
