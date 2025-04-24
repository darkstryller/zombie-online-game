using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemUI : MonoBehaviour
{
    private PlayerItemUI player;

    ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable();

    private const string PLAYER_AVATAR_PROP = "player_avatar";

    public void SetUp(Player player)
    {
        this.player = player;
        playerNickName.text = player.NickName;
        UpdatePlayerAvatar();
    }

    private void Update()
    {
        if(this player != PhotonNetwork.LocalPlayer)
        {
            return null;
        }
    }

}
