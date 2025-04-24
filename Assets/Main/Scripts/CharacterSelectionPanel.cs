using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using Photon.Voice.PUN.UtilityScripts;
using UnityEngine;

public class CharacterSelectionPanel : MonoBehaviour
{
    [SerializeField] private PlayerItemUI playerPrefab;
    [SerializeField] private Transform contentTransform;

    private List<PlayerItemUI> playersUI = new List<PlayerItemUI> ();

    private void Start()
    {
        ConnectionManager.instance.OnJoinedRoom += UdpatePlayers;
        ConnectionManager.instance.OnPlayerLeftRoom += UpdatePlayers;
        ConnectionManager.instance.OnPlayerEnteredRoom += UpdatePlayers;

        UpdatePlayers();
    }

    private void UpdatePlayers()
    {
        ClearPlayers();

        Dictionary<int, Player> players = ConnectionManager.instance.GetPlayersInRoom();
        print("Players count: " + players.Count);

        foreach (KeyValuePair<int,Player> palyer in players)
        {
            PlayerItemUI playerUI = Instantiate(playerPrefab, contentTransform);
            playerUI.SetUp(player.Value);
            playersUI.Add(playerUI);
        }
    }

}
