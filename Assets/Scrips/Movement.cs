using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour {
    [Header("GameObjects")]
    public GameObject ball;
    public GameObject death_wall;
    public GameObject LaserBeam;
    public Text text_lives;

    [Space]

    [Header("Properties")]
    public bool isAI = true;
    public float moveSpeed;
    public int lives = 10;

    private GameObject closetsBall;
    private GameObject[] balls;
    private Vector3 startPos;
    private bool updateArray = false;

	void Start () {
        startPos = transform.position;
        updateBallArray();
        moveSpeed = 2;
	}
	
	void Update () {
        //Displays the players health
        text_lives.text = gameObject.name + " lives: " + lives.ToString();

        //If lives > 0 player can move, else set "Death_wall" true and stop all movement
        if (lives > 0)
        {
            //Checks if a ball has been added or removed from the game and the updates the array with Balls
            for (int i = 0; i < balls.Length; i++)
            {
                if (balls[i].gameObject == null) { updateArray = true; }
            }
            if (ball == null || updateArray || GameObject.FindGameObjectsWithTag("Ball").Length > balls.Length)
            {
                updateBallArray();
            }
            else
            {
                findClosetsBall();
                if (!isAI) { move(); }
                else { ai(); }
            }
        }
        else {
            Collider col = death_wall.GetComponent<Collider>();
            col.enabled = true;
            LaserBeam.SetActive(true);
        }
    }

    //Updates the array and sets the closets ball equels the varible "ball"
    void updateBallArray()
    {
        balls = GameObject.FindGameObjectsWithTag("Ball");
        if (balls.Length != 0)
        {
            ball = balls[0];
        }
        else { ball = null;}
        updateArray = false;
    }

    //Keeps updating witch ball that is closets 
    void findClosetsBall()
    {
        //Sets a random really high dist. Checks all the balls to find the closets 
        float dist = 100f;
        for(int i = 0; i < balls.Length; i++)
        {
            float newDist = Vector3.Distance(balls[i].transform.position, transform.position);
            if (dist > newDist)
            {
                dist = newDist;
                closetsBall = balls[i].gameObject;
            }
        }
        ball = closetsBall;
    }

    //This method controlls the ai movement
    void ai()
    {
        //Top_enemy
        if ((startPos.z < -4f || startPos.z > 4f) && startPos.x == 0 && balls[0] != null)
        {
            Vector3 pos = new Vector3(ball.transform.position.x, transform.position.y, transform.position.z);
            if (pos.x > -2.9f && pos.x < 2.9f) { /*transform.position = pos;*/
                float step = moveSpeed * Time.deltaTime * 5;
                transform.position = Vector3.MoveTowards(transform.position, pos, step);
            }
        } 
        //Left_enemy and player_2 if "isAI == true"
        else if(balls[0] != null)
        {
            Vector3 pos = new Vector3(transform.position.x, transform.position.y, ball.transform.position.z);
            if (pos.z > -3.16f && pos.z < 3.16f) { /*transform.position = pos; */
                float step = moveSpeed * Time.deltaTime * 5;
                transform.position = Vector3.MoveTowards(transform.position, pos, step);
            }
        }
    }

    void move()
    {
        //Player_1
        if (Input.GetKey(KeyCode.RightArrow) && gameObject.name == "Player_1" && transform.position.x < 3f)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow) && gameObject.name == "Player_1" && transform.position.x > -3f)

        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
        //Player_2
        if (Input.GetKey(KeyCode.W) && gameObject.name == "Player_2" && transform.position.z < 2.8f)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S) && gameObject.name == "Player_2" && transform.position.z > -3f)

        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
    }
}
