using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager shopManager;

    public static int[] ownedStatic = {1, 0, 0 };
    public string[] shipOwned;

    private void Awake()
    {
        if (shopManager && shopManager != this)
        {
            Debug.LogError("Loi nhieu Shop Manager");
        }
        else
            shopManager = this;

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
}
