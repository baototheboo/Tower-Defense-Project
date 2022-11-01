using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public Projectiles bullet;
    public Transform[] firePoints;
    public float shotPerSeconds;
    private float nextShotTime;

    public int level;
    public int maxLevel;
    public int upgradeCost;
    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        
    }

    public void AddLevel()
    {
        if (upgradeCost <= GameManager.instance.currentGold && level < maxLevel)
        {
            level++;
            GameManager.instance.ReduceGold(upgradeCost);
            anim.SetTrigger("Upgrade");
            shotPerSeconds++;
            AudioManager.instance.PlaySFX(11);
        }
    }

    public void Shoot()
    {
        if (nextShotTime <= Time.time)
        {
            //loop through firepoints
            foreach (Transform firePoint in firePoints)
            {
                Projectiles _bulet = Instantiate(bullet, firePoint.position, firePoint.rotation);
            }
            nextShotTime = Time.time + (1 / shotPerSeconds);
        }
    }
}
