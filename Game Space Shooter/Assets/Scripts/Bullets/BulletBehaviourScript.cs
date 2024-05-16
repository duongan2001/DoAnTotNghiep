using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviourScript : MonoBehaviour
{
    [SerializeField] float speed;
    // Update is called once per frame
    void Update()
    {

        if (gameObject.transform.position.y >= 6)
        {
            Destroy(gameObject);
        }
        transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
