using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GunHolderScript : MonoBehaviour
{
    [SerializeField] GameObject[] guns;

    public void ChangeGun(int id)
    {
        for (int i = 0; i < guns.Length; i++)
        {
            if (guns[i] != null)
            {
                guns[i].SetActive(i == id); 
                Debug.Log("Gun id: " + " " + id);
            }
        }
    }

    public void LoadGun(int id)
    {
        for (int i = 0; i < guns.Length; i++)
        {
            if (guns[i] != null)
            {
                var gi = guns[i].GetComponent<Gun>();
                gi.FullReload();
            }
        }
    }
}