using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGameManager : MonoBehaviour
{
    public static TestGameManager testGameManager;

    public static int currentShipS;
    int currentShip = 0;

    public GameObject[] ships;
    public GameObject[] shipSpawnPos;
    public GameObject shipSpawnPosition;
    [SerializeField] GameObject cloneShip;

    //=====================================
    //[SerializeField]int[] owned = { 1, 1, 0};
    public static int[] ownedStatic = { 1, 1, 0};
    public string[] shipOwned;
    public int coin = 200;
    //public int[] coinShip;

    private void Awake()
    {
        PlayerPrefs.SetInt("isOwned0", 1);
        //PlayerPrefs.SetInt("isOwned1", 0);

        shipOwned = new string[ownedStatic.Length];

        for (int i = 0; i < ownedStatic.Length; i++)
        {
            shipOwned[i] = "isOwned" + i;
            ownedStatic[i] = PlayerPrefs.GetInt(shipOwned[i]);
            //owned[i] = ownedStatic[i];

            Debug.Log(shipOwned[i] + " " + PlayerPrefs.GetInt(shipOwned[i]));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < ships.Length; i++)
            Instantiate(ships[i], shipSpawnPos[i].transform.position, Quaternion.identity);

        GetCurrentShip();
        currentShip = currentShipS;
        cloneShip = Instantiate(ships[currentShip], shipSpawnPosition.transform.position, Quaternion.identity);
    }

    public void GetCurrentShip()
    {
        currentShipS = PlayerPrefs.GetInt("CurrentShip");
    }

    public void GetCurrentOwnedState()
    {
        for (int i = 0; i < ownedStatic.Length; i++)
        {
            ownedStatic[i] = PlayerPrefs.GetInt(shipOwned[i]);
        }
    }    
}
