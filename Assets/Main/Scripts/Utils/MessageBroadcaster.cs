using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class MessageBroadcaster : MonoBehaviourPun
{
    public static MessageBroadcaster Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void BroadcastMessageToAll(string msg)
    {
        photonView.RPC(nameof(RPC_ShowMessage), RpcTarget.All, msg);
    }

    [PunRPC]
    private void RPC_ShowMessage(string msg)
    {
        MessageDisplay.Instance?.AddMessage(msg);
    }
}

