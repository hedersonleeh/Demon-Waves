using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject GunshootPrefab;
    Rigidbody2D playerRB;
    Animator playerAnimator;

    AudioSource shootSound;
    // Disparo

    float cadenciaDeTiro;
    public float delayDeTiro;
    public Vector2 targetP;

    //Movimiento

    public float speed;

    //Vida
    public int playerLife;
    float coolDown;


    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        targetP = new Vector2(0f, -1f);
        playerAnimator = GetComponent<Animator>();
        cadenciaDeTiro = delayDeTiro;
        playerLife = 3;
        shootSound = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        GunshootPrefab.transform.position = gameObject.transform.position;
        PlayerAnimations();
        IsMoving(true);

        if (cadenciaDeTiro <= 0f)
        {
            // Debug.Log("Tiempo");
            playerAnimator.SetBool("ShootA", false);
            playerAnimator.SetBool("ShootB", false);
            playerAnimator.SetBool("ShootFront", false);
            playerAnimator.SetBool("ShootBack", false);

            Shoot();
        }
        else
        {
            cadenciaDeTiro -= Time.deltaTime;
        }
        coolDown -= Time.deltaTime;



    }
    public void IsMoving(bool moving)
    {
        if (playerLife <= 0)
        {
            FindObjectOfType<AudioManager>().Play("SoliderDeath");
            gameObject.SetActive(false);
            Destroy(this);

        }
        if (moving == true)
            playerRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, Input.GetAxisRaw("Vertical") * speed * Time.deltaTime);
        //moving = true;
    }
    void Shoot()
    {

        if (Input.GetKey(KeyCode.F) && Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {

            shootSound.Play();
            ResetTimer();
            if (targetP == Vector2.down)
            {
                playerAnimator.SetBool("ShootBack", false);
                playerAnimator.SetBool("ShootA", false);
                playerAnimator.SetBool("ShootB", false);
                playerAnimator.SetBool("ShootFront", true);
                GunshootPrefab.transform.eulerAngles = new Vector3(0f, 0f, -90f);
                Instantiate(GunshootPrefab);

            }
            else if (targetP == Vector2.up)
            {
                playerAnimator.SetBool("ShootBack", true);
                playerAnimator.SetBool("ShootA", false);
                playerAnimator.SetBool("ShootB", false);
                playerAnimator.SetBool("ShootFront", false);
                GunshootPrefab.transform.eulerAngles = new Vector3(0f, 0f, 90f);
                Instantiate(GunshootPrefab);

            }
            else if (targetP == Vector2.left)
            {
                playerAnimator.SetBool("ShootBack", false);
                playerAnimator.SetBool("ShootFront", false);
                playerAnimator.SetBool("ShootA", false);
                playerAnimator.SetBool("ShootB", true);
                GunshootPrefab.transform.eulerAngles = Vector3.zero;
                Instantiate(GunshootPrefab);

            }
            else if (targetP == Vector2.right)
            {
                playerAnimator.SetBool("ShootBack", false);
                playerAnimator.SetBool("ShootBack", false);
                playerAnimator.SetBool("ShootFront", false);
                playerAnimator.SetBool("ShootB", false);

                playerAnimator.SetBool("ShootA", true);
                GunshootPrefab.transform.eulerAngles = Vector3.zero;
                Instantiate(GunshootPrefab);
            }

        }

    }

    void ResetTimer()
    {

        cadenciaDeTiro = delayDeTiro;
    }
    void newCooldown(float coolDownhit)
    {
        coolDown = coolDownhit;
    }
    void PlayerAnimations()
    {
        playerAnimator.SetBool("isMoving", false);
        playerAnimator.SetBool("Down", false);
        playerAnimator.SetBool("Up", false);
        playerAnimator.SetBool("Left", false);
        playerAnimator.SetBool("Right", false);

        if (Input.GetAxisRaw("Horizontal") == -1)
        {
            GunshootPrefab.GetComponent<SpriteRenderer>().flipX = true;


            playerAnimator.SetBool("isMoving", true);

            playerAnimator.SetBool("Down", false);
            playerAnimator.SetBool("Up", false);
            playerAnimator.SetBool("Right", false);
            playerAnimator.SetBool("Left", true);
            targetP = Vector2.left;
        }
        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            GunshootPrefab.GetComponent<SpriteRenderer>().flipX = false;
            playerAnimator.SetBool("isMoving", true);

            playerAnimator.SetBool("Down", false);
            playerAnimator.SetBool("Up", false);
            playerAnimator.SetBool("Left", false);
            playerAnimator.SetBool("Right", true);
            targetP = Vector2.right;
        }
        if (Input.GetAxisRaw("Vertical") == -1)
        {
            playerAnimator.SetBool("isMoving", true);

            playerAnimator.SetBool("Right", false);
            playerAnimator.SetBool("Up", false);
            playerAnimator.SetBool("Left", false);
            playerAnimator.SetBool("Down", true);
            targetP = Vector2.down;
        }
        if (Input.GetAxisRaw("Vertical") == 1)
        {
            playerAnimator.SetBool("isMoving", true);

            playerAnimator.SetBool("Down", false);
            playerAnimator.SetBool("Left", false);
            playerAnimator.SetBool("Right", false);
            playerAnimator.SetBool("Up", true);
            targetP = Vector2.up;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (coolDown <= 0)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                FindObjectOfType<AudioManager>().Play("SoliderHit");
                newCooldown(2f);
                playerLife--;
                GameManager.instance.UpdateScores();
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (coolDown <= 0)
        { 
            if (collision.gameObject.CompareTag("Enemy"))
            {

                FindObjectOfType<AudioManager>().Play("SoliderHit");
  
            }

         }
    }
}
