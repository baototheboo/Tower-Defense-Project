using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    public int damage;
    private Rigidbody2D rb;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<EnemiesHealth>() != null)
        {
            //store the info of colliding in _enemyHealth
            EnemiesHealth _enemiesHealth = collision.gameObject.GetComponent<EnemiesHealth>();
            _enemiesHealth.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
