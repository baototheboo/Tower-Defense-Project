using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float waitingTime = 1;
    public GameObject spawnPoint;
    public GameObject[] enemies;
    public int maxEnemiesOnScreen;
    public int enemiesOnScreen;
    public int totalEnemies;
    public int enemiesPerSpawn;
    public int maxWave;
    public int currentWave = 0;
    int numberEnemy3 = 0;
    int numberEnemy2 = 0;
    
    private void Awake()
    {
        instance = this;
    }
    
    // Update is called once per frame
    void Update()
    {
        if(currentWave < maxWave && enemiesOnScreen == 0)
        {
            currentWave++;
            totalEnemies++;
            maxEnemiesOnScreen++;
            numberEnemy3 = 0;
            numberEnemy2 = 0;
            StartCoroutine(Spawn());
        }
        else if(currentWave > maxWave)
        {
            StopAllCoroutines();
        }
    }

    IEnumerator Spawn()
    {
        if(enemiesPerSpawn > 0 && enemiesOnScreen < totalEnemies)
        {
            for (int i = 0; i < enemiesPerSpawn; i++)
            {
                if(enemiesOnScreen < maxEnemiesOnScreen)
                {
                    
                    GameObject newEnemy = Instantiate(enemies[whichEnemy()] as GameObject);
                    newEnemy.transform.position = spawnPoint.transform.position;
                    enemiesOnScreen++;
                }
            }

            yield return new WaitForSeconds(waitingTime);
            StartCoroutine(Spawn());
        }
    }

    int whichEnemy()
    {
        if (currentWave > 10)
        {
            if (numberEnemy2 < currentWave / 10)
            {
                numberEnemy2++;
                return 1;
            }
            return 0;
        }
        else if (currentWave > 3 && currentWave <= 10)
        {
            if(numberEnemy3 < currentWave / 3)
            {
                numberEnemy3++;
                return 2;
            }
            return 0;
        }
        return 0;
    }
}
