using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTarget : MonoBehaviour
{
    public Transform closestEnemy;
    public GameObject[] multipleEnemies;
    public Tower tower;
    public bool shouldShoot;

    // Start is called before the first frame update
    void Start()
    {
        tower = GetComponentInParent<Tower>();
    }

    // Update is called once per frame
    void Update()
    {
        if (closestEnemy != null)
        {
            LockAtEnemy();
            tower.Shoot();

        }

        closestEnemy = GetClosestEnemy();
    }

    public void LockAtEnemy()
    {
        Vector2 lookDirection = closestEnemy.transform.position - transform.position;
        transform.up = new Vector2(lookDirection.x, lookDirection.y);
    }

    public Transform GetClosestEnemy()
    {
        //enemies are those objects that have a tag of enemy
        multipleEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        //get the closest value
        float closestDistance = Mathf.Infinity;
        //transform of enemies are null
        Transform enemyPos = null;

        //search through enemies to find closest enemy and put it as a target
        foreach (GameObject enemies in multipleEnemies)
        {
            //get float value of current distance
            float currentDistance;
            currentDistance = Vector3.Distance(transform.position, enemies.transform.position);

            if (currentDistance < closestDistance)
            {
                closestDistance = currentDistance;
                enemyPos = enemies.transform;
            }
        }

        return enemyPos;

    }
}
