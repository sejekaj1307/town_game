using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is attached "Ball" and is used for the ball physics
public class Ball : MonoBehaviour {

    public Rigidbody rb;
    private float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        //Checks if the ball should be removed
        if (transform.position.x < -8f || transform.position.x > 8f || transform.position.z < -8f || transform.position.z > 8f) { Destroy(gameObject); }
        //Checks if the ball is moving to slow and therefor gives it small kicks to get up to speed
        if (rb.velocity.magnitude < 4f) { rb.AddForce(rb.velocity * 15 * Time.deltaTime); }
    }

    void OnCollisionEnter(Collision col)
    {  
        //OBS. the object that the ball hits is hidden under the player!
        //If the ball hits a player, the ball is bounced away with the same speed it came with
        if (col.gameObject.CompareTag("Player") && rb.velocity.magnitude < 15f) {
            speed = rb.velocity.magnitude;
            rb.AddForce(col.transform.forward * (speed*2));
        }
    }
}
