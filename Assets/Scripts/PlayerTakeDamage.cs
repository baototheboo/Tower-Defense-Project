using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakeDamage : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<EnemiesHealth>()!= null)
        {
            GameManager.instance.PlayerGetDamage() ;
            EnemiesHealth _enemiesHealth = other.gameObject.GetComponent<EnemiesHealth>();
            _enemiesHealth.Destroy();

           

        }
    }
}
