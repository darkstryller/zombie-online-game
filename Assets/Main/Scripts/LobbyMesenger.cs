using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
public static class LobbyMesenger
{
   public static Dictionary<PlayerMovement, string> Usernames = new Dictionary<PlayerMovement, string>();
   
   public static void PlayerLeftMessage(string player)
    {
        Debug.Log("leave message");
        foreach (var item in Usernames)
        {
            item.Key.LobbyMesage(player + " left the room");
        }
    }
    public static void PlayerEnterMessage(string player)
    {
        foreach (var item in Usernames)
        {
            item.Key.LobbyMesage(player + " entered the room");
        }
    }
    public static void PlayerDeadMessage(string player)
    {
        foreach (var item in Usernames)
        {
            item.Key.LobbyMesage(player + " is dead");
        }
    }
}
