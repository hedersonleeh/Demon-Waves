using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragoncilloBehaviur : MonoBehaviour {
    Transform targetPlayer;
    public GameObject fireballPrefab;
    public float timePerShoot;
    public float delay;
    public float timerDeMover = 2f;
    Animator dragonAnimator;
    
    public int enemyLife;
    public float speed;
    public float stunTime;
    public float timer;
    Animator enemyAnim;
    float beginSpeed = 0.08f;
    float finalSpeed = 0;

    // Use this for initialization
    void Start () {
        
        targetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        timePerShoot = delay;
        dragonAnimator = GetComponent<Animator>();
        enemyAnim = GetComponent<Animator>();

       
    }

    // Update is called once per frame
    void Update() {

        if (timerDeMover <=0)
        {
            speed = beginSpeed;
            transform.position = Vector2.MoveTowards(transform.position, targetPlayer.position, speed * Time.deltaTime);
            ShootPlayer();
            dragonAnimator.SetBool("Move", true);
                if (enemyLife == 0)
                {
                    transform.position = transform.position;
                beginSpeed = finalSpeed;

                }
        }
        else
        {
            timerDeMover -= Time.deltaTime;
        }

       if(enemyLife == 0)
        {
            GameManager.instance.Score(90f);
        }
    }
    void ShootPlayer()
    {
        if (timePerShoot <= 0)
        {
            Instantiate(fireballPrefab, transform.position, Quaternion.identity);
            timePerShoot = delay;

            
            
        }
        else
        {
            timePerShoot -= Time.deltaTime;
        }
    }
    void Stun()
    {
        if (enemyLife == 0)
        {
            
            timer = stunTime;
            return;
        }
        else
        {
            GameManager.instance.Score(10f);
           enemyAnim.SetTrigger("isHit");
            speed = 0;
            timer = stunTime;
        }
    }
    public void DestroyEnemy()
    {
        GameManager.instance.enemyCount++;
        GameManager.instance.Score(100f);
        Destroy(gameObject);

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            enemyLife--;
            FindObjectOfType<AudioManager>().Play("MonsterHit");
            if (enemyLife == 0)
            {
                FindObjectOfType<AudioManager>().Play("MonsterDeath");
                speed = 0;
                timePerShoot = 10;
                //EnemySoundDead.Play();
                
                Stun();
                 Debug.Log(speed);
                enemyAnim.SetBool("IsDead", true);

            }
            else
            {
                Stun();

            }

        }
    }
}
