using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitboxScript : MonoBehaviourPunCallbacks
{
    [SerializeField] EnemyStats stats;
    [SerializeField] LayerMask targetlayer;
    PhotonView view;

    private void Awake()
    {
        view = GetComponent<PhotonView>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!PhotonNetwork.IsMasterClient) return;
        
        if(((1 << collision.gameObject.layer) & targetlayer) != 0)
        {
            PhotonView targetView = collision.gameObject.GetComponent<PhotonView>();
            if (targetView != null)
            {
                targetView.RPC("TakeDamage", RpcTarget.AllBuffered, stats._damage);
            }
        }
    }
}