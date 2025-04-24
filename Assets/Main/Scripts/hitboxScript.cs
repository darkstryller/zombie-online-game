using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitboxScript : MonoBehaviour
{
    [SerializeField] EnemyStats stats;
    [SerializeField] LayerMask targetlayer;
 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(((1 << collision.gameObject.layer) & targetlayer) != 0)
        {
            var health = collision.gameObject.GetComponent<HealthScript>();
            if (health != null)
            {
                health.TakeDamage(stats._damage);
            }
        }
            
        
    }
    
}
