using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvadeBar : MonoBehaviour
{
    [SerializeField] private Image bar;
   [SerializeField] private PlayerMovement player;

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            UpdateBar();
        }
    }

    void UpdateBar()
    {
        bar.fillAmount = player.GetevadeTime();
    }
}
