using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Mathematics;

public class Gun : MonoBehaviourPunCallbacks, IGun
{
    #region Variables
    [SerializeField] GunStats gunData;
    [SerializeField] Transform shootPoint;
    [SerializeField] private Text ammoCount;
    [SerializeField] private Text maxAmmoCount;

    private Vector2 mousePos;
    private Vector2 startPos;
    private MarketScript market;
    private int currentAmmo;
    private int maxAmmo;
    private int ammoClip;
    private int shotsFired;
    private bool IsReloading;
    private bool isShooting;
    private float nextFireTime;
#endregion

#region Metodos de Unity
    void Start()
    {
        market = FindObjectOfType<MarketScript>();
        ammoClip = gunData._clipAmmo;
        currentAmmo = ammoClip;
        maxAmmo = gunData._maxAmmo;
        ammoCount.text = currentAmmo.ToString();
        maxAmmoCount.text = maxAmmo.ToString();
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            startPos = shootPoint.position;

            if (gunData._IsAutomatic)
            {
                if (Input.GetKey(KeyCode.Mouse0) && Time.time >= nextFireTime && currentAmmo > 0)
                {
                    nextFireTime = Time.time + 1f / gunData._fireFate;
                    Shoot();
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Mouse0) && currentAmmo > 0)
                {
                    Shoot();
                }
            }

            if (maxAmmo > 0 && Input.GetKeyDown(KeyCode.R))
            {
                Reload();
            }

            ammoCount.text = currentAmmo.ToString();
            maxAmmoCount.text = maxAmmo.ToString();
        }
    }
    #endregion

#region Metodos
   
    public void Shoot()
    {
        if (market != null && !market._IsShooping && !IsReloading)
        {
            currentAmmo--;
            shotsFired++;
            RaycastHit2D hit = Physics2D.Raycast(startPos, (mousePos - startPos).normalized, gunData._range);
            Debug.DrawLine(startPos, startPos + (mousePos - startPos).normalized * gunData._range, Color.red, 0.1f);

            if (hit)
            {
                IDamageable damageable = hit.collider.GetComponent<IDamageable>();

                if (damageable != null)
                {
                    damageable.GetDamage(gunData._damage);
                    Debug.Log("<color=yellow>" + damageable + "</color>" + " Se comió "  + "<color=yellow>" + gunData._damage + "</color>" + " de daño");
                }
            }
        }
    }

    public void Reload()
    {
        if (IsReloading || maxAmmo <= 0 || currentAmmo == ammoClip)
        {
            return; // No recarga si ya está recargando, no hay balas, o el cargador está lleno
        }
        
        IsReloading = true;
    
        int ammoNeeded = ammoClip - currentAmmo; // Cuántas balas faltan para llenar el cargador
        int ammoToLoad = Mathf.Min(ammoNeeded, maxAmmo); // Carga solo lo que haya disponible

        currentAmmo += ammoToLoad; // Añadir balas al cargador
        maxAmmo -= ammoToLoad; // Restar de la reserva

        IsReloading = false; // Termina la recarga
    }

    public void GetAmmo()
    {
        maxAmmo += ammoClip;
    }

    public void FullReload()
    {
        currentAmmo = gunData._clipAmmo;
        maxAmmo = gunData._maxAmmo;
    }

   
    #endregion
}
