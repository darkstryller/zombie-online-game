using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerGunFeedback : MonoBehaviour // este script es para que se puedan dar los rpc de las armas
{                                               // asi no se tiene que poner mas photon view a las armas, solo usan las del player
    [SerializeField] private GunHolderScript gunHolder;

    [PunRPC]
    public void RPC_ShowMuzzleFlash()
    {
        var activeGun = gunHolder.GetActiveGun();

        if (activeGun != null)
        {
            activeGun.ShowFlash();
        }
    }

    [PunRPC]
    public void RPC_PlayShootSound()
    {
        var activeGun = gunHolder.GetActiveGun();

        if (activeGun != null)
        {
            activeGun.PlayShootSound();
        }
    }

    [PunRPC]
    public void RPC_ChangeGun(int id)
    {
        gunHolder.ChangeGun(id);
    }

}
