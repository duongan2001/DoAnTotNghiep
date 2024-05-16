using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    //int health;
    public int dmg;


    public int Dmg
    {
        get { return dmg; }
        set { dmg = value; }
    }

    public void ABC()
    {
        Debug.Log("ABCCC");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Test Invoke");
            InvokeRepeating("ABC", 1, 1);
        }
    }
}
