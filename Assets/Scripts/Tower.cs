using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public Projectiles bullet;
    public Transform[] firePoints;
    public float shotPerSeconds;
    private float nextShotTime;

    private void Update()
    {
        
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
