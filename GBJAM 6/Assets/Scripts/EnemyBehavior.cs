using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

    public PlayerController player;
    Transform target;
    Animator enemyAnim;
    

    public int enemyLife = 3;
    public float speed;
    public float stunTime;
    public float timer;

    void Start() {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        timer = 0f;
        enemyAnim = GetComponent<Animator>();
        

    }

    // Update is called once per frame
    void Update() {
       
        if (timer <= 0f)
        {
            if (target == null)
            {
                return;
            }
            else
            {
                speed = 0.1f;
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
        }
        else if (enemyLife <= 0)
        {
            
            transform.position = transform.position;
        }
        else
        {
            timer -= Time.deltaTime;
        }
        EnemyDificulty();

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            enemyLife--;
            FindObjectOfType<AudioManager>().Play("SkeletonHit");
            if (enemyLife == 0)
            {
                FindObjectOfType<AudioManager>().Play("SkeletonDeath");
                GameManager.instance.Score(100f);
               Stun();
               // Debug.Log(speed);
                enemyAnim.SetBool("isDead", true);

            }
            else
            {
                Stun();

            }

        }
    }
    public void DestroyEnemy()
    {
        GameManager.instance.enemyCount++;
        Destroy(gameObject);



    }
    void Stun()
    {
        if (enemyLife == 0)
        {
            speed = 0;
            timer = stunTime;
            return;
        } else
        {
            GameManager.instance.Score(10f);
            enemyAnim.SetTrigger("isHit");
            speed = 0;
            timer = stunTime;
        }
    }
    void EnemyDificulty()
    {
        if (GameManager.instance.wave >= 4 && GameManager.instance.wave <= 6)
        {
            speed = 0.2f;
            //enemyLife = 4;
        }
        if (GameManager.instance.wave >= 6 && GameManager.instance.wave <= 10)
        {
            speed *= 2f;
           // enemyLife = 5;
        }
        if (GameManager.instance.wave >= 6 && GameManager.instance.wave <= 10)
        {
            speed *= 3f;
            //enemyLife= 6;
        }
    }

}
