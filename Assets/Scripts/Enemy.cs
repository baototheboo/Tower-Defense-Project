using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public int target = 0;
    public Transform exitPoint;
    public Transform[] wayPoints;
    public float navTimeUpdate;
    public float currentNavTime;
    public Transform enemy;
    private EnemiesHealth enemieHealth;

    // Start is called before the first frame update
    void Start()
    {
        enemieHealth = GetComponent<EnemiesHealth>();
        enemy = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemieHealth.isDead != true)
        {
            if (wayPoints != null)
            {
                currentNavTime += Time.deltaTime;
                if (currentNavTime > navTimeUpdate && speed > 0)
                {
                    if (target < wayPoints.Length)
                    {

                        if (enemy.position.x > wayPoints[target].position.x)
                        {
                            enemy.rotation = Quaternion.Euler(0, 180, 0);
                            enemy.position = Vector2.MoveTowards(enemy.position, wayPoints[target].position, speed * currentNavTime);
                        }
                        else
                        {
                            enemy.rotation = Quaternion.Euler(0, 0, 0);
                            enemy.position = Vector2.MoveTowards(enemy.position, wayPoints[target].position, speed * currentNavTime);
                        }

                    }
                    else
                    {
                        enemy.position = Vector2.MoveTowards(enemy.position, exitPoint.position, speed * currentNavTime);
                    }
                    currentNavTime = 0;
                }
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "WP")
        {
            target += 1;

        }
    }
}
