using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyManager : MonoBehaviour
{
    public static SpawnEnemyManager spawnEnemyManager;

    public EnemyWave[] enemyWave;
    bool endWave = true;
    [SerializeField]bool endSpawn = false;

    [SerializeField] float waveCDDefault;
    float waveCD;
    [SerializeField] float case1CDDefault;
    float case1CD;
    public int currentWave = 0;
    [SerializeField] int countEnemy = 0;
    public int waveIndex = 0;
    public int enemyDown = 0;
    public bool levelCompleted = false;

    private void Awake()
    {
        if (spawnEnemyManager && spawnEnemyManager != this)
        {
            Debug.LogError("Loi nhieu Spawn Enemy Manager");
        }
        else
            spawnEnemyManager = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        case1CD = 0;
    }

    // Update is called once per frame
    void Update()
    {
        case1CD -= Time.deltaTime;
        waveCD -= Time.deltaTime;

        if (currentWave >= enemyWave.Length && endWave)
        {
            levelCompleted = true;
            Time.timeScale = 0;
            return;
        }

        if (currentWave < enemyWave.Length)
        {
            if (enemyDown == enemyWave[currentWave].enemyNumber)
            {
                countEnemy = 0;
                waveIndex = 0;
                endSpawn = false;
                enemyDown = 0;
                currentWave++;
                waveCD = waveCDDefault;
                GameManager.gameManager.enablePlayerAttack = false;
                return;
            }         
        }
            
        //Sinh quai
        if (GameManager.gameManager.currentTime >= 5 && countEnemy < enemyWave[currentWave].enemyNumber)
        {
            if (waveCD <= 0)
            {
                SpawnEnemy();
            }         
        }
            
    }

    public void SpawnCase0()
    {
        GameManager.gameManager.enablePlayerAttack = true;

        Debug.Log("Current Wave: " + currentWave);

        for (int i = 0; i < enemyWave[currentWave].enemyNumber; i++)
        {
            Instantiate(enemyWave[currentWave].enemy, new Vector3(enemyWave[currentWave].enemySpawnPosX[i], enemyWave[currentWave].enemySpawnPosY[i]), Quaternion.identity);
            countEnemy++;
        }      
    }

    public void SpawnCase1()
    {
        GameManager.gameManager.enablePlayerAttack = true;

        if (countEnemy >= enemyWave[currentWave].enemyNumber)
        {
            endSpawn = true;
        }

        if (!endSpawn && case1CD <= 0)
        {
            Debug.Log("Current Wave: " + currentWave + " - Enemy: " + waveIndex);
            Instantiate(enemyWave[currentWave].enemy, new Vector3(enemyWave[currentWave].enemySpawnPosX[waveIndex], enemyWave[currentWave].enemySpawnPosY[waveIndex]), Quaternion.identity); //Spawn Enemy Point
            countEnemy++;
            waveIndex++;
            case1CD = case1CDDefault;
        }
    }

    public void SpawnEnemy()
    {
        switch (enemyWave[currentWave].spawnCase)
        {
            case 0:
                SpawnCase0();
                break;

            case 1:
                SpawnCase1();
                break;
        }
    }
}
