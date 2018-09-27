using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {
    public float speed = 20;
    Vector2 target;
   
    Rigidbody2D bulletRB;


	// Use this for initialization
	void Start () {
        bulletRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().targetP;
    }

    // Update is called once per frame
    void Update() {

        bulletRB.AddForce(target * speed * Time.deltaTime);
        if (Input.GetAxisRaw("Horizontal") == -1)
        {
            bulletRB.AddForce(target * speed * Time.deltaTime);

        }
        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            bulletRB.AddForce(target * speed * Time.deltaTime);
        }
        if (Input.GetAxisRaw("Vertical") == -1)
        {
            bulletRB.AddForce(target * speed * Time.deltaTime);
        }
        if (Input.GetAxisRaw("Vertical") == 1)
        {
            bulletRB.AddForce(target * speed * Time.deltaTime);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
