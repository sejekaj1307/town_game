using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Z_Movement : MonoBehaviour {
    public LayerMask interactionMask;
    Camera cam;

    public delegate void OnFocusChanged(Interactable newFocus);
    public OnFocusChanged onFocusChangedCallback;

    public Interactable focus;	// Our current focus: Item, Enemy etc.

    public float life = 10;
    public float maxHealth;

    public float speed = 6.0F;
    public float turnSpeed = 10F;
    public float jumpSpeed = 100F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    Vector3 newDir;
    private Rigidbody rb;
    private bool canJump = false;
    float x_in;
    float z_in;



    public float offset = 0.0f;

    void Start()
    {
        maxHealth = life;
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
    }
    void Update () {
        move();
        if (transform.position.y < -0.05) transform.position = new Vector3(transform.position.x,-0.045f,transform.position.z);

        if (Input.GetMouseButtonDown(1))
        {
            // Shoot out a ray
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // If we hit
            if (Physics.Raycast(ray, out hit, 100f, interactionMask))
            {
                SetFocus(hit.collider.GetComponent<Interactable>());
            }
        }
    }


    void SetFocus(Interactable newFocus)
    {
        if (onFocusChangedCallback != null)
            onFocusChangedCallback.Invoke(newFocus);

        // If our focus has changed
        if (focus != newFocus && focus != null)
        {
            // Let our previous focus know that it's no longer being focused
            focus.OnDefocused();
        }

        // Set our focus to what we hit
        // If it's not an interactable, simply set it to null
        focus = newFocus;

        if (focus != null)
        {
            // Let our focus know that it's being focused
            focus.OnFocused(transform);
        }

    }

    void move()
    {
        x_in = Input.GetAxis("Horizontal");
        z_in = Input.GetAxis("Vertical");
        if (canJump) {
            
            moveDirection = new Vector3(x_in, 0, z_in);
        }
        else { moveDirection = new Vector3(x_in, 0, z_in); }

        moveDirection *= speed;
        if (Input.GetButton("Jump") && canJump) { rb.AddForce(transform.up * jumpSpeed); canJump = false; }


        transform.position = transform.position + (moveDirection * Time.deltaTime);

        Vector3 targetDir = (moveDirection+transform.position) - transform.position;
        float step = speed * Time.deltaTime*turnSpeed;
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow)) { newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F); }
        
        Debug.DrawRay(transform.position, newDir, Color.red);
        //transform.rotation = Quaternion.LookRotation(newDir);
        PlayerRotation();
    }

    void PlayerRotation()
    {
        Camera c = Camera.main;
        Vector3 dirrection = new Vector3((Input.mousePosition.x - c.pixelWidth / 2) / c.pixelWidth,0f,(Input.mousePosition.y - c.pixelHeight / 2) / c.pixelHeight);
        //print((Input.mousePosition.x - c.pixelWidth / 2) / c.pixelWidth + " " + (Input.mousePosition.y - c.pixelHeight / 2) / c.pixelHeight);
        //print(dirrection);
        //Vector3 test1 = Vector3.RotateTowards(transform.position, dirrection + transform.position, 20f*Time.deltaTime, 0.0F);
        transform.LookAt(dirrection + transform.position);
    }

    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.CompareTag("Terrain"))
        {
            canJump = true;
        }
    }

    public void Heal(int healthGain)
    {
        if (life + healthGain <= maxHealth)
            life += healthGain;
        else
            life = maxHealth;
    }
}
