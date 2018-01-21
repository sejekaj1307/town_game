using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Z_Movement : MonoBehaviour {
    public float speed = 6.0F;
    public float jumpSpeed = 100F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    private Rigidbody rb;
    private bool canJump = false;
    float x_in;
    float z_in;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update () {
        move();
	}

    void move()
    {
        if (canJump) {
            x_in = Input.GetAxis("Horizontal");
            z_in = Input.GetAxis("Vertical");
            moveDirection = new Vector3(x_in, 0, z_in);
        }
        else { moveDirection = new Vector3(x_in, 0, z_in); }

        moveDirection *= speed;
        if (Input.GetButton("Jump") && canJump) { rb.AddForce(transform.up * jumpSpeed); canJump = false; }


        transform.position = transform.position + (moveDirection * Time.deltaTime);

        Vector3 targetDir = (moveDirection+transform.position) - transform.position;
        float step = speed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
        Debug.DrawRay(transform.position, newDir, Color.red);
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.CompareTag("Terrain"))
        {
            canJump = true;
        }
    }

}
