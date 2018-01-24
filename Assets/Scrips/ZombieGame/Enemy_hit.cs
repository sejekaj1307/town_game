using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy_hit : MonoBehaviour {
    public int lives = 100;
    private Rigidbody rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        print(lives);	
	}

    private void OnParticleCollision(GameObject other)
    {
        rb.AddForce(transform.right * 100f);
        lives--;

    }

}
