using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesHealth : MonoBehaviour
{
    public Animator anim;
    public float currentHealth;
    public float maxHealth;
    public bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        anim.SetTrigger("Hurt");

        if(currentHealth <= 0)
        {
            isDead = true;
            currentHealth = 0;
            anim.SetBool("isDead", true);
            Destroy(gameObject, 2f);
        }
    }
}
