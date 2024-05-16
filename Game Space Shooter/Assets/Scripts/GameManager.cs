using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Bien singleton
    public static GameManager gameManager;
    #endregion

    #region Bien unity editor
    [SerializeField] float timeSpawnStone;
    [SerializeField] GameObject[] stones;

    [SerializeField] float timeSpawnEnemy;
    [SerializeField] GameObject[] enemies;
    [SerializeField] EnemyWave[] enemyWaves;
    
    [SerializeField] GameObject[] ships;

    public int[] shipDamage;

    [SerializeField] GameObject shipSpawnPos;   
    #endregion

    #region Bien spawn case
    float spawnStone;
    public float currentTime;

    public int spawnCase;

    public static int countEnemyCase1;
    public static float getRanPosX;
    public static float getRanPosY;
    #endregion

    #region Bien xu ly UI
    //int score;
    bool isDead;
    bool isPause;
    #endregion

    #region Bien static
    public static int dmg;
    public static int enemyDown = 0;
    #endregion

    public bool enablePlayerAttack;
    int coin;
    int levelReward;

    [SerializeField] float bgSpeed;
    [SerializeField] GameObject bg;

    private void Awake()
    {
        if (gameManager && gameManager != this)
        {
            Debug.LogError("Loi nhieu Game Manager");
        }
        else
            gameManager = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        levelReward = (3 + PlayerPrefs.GetInt("CurrentLevel")) * 50;
        Time.timeScale = 1f;
        spawnStone = 10;
        //score = 0;
        isPause = false;
        currentTime = 0;

        coin = PlayerPrefs.GetInt("Coin");
        dmg = shipDamage[PlayerPrefs.GetInt("CurrentShip")];

        Instantiate(ships[PlayerPrefs.GetInt("CurrentShip")], shipSpawnPos.transform.position, Quaternion.identity);

        UIManager.uIManager.SetRewardText("+" + levelReward + " ");
    }

    // Update is called once per frame
    void Update()
    {
        if (SpawnEnemyManager.spawnEnemyManager.levelCompleted)
        {
            UIManager.uIManager.SetNextLevelState(true);
            return;
        }

        //CHECK GAMEOVER
        if (isDead)
        {            
            Time.timeScale = 0;
            UIManager.uIManager.SetGameoverState(true);
            //HighScoreManager.scoreManager.CompareHighScore(Score);
            //HighScoreManager.scoreManager.SetHighScore();
            SoundManager.soundManager.sounds[0].source.Stop();
            //score = 0;
            return;
        }

        //CHECK PAUSE GAME
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause)
                Resume();
            else
                Pause();
        }

        currentTime += Time.deltaTime;

        MoveBackGround(bg);
        SpawnStone();
    }

/*    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
        }
    }*/

    public void SpawnStone()
    {
        spawnStone -= Time.deltaTime;
        //Sinh da
        if (spawnStone <= 0)
        {
            int ran = Random.Range(0, stones.Length);
            Instantiate(stones[ran], new Vector3(Random.Range(-2.55f, 2.55f), 6), Quaternion.identity);
            spawnStone = timeSpawnStone;
        }
    }
  
    public void SetGameOver(bool state)
    {
        isDead = state;
    }

    public void RePlay()
    {
        SoundManager.soundManager.PlaySFX(1);
        isDead = false;
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
        currentTime = 0;
    }

    public void Resume()
    {
        SoundManager.soundManager.PlaySFX(1);
        isPause = false;
        Time.timeScale = 1;
        UIManager.uIManager.SetPauseState(false);
    }

    public void Pause()
    {
        SoundManager.soundManager.PlaySFX(1);
        isPause = true;
        Time.timeScale = 0;
        UIManager.uIManager.SetPauseState(true);
    }

    public void Mainmenu()
    {
        SoundManager.soundManager.PlaySFX(1);
        isDead = false;

        if (PlayerPrefs.GetInt("CurrentLevel") == PlayerPrefs.GetInt("OpenedLevel") && PlayerPrefs.GetInt("CurrentLevel") != 10)
        {
            PlayerPrefs.SetInt("OpenedLevel", PlayerPrefs.GetInt("OpenedLevel") + 1);
            PlayerPrefs.SetInt("Level" + PlayerPrefs.GetInt("OpenedLevel"), 1);
        }

        coin += levelReward;
        PlayerPrefs.SetInt("Coin", coin);

        SceneManager.LoadScene("Mainmenu");
    }    

    public void NextLevel()
    {
        if (PlayerPrefs.GetInt("CurrentLevel") < PlayerPrefs.GetInt("OpenedLevel"))
        {
            PlayerPrefs.SetInt("CurrentLevel", PlayerPrefs.GetInt("CurrentLevel") + 1);
            SceneManager.LoadScene("Level" + PlayerPrefs.GetInt("CurrentLevel"));
        }
        else if (PlayerPrefs.GetInt("CurrentLevel") == PlayerPrefs.GetInt("OpenedLevel") && PlayerPrefs.GetInt("CurrentLevel") != 10)
        {
            PlayerPrefs.SetInt("OpenedLevel", PlayerPrefs.GetInt("OpenedLevel") + 1);
            PlayerPrefs.SetInt("Level" + PlayerPrefs.GetInt("OpenedLevel"), 1);
            PlayerPrefs.SetInt("CurrentLevel", PlayerPrefs.GetInt("CurrentLevel") + 1);
            SceneManager.LoadScene("Level" + PlayerPrefs.GetInt("CurrentLevel"));
        }
    }

    public void MoveBackGround(GameObject bg)
    {
        float loR = Input.GetAxis("Horizontal");
        float uoD = Input.GetAxis("Vertical");
        bg.transform.position = new Vector3(bg.transform.position.x - loR * bgSpeed * Time.deltaTime, bg.transform.position.y - uoD * bgSpeed * Time.deltaTime);
    }
}
