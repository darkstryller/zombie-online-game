using System;
using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(PhotonView))] // me aseguro que el objeto tenga el photon view
public class HealthScript : MonoBehaviourPun
{
    [Header("Health")]
    public int maxHealth = 100;

    [SerializeField] private int currentHealth;
    public int CurrentHealth => currentHealth;

    public event Action<int, int> OnHealthChanged;

    void Start()
    {
        currentHealth = maxHealth;

        if (photonView.IsMine) // solo el dueño le manda a todos para actualizar su vida
        {
            photonView.RPC(nameof(RPC_UpdateHealth), RpcTarget.All, currentHealth, maxHealth);
        }
    }
   
    public void TakeDamage(int damage)
    {
        if (!photonView.IsMine) return;  // si no esta sincronizado corta aca

        currentHealth = Mathf.Max(currentHealth - damage, 0); // x si acaso me aseguro que la vida nunca sea negativa (osea no baje de 0)
        photonView.RPC(nameof(RPC_UpdateHealth), RpcTarget.All, currentHealth, maxHealth);
    }

    public void ResetHealth() // metodo x si quiero reiniciar la vida
    {
        if (!photonView.IsMine) return;

        currentHealth = maxHealth;
        photonView.RPC(nameof(RPC_UpdateHealth), RpcTarget.AllBuffered, currentHealth, maxHealth);
    }

    public bool IsAlive()
    {
        return currentHealth > 0;
    }

    [PunRPC] // rpc para actualizar la vida con evento
    void RPC_UpdateHealth(int newCurrent, int newMax)
    {
        currentHealth = newCurrent;
        maxHealth = newMax;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }
}
