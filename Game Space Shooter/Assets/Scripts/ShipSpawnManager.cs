using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSpawnManager : MonoBehaviour
{
    public static ShipSpawnManager shipSpawnManager;
    public static int currentShipStatic;

    //int currentShip = 0;

    [SerializeField] GameObject shipSpawnPosMainMenu;
    public GameObject[] ships;
    public GameObject[] shipsSpawnMainMenu;
    public GameObject[] shipSpawnPos;
    public GameObject[] cloneShip;
    public GameObject cloneShipMainMenu;

    private void Awake()
    {
        if (shipSpawnManager && shipSpawnManager != this)
        {
            Debug.LogError("Loi nhieu Ship Spawn Manager");
        }
        else
            shipSpawnManager = this;

        //==========================================
        //PlayerPrefs.SetInt("isOwned1",0);
        //PlayerPrefs.SetInt("isOwned2", 0);
        //PlayerPrefs.SetInt("Coin", 00);
    }

    // Start is called before the first frame update
    void Start()
    {
        GetCurrentShip();
        //currentShip = currentShipStatic;
        SpawnShipMainMenu();
        //SpawnShipInShop();

    }

    public void GetCurrentShip()
    {
        currentShipStatic = PlayerPrefs.GetInt("CurrentShip");
    }

    public void SpawnShipMainMenu()
    {
        cloneShipMainMenu = Instantiate(shipsSpawnMainMenu[PlayerPrefs.GetInt("CurrentShip")], shipSpawnPosMainMenu.transform.position, Quaternion.identity);
    }

    /*public void SpawnShipInShop()
    {
        for (int i = 0; i < ships.Length; i++)
        {
            shipSpawnPos[i].SetActive(false);
        }

    }*/
}
