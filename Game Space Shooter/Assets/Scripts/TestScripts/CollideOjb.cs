using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideOjb : MonoBehaviour
{
    //public Circle circle;
    [SerializeField] int hp = 10;
    [SerializeField] Circle Circle;
    Circle collideCircle;

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Va cham voi " + collision.gameObject.name);
            Debug.Log(1);
        } 
            
    }*/

    private void Start()
    {
        //dmg = Circle.Dmg;
    }

    public void TakeDmg(int dmg)
    {
        hp -= dmg;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collideCircle = Circle;
            Debug.Log("dam = "+ collideCircle.dmg);
            TakeDmg(collideCircle.dmg);
        }    
    }
}
