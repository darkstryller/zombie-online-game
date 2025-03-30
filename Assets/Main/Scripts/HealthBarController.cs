using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    [SerializeField] private Image bar;
    [SerializeField] private HealthScript health;

    void Update()
    {
        UpdateBar();
    }

    void UpdateBar()
    {
        bar.fillAmount = (float)health.GetCurrentHealth() / health.maxHealth; 
    }
}
