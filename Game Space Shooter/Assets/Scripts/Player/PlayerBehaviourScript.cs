using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviourScript : MonoBehaviour
{
    [SerializeField] GameObject gameObj;
    [SerializeField] GameObject getPosition;
    [SerializeField] Animator animator;

    [SerializeField] float tocDo;
    [SerializeField]float setFireRate;
    float fireRate;
    float loR;
    float uoD;

    // Start is called before the first frame update
    void Start()
    {
        fireRate = 0;
        GameManager.gameManager.enablePlayerAttack = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Move
        Move();

        //Fire
        Fire();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")||collision.gameObject.CompareTag("EnemyBullet"))
        {
            Debug.Log("Thua");
            SoundManager.soundManager.PlaySFX(4);
            //UIManager.uIManager.SetScoreGOText("Score: " + GameManager.gameManager.Score);
            GameManager.gameManager.SetGameOver(true);
            
        }
    }

    public void Move()
    {
        //Move
        loR = Input.GetAxis("Horizontal");
        if (loR < 0)
        {
            animator.SetBool("Left", true);
            animator.SetBool("Right", false);
        }
        else if (loR > 0)
        {
            animator.SetBool("Left", false);
            animator.SetBool("Right", true);
        }
        else
        {
            animator.SetBool("Left", false);
            animator.SetBool("Right", false);
        }

        uoD = Input.GetAxis("Vertical");
        transform.position = new Vector3(transform.position.x + loR * tocDo * Time.deltaTime, transform.position.y + uoD * tocDo * Time.deltaTime);
    }

    public void Fire()
    {
        fireRate -= Time.deltaTime;
        if (fireRate <= 0 && GameManager.gameManager.enablePlayerAttack)
        {
            Instantiate(gameObj, new Vector3(getPosition.transform.position.x, getPosition.transform.position.y), gameObj.transform.rotation);
            fireRate = setFireRate;
        }
    }    
}
