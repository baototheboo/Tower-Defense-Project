using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public Text CurrenHealthText;
    public int currentWave = 0;
    int numberEnemy3 = 0;
    int numberEnemy2 = 0;
    public int currentHealth;
    public GameObject winWindow;
    public GameObject loseWindow;
    
    public int currentGold;
    public Text goldText;
    private void Awake()
    {
        instance = this;
        currentHealth = maxHealth;
    }
    
    // Update is called once per frame
    void Update()
    {
        goldText.text = currentGold.ToString();
        CurrentWaveText.text = "Wave " + currentWave.ToString();
        CurrenHealthText.text = currentHealth.ToString()+"/"+maxHealth.ToString();
        if(currentWave < maxWave && enemiesOnScreen == 0)
        {
            currentWave++;
            totalEnemies++;
            maxEnemiesOnScreen++;
            numberEnemy3 = 0;
            numberEnemy2 = 0;
            totalCurrentEnemies = 0;
            StartCoroutine(Spawn());
        }
        else if(currentWave == maxWave && enemiesOnScreen == 0)
        {
            winWindow.SetActive(true);
            StopAllCoroutines();
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

    IEnumerator Spawn()
    {
        if(enemiesPerSpawn > 0 && totalCurrentEnemies < totalEnemies)
        {
            for (int i = 0; i < enemiesPerSpawn; i++)
            {
                if(totalCurrentEnemies < maxEnemiesOnScreen)
                {
                    
                    GameObject newEnemy = Instantiate(enemies[whichEnemy()] as GameObject);
                    newEnemy.transform.position = spawnPoint.transform.position;
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
            currentHealth = 0;
        }
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
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
