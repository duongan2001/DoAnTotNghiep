using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourScript : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bulletPos;
    [SerializeField] float setFireRate;
    [SerializeField] int health;
    [SerializeField] float speed;
    int baseHealth;
    bool attack = false;

    //public static int countEnemy;
    //public static float getRanPosX;

    float fireRate;
    float stopPosY;
    float stopPosX;
    float spawnPosX;
    int enemySpawnCase;

    private void Awake()
    {
        fireRate = 0;
        spawnPosX = SpawnEnemyManager.spawnEnemyManager.enemyWave[SpawnEnemyManager.spawnEnemyManager.currentWave].enemySpawnPosX[SpawnEnemyManager.spawnEnemyManager.waveIndex];
        Debug.Log("Stop pos X: " + spawnPosX);
        stopPosX = SpawnEnemyManager.spawnEnemyManager.enemyWave[SpawnEnemyManager.spawnEnemyManager.currentWave].enemyStopPosX[SpawnEnemyManager.spawnEnemyManager.waveIndex];
        Debug.Log("Stop pos X: " + stopPosY);
        stopPosY = SpawnEnemyManager.spawnEnemyManager.enemyWave[SpawnEnemyManager.spawnEnemyManager.currentWave].enemyStopPosY[SpawnEnemyManager.spawnEnemyManager.waveIndex];
        Debug.Log("Stop pos Y: " + stopPosX);

        enemySpawnCase = SpawnEnemyManager.spawnEnemyManager.enemyWave[SpawnEnemyManager.spawnEnemyManager.currentWave].spawnCase;
        baseHealth = health;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {        
        Move();
    }

    public void EnemyFire()
    {
        fireRate -= Time.deltaTime;

        if (fireRate <= 0 && attack == true)
        {
            Instantiate(bullet, new Vector3(bulletPos.transform.position.x, bulletPos.transform.position.y), bullet.transform.rotation);
            fireRate = setFireRate;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AttackLine"))
        {
            EnemyFire();
        }

        if(collision.gameObject.CompareTag("PlayerBullet"))
        {
            health-=GameManager.dmg;

            if (health <= 0)
            {
                Destroy(gameObject);
                SpawnEnemyManager.spawnEnemyManager.enemyDown++;
                SoundManager.soundManager.PlaySFX(2);
                //GameManager.gameManager.Score += baseHealth;
                //UIManager.uIManager.SetScoreText("Score: " + GameManager.gameManager.Score);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AttackLine"))
        {
            EnemyFire();
        }
    }

    public void Move()
    {
        switch(enemySpawnCase)
        {
            case 0:
                if (transform.position.y > stopPosY)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime * speed);
                }
                else
                    attack = true;
                break;
            
            case 1:
                if (spawnPosX < 0)
                {
                    if (transform.position.x < stopPosX)
                    {                       
                        transform.position = new Vector3(transform.position.x + Time.deltaTime * speed, transform.position.y);
                    }
                    else
                        attack = true;
                }
                else if (spawnPosX > 0)
                {
                    if (transform.position.x > stopPosX)
                    {
                        transform.position = new Vector3(transform.position.x - Time.deltaTime * speed, transform.position.y);
                    }
                    else
                        attack = true;
                }
                break;
            default:
                break;
        }
    }
}
