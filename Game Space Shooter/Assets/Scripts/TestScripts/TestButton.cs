using UnityEngine;
using UnityEngine.UI;

public class TestButton : MonoBehaviour
{
    // Start is called before the first frame update
    public Button[] pickShip;
    public Text[] state;
    public Text testFade;
    public Animation testFadeAnim;
    public GameObject clone;

    //doi mau nut===========================================================
    public Animator[] buttonsAnimator;

    //
    [SerializeField]int[] coinShip;
    
    private void Start()
    {
        Debug.Log(PlayerPrefs.GetInt("CurrentShip"));

        for (int i = 0; i < pickShip.Length; i++)//Duyet danh sach tau trong shop
        {
            if (TestGameManager.ownedStatic[i] == 1)
            {
                if (i == PlayerPrefs.GetInt("CurrentShip")/*id*/)
                {
                    //doi mau nut===========================================================
                    buttonsAnimator[i].SetBool("Selected",true);
                    
                    state[i].text = "Selected";
                } 
                else
                {
                    //doi mau nut===========================================================
                    buttonsAnimator[i].SetBool("Selected", false);

                    state[i].text = "Select";
                }                        
            }
            else
            {
                //doi mau nut===========================================================
                buttonsAnimator[i].SetBool("Coin", true);

                state[i].text = coinShip[i].ToString();
            }                   
        }

        SetShipIndex();

        //PlayerPrefs.SetInt("Coin", 1000);
    }

    public void SetShipIndex()
    {
        pickShip[0].onClick.AddListener(() => { PickShip(0);
        });

        pickShip[1].onClick.AddListener(() => { PickShip(1);
            
        });

        pickShip[2].onClick.AddListener(() => { PickShip(2);
            
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
        if (TestGameManager.ownedStatic[index]==1)//Tau nay da mua
        {
            PickOwnedShip(index);
        }
        else
        {
            if (PlayerPrefs.GetInt("Coin") >= coinShip[index])//Kiem tra tien co voi tien tau
            {
                //Cho tau thanh da mua
                PlayerPrefs.SetInt(TestGameManager.testGameManager.shipOwned[index], 1);
                TestGameManager.ownedStatic[index] = PlayerPrefs.GetInt(TestGameManager.testGameManager.shipOwned[index]);

                PickOwnedShip(index);
            }
            else
            {
                testFade.text = "K du tien"; 
                testFadeAnim.Play();
            }    
        }   
    }   
    
    public void PickOwnedShip(int index)
    {
        PlayerPrefs.SetInt("CurrentShip", index);
        Debug.Log(PlayerPrefs.GetInt("CurrentShip")/*id*/);
        TestGameManager.currentShipS = PlayerPrefs.GetInt("CurrentShip");

        for (int i = 0; i < pickShip.Length; i++)//Duyet danh sach tau trong shop
        {
            if (TestGameManager.ownedStatic[i] == 1)
            {
                if (i == PlayerPrefs.GetInt("CurrentShip")/*id*/)
                {
                    //doi mau nut===========================================================
                    buttonsAnimator[i].SetBool("Selected", true);

                    state[i].text = "Selected";
                }
                else
                {
                    //doi mau nut===========================================================
                    buttonsAnimator[i].SetBool("Selected", false);

                    state[i].text = "Select";
                }    
                    
            }
        }
    }    

}
