using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int target = 0;
    public Transform exitPoint;
    public Transform[] wayPoints;
    public float navTimeUpdate;
    public float currentNavTime;
    private Transform enemy;
    //private EnemyHealth enemyHealth;
    // Start is called before the first frame update
    void Start()
    {
        //enemyHealth = GetComponent<EnemyHealth>();
        enemy = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (enemyHealth.isDead != true)
        //{
            if (wayPoints != null)
            {
                currentNavTime += Time.deltaTime;

                if (currentNavTime > navTimeUpdate)
                {
                    if (target < wayPoints.Length)
                    {
                        
                        if(enemy.position.x > wayPoints[target].position.x)
                        {
                            enemy.rotation = Quaternion.Euler(0, 180, 0); 
                            enemy.position = Vector2.MoveTowards(enemy.position, wayPoints[target].position, currentNavTime);
                        } 
                        else
                        {
                            enemy.rotation = Quaternion.Euler(0, 0, 0);
                            enemy.position = Vector2.MoveTowards(enemy.position, wayPoints[target].position, currentNavTime);
                        }
                    
                    }
                    else
                    {
                        enemy.position = Vector2.MoveTowards(enemy.position, exitPoint.position, currentNavTime);
                    }

                    currentNavTime = 0;
                }
            }
        //}

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "WP")
        {
            target += 1;

        }
    }
}
