using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerGunSync : MonoBehaviourPun // este script es para que se puedan dar los rpc de las armas, es como un puente entre el gunholder y photon
{                                               // asi no se tiene que poner mas photon view a las armas, solo usa el photonview del player
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
