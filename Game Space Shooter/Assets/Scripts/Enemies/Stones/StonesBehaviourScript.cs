using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StonesBehaviourScript : MonoBehaviour
{
    [SerializeField] float tocDoRoi;
    [SerializeField] int health;

    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - tocDoRoi * Time.deltaTime);
        if (transform.position.y<=-6)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            //Destroy(gameObject);
            health -= GameManager.dmg;
            
            if (health <= 0)
            {
                SoundManager.soundManager.PlaySFX(3);
                Destroy(gameObject);
            }
        }
    }
}
