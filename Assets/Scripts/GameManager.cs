using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Scripts;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float waitingTime = 1;
    public GameObject spawnPoint;
    public GameObject[] enemies;
    public int maxEnemiesOnScreen;
    public int enemiesOnScreen;
    public int totalEnemies;
    public int totalCurrentEnemies;
    public int enemiesPerSpawn;
    public int maxWave;
    public int maxHealth = 4;
    public Text CurrentWaveText;
    public Text LoseAtWave;
    public Text CurrenHealthText;
    public int currentWave = 0;
    int numberEnemy3 = 0;
    int numberEnemy2 = 0;
    public int currentHealth;
    public GameObject winWindow;
    public GameObject loseWindow;

    public GameObject[] screenObjects;

    public int currentGold;
    public Text goldText;
    private void Awake()
    {
        instance = this;
        currentHealth = maxHealth;
    }
    public string[] listTagEnemy = { "Enemy1", "Enemy2", "Enemy3" };
    // Update is called once per frame
    void Update()
    {
        goldText.text = currentGold.ToString();
        if (ObjectPool.Instance.poolDictionary["Enemy1"].Count < totalEnemies+1)
        {
            ObjectPool.Instance.AddToPool("Enemy1");
        } 
        else if(ObjectPool.Instance.poolDictionary["Enemy2"].Count < currentWave/10+1)
        {
            ObjectPool.Instance.AddToPool("Enemy2");
        }
        else if (ObjectPool.Instance.poolDictionary["Enemy3"].Count < currentWave / 3+1)
        {
            ObjectPool.Instance.AddToPool("Enemy3");
        }
        else
        {
            CurrentWaveText.text = "WAVE " + currentWave.ToString();
            LoseAtWave.text = "You survived for " + currentWave.ToString() + " wave(s)!";
            CurrenHealthText.text = currentHealth.ToString() + "/" + maxHealth.ToString();
            if (currentWave < maxWave && enemiesOnScreen == 0)
            {
                currentWave++;
                totalEnemies++;
                maxEnemiesOnScreen++;
                numberEnemy3 = 0;
                numberEnemy2 = 0;
                totalCurrentEnemies = 0;
                StartCoroutine(Spawn());
            }
            else if (currentWave == maxWave && enemiesOnScreen == 0)
            {
                winWindow.SetActive(true);
                StopAllCoroutines();
            }
        }
    }

    public void AddGold(int amount)
    {
        currentGold += amount;
    }

    public void ReduceGold(int amount)
    {
        currentGold -= amount;

    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        if (Time.timeScale == 0f)
            Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        if (Time.timeScale == 0f)
            Time.timeScale = 1f;
    }

    IEnumerator Spawn()
    {
        if(enemiesPerSpawn > 0 && totalCurrentEnemies < totalEnemies)
        {
            for (int i = 0; i < enemiesPerSpawn; i++)
            {
                if(enemiesOnScreen < maxEnemiesOnScreen)
                {

                    //GameObject newEnemy = Instantiate(enemies[whichEnemy()] as GameObject);
                    //newEnemy.transform.position = spawnPoint.transform.position;
                    ObjectPool.Instance.SpawnFromPool(listTagEnemy[whichEnemy()], spawnPoint.transform.position, Quaternion.identity);
                    enemiesOnScreen++;
                    totalCurrentEnemies++;
                }
            }

            yield return new WaitForSeconds(waitingTime);
            StartCoroutine(Spawn());
        }
    }
    public void PlayerGetDamage()
    {
        currentHealth--;
        if(currentHealth <= 0)
        {
            loseWindow.SetActive(true);
            Time.timeScale = 0f;
            currentHealth = 0;
            foreach (GameObject obj in screenObjects)
            {
                obj.SetActive(false);
            }
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
            if (numberEnemy3 < currentWave / 3)
            {
                numberEnemy3++;
                return 2;
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
