using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    const float waitingTime = 1;
    public GameObject spawnPoint;
    public GameObject[] enemies;
    public int maxEnemiesOnScreen;
    public int enemiesOnScreen;
    public int totalEnemies;
    public int enemiesPerSpawn;
    public int currentGold;
    public Text goldText;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        goldText.text = currentGold.ToString();
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
        if(enemiesPerSpawn > 0 && enemiesOnScreen < totalEnemies)
        {
            for (int i = 0; i < enemiesPerSpawn; i++)
            {
                if(enemiesOnScreen < maxEnemiesOnScreen)
                {
                    GameObject newEnemy = Instantiate(enemies[0] as GameObject);
                    newEnemy.transform.position = spawnPoint.transform.position;
                    enemiesOnScreen++;
                }
            }

            yield return new WaitForSeconds(waitingTime);
            StartCoroutine(Spawn());
        }
    }
}
