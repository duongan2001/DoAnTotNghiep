using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletBehavior : MonoBehaviour
{
    [SerializeField] float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= -6)
        {
            Destroy(gameObject);
        }

        transform.position = new Vector3(transform.position.x, transform.position.y - speed * Time.deltaTime);
    }
}
