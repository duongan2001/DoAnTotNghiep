using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public static class ShopExtention 
{
    // Start is called before the first frame update
    public static void AddEventListener<T>(this Button btn, T param, Action<T> Onclick)
    {
        btn.onClick.AddListener(delegate () {
            Onclick(param);
        });
    }

}
