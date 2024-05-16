using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject levelPanel;
    [SerializeField] GameObject highScorePanel;
    [SerializeField] GameObject shopPanel;
    [SerializeField] GameObject settingsPanel;
    [SerializeField] Text coin;

    /*[SerializeField] Text highScore0;
    [SerializeField] Text highScore1;
    [SerializeField] Text highScore2;
    [SerializeField] Text highScore3;
    [SerializeField] Text highScore4;*/

    public Button[] pickShip;
    public Text[] state;

    [SerializeField] int[] coinShip;
    public GameObject fadeAlertOjb;
    public Text fadeAlert;
    public Animation fadeAlertAnim;

    public Animator[] buttonsAnimator;

    public Toggle onOffMusic;
    public Toggle onOffSFX;

    public Animator[] levelButtonsAnimator;

    int coins;

    // Start is called before the first frame update
    private void Start()
    {
        /*HighScoreManager.scoreManager.GetHighScore();
        highScore0.text = "1st\t" + PlayerPrefs.GetInt("HighScore0");
        highScore1.text = "2nd\t" + PlayerPrefs.GetInt("HighScore1");
        highScore2.text = "3rd\t" + PlayerPrefs.GetInt("HighScore2");
        highScore3.text = "4th\t" + PlayerPrefs.GetInt("HighScore3");
        highScore4.text = "5th\t" + PlayerPrefs.GetInt("HighScore4");*/

        PlayerPrefs.SetInt("Level1", 1);
        PlayerPrefs.SetInt("OpenedLevel", 1);

        coins = PlayerPrefs.GetInt("Coin");
        coin.text = PlayerPrefs.GetInt("Coin") + "  ";

        SetStartShopState();

        GetSoundStateStart();

        Time.timeScale = 1f;

    }

    public void Play()
    {
        SoundManager.soundManager.PlaySFX(1);
        mainPanel.SetActive(false);
        levelPanel.SetActive(true);
        Destroy(ShipSpawnManager.shipSpawnManager.cloneShipMainMenu);

        for (int i = 0; i < levelButtonsAnimator.Length; i++)
        {
            int lv = i + 1;
            Debug.Log("Level " + lv);
            if (PlayerPrefs.GetInt("Level"+ lv) == 0)
            {
                levelButtonsAnimator[i].SetBool("isCompleted", false);
                Debug.Log("Level " + lv + " is not opened");
            }
            else
                levelButtonsAnimator[i].SetBool("isCompleted", true);
        }
    }

    /*public void HighScore()
    {
        SoundManager.soundManager.PlaySFX(1);
        mainPanel.SetActive(false);
        highScorePanel.SetActive(true);
    }*/

    /*public void CloseHighScore()
    {
        SoundManager.soundManager.PlaySFX(1);
        mainPanel.SetActive(true);
        highScorePanel.SetActive(false);

    }*/

    public void Shop()
    {
        //SetStartShopState();
        SoundManager.soundManager.PlaySFX(1);
        shopPanel.SetActive(true);
        mainPanel.SetActive(false);
        Destroy(ShipSpawnManager.shipSpawnManager.cloneShipMainMenu);

        for (int i = 0; i < ShipSpawnManager.shipSpawnManager.cloneShip.Length; i++)
        {
            ShipSpawnManager.shipSpawnManager.shipSpawnPos[i].SetActive(true);
        }

        SetShopButtonColor();
    }   
    
    public void CloseShop()
    {
        SoundManager.soundManager.PlaySFX(1);
        shopPanel.SetActive(false);
        mainPanel.SetActive(true);
        fadeAlertOjb.SetActive(false);

        ShipSpawnManager.shipSpawnManager.SpawnShipMainMenu();
        for (int i = 0; i < ShipSpawnManager.shipSpawnManager.cloneShip.Length; i++)
        {
            ShipSpawnManager.shipSpawnManager.shipSpawnPos[i].SetActive(false);
        }
    }    

    public void Settings()
    {
        SoundManager.soundManager.PlaySFX(1);
        settingsPanel.SetActive(true);
        mainPanel.SetActive(false);
        Destroy(ShipSpawnManager.shipSpawnManager.cloneShipMainMenu);
    }

    public void CloseSettings()
    {
        SoundManager.soundManager.PlaySFX(1);
        settingsPanel.SetActive(false);
        mainPanel.SetActive(true);
        ShipSpawnManager.shipSpawnManager.SpawnShipMainMenu();
    }

    public void Quit()
    {
        SoundManager.soundManager.PlaySFX(1);
        Application.Quit();
    }

    public void SetShipIndex()
    {
        pickShip[0].onClick.AddListener(() => {
            PickShip(0);
        });

        pickShip[1].onClick.AddListener(() => {
            PickShip(1);
        });

        pickShip[2].onClick.AddListener(() => {
            PickShip(2);
        });


        //Cách 2
        /*for (int i = 0; i < pickShip.Length; i++)
        {
            Debug.Log(i);
            pickShip[i].AddEventListener(i, PickShip);
        }*/
    }

    public void PickShip(int index)
    {
        if (ShopManager.ownedStatic[index] == 1)//Tau nay da mua
        {
            SoundManager.soundManager.PlaySFX(1);
            PickOwnedShip(index);
        }
        else
        {
            if (coins >= coinShip[index])//Du tien mua tau
            {
                SoundManager.soundManager.PlaySFX(2);
                //Cho tau thanh da mua
                coins = coins - coinShip[index];

                Debug.Log(coins + "coins remain");

                PlayerPrefs.SetInt("Coin", coins);
                coin.text = PlayerPrefs.GetInt("Coin") + " coins";

                PlayerPrefs.SetInt(ShopManager.shopManager.shipOwned[index], 1);
                ShopManager.ownedStatic[index] = PlayerPrefs.GetInt(ShopManager.shopManager.shipOwned[index]);

                PickOwnedShip(index);
            }
            else
            {
                SoundManager.soundManager.PlaySFX(1);
                fadeAlertOjb.SetActive(true);
                fadeAlert.text = "Not enough coins";
                fadeAlertAnim.Play();
            }
        }
    }

    public void SetStartShopState()
    {
        Debug.Log(PlayerPrefs.GetInt("CurrentShip"));

        for (int i = 0; i < pickShip.Length; i++)//Duyet danh sach tau trong shop
        {
            if (ShopManager.ownedStatic[i] == 1)
            {
                if (i == PlayerPrefs.GetInt("CurrentShip")/*id*/)
                {
                    state[i].text = "Selected";
                }       
                else
                {
                    state[i].text = "Select";
                }
            }
            else
            {
                state[i].text = coinShip[i].ToString();
            }
        }

        SetShipIndex();
    }

    public void SetShopButtonColor()
    {
        for (int i = 0; i < pickShip.Length; i++)//Duyet danh sach tau trong shop
        {
            if (ShopManager.ownedStatic[i] == 1)
            {
                if (i == PlayerPrefs.GetInt("CurrentShip")/*id*/)
                {
                    buttonsAnimator[i].SetBool("Selected", true);
                }
                else
                {
                    buttonsAnimator[i].SetBool("Selected", false);
                }
            }
            else
            {
                buttonsAnimator[i].SetBool("Coin", true);
            }
        }
    }    

    public void PickOwnedShip(int index)
    {
        PlayerPrefs.SetInt("CurrentShip", index);
        Debug.Log(PlayerPrefs.GetInt("CurrentShip")/*id*/);
        ShipSpawnManager.currentShipStatic = PlayerPrefs.GetInt("CurrentShip");

        for (int i = 0; i < pickShip.Length; i++)//Duyet danh sach tau trong shop
        {
            if (ShopManager.ownedStatic[i] == 1)
            {
                if (i == PlayerPrefs.GetInt("CurrentShip")/*id*/)
                {
                    buttonsAnimator[i].SetBool("Coin", false);
                    buttonsAnimator[i].SetBool("Selected", true);
                    state[i].text = "Selected";
                }
                else
                {
                    buttonsAnimator[i].SetBool("Selected", false);
                    state[i].text = "Select";
                }
            }
        }
    }

    public void GetSoundStateStart()
    {
        if (PlayerPrefs.HasKey("musicState"))
        {
            if (PlayerPrefs.GetInt("musicState") == 1)
                onOffMusic.isOn = true;
            else
                onOffMusic.isOn = false;
        }
        else
        {
            onOffMusic.isOn = true;
            PlayerPrefs.SetInt("musicState", 1);
        }
            

        if (PlayerPrefs.HasKey("sfxState"))
        {
            if (PlayerPrefs.GetInt("sfxState") == 1)
                onOffSFX.isOn = true;
            else
                onOffSFX.isOn = false;
        }
        else
        {
            onOffSFX.isOn = true;
            PlayerPrefs.SetInt("sfxState", 1);
        }
    }

    public void SetMusicState()
    {
        //SoundManager.soundManager.PlaySFX(1);
        bool state = onOffMusic.isOn;
        Debug.Log("Music now: " + PlayerPrefs.GetInt("musicState"));
        if (state == true)
        {
            PlayerPrefs.SetInt("musicState", 1);
            SoundManager.soundManager.sounds[0].source.Play();
        }
        else
        {
            PlayerPrefs.SetInt("musicState", 0);
            SoundManager.soundManager.sounds[0].source.Stop();
        }
            
        Debug.Log("Music: " + PlayerPrefs.GetInt("musicState"));
    }

    public void SetSFXState()
    {
        //SoundManager.soundManager.PlaySFX(1);
        Debug.Log("SFX now: " + PlayerPrefs.GetInt("sfxState"));
        if (onOffSFX.isOn == true)
        {
            PlayerPrefs.SetInt("sfxState", 1);
            SoundManager.soundManager.SetOnOffSFXState();
        }
        else
        {
            PlayerPrefs.SetInt("sfxState", 0);
            SoundManager.soundManager.SetOnOffSFXState();
        }
            
        Debug.Log("SFX: " + PlayerPrefs.GetInt("sfxState"));
    }

    public void PlayLevel(int i)
    {
        SoundManager.soundManager.PlaySFX(1);
        if (PlayerPrefs.GetInt("Level" + i) == 1)
        {
            PlayerPrefs.SetInt("CurrentLevel", i);
            SceneManager.LoadScene("Level" + i);
        }      
    }

    public void CloseLevelPanel()
    {
        SoundManager.soundManager.PlaySFX(1);
        mainPanel.SetActive(true);
        levelPanel.SetActive(false);
        ShipSpawnManager.shipSpawnManager.SpawnShipMainMenu();
    }
}
