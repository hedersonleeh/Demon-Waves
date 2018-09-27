using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballBehaviur : MonoBehaviour
{

    Transform player;
    Vector2 target;
    public float speed = .5f;
    public float timer;
    Rigidbody2D fireballRB;

    // Use this for initialization
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("MosterFire");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if(player == null)
        {
            Destroy(gameObject);
        }
        target = new Vector2(player.position.x, player.position.y);

        timer = 2f;

    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            return;
        }
        else
        {
            transform.RotateAround(transform.position, Vector3.forward, 100 * Time.deltaTime);

            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            if (transform.position.x == target.x && transform.position.y == target.y)
                Destroy(gameObject);
        }
      
    }
    private void OnTriggerExit2D(Collider2D collision)
        {
        if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.lifePlayer.playerLife--;
            GameManager.instance.UpdateScores();
            Destroy(gameObject);
        }
    }
}

