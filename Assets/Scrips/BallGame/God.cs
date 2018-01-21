using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class God : MonoBehaviour {

    public GameObject Ball;
    public GameObject[] players;
    public float spawnRate = 5f;
    public string gameMode = "";


    void Start () {
        players = GameObject.FindGameObjectsWithTag("Player");

        spawnBall(new Vector3(0.5f,0f,0.5f));
        //spawnBall(transform.right);

        //Calls the method "Spawn" after "float spawnRate" seconds 
        InvokeRepeating("Spawn", 0f, spawnRate);
    }
	

	void Update () {
        //Makes it possible to pause the game and start it again by pressing "P"
        if (Input.GetKeyDown(KeyCode.P) && GameObject.FindGameObjectsWithTag("Button").Length == 0)
        {
            if (Time.timeScale == 1) { Time.timeScale = 0; } else { Time.timeScale = 1; }
        }
        
        //If there are no balls left, spawn a new ball
        if(GameObject.FindGameObjectsWithTag("Ball").Length == 0)
        {
            spawnBall(new Vector3(0.5f,0f,0.5f));
        }

        if (gameMode == "FFS")
        {
            print("Gamemode: Free for all!");
        }
        else if(gameMode == "Teams")
        {
            print("Gamemode: Teams of two!");
        }
	}

    void Spawn()
    {
        Rigidbody rb;
        
        //Spawn point is set (should be done from the corners)
        Vector3 pos = new Vector3(transform.position.x,0.25f, transform.position.z);
        //finding a random direction to shoot out the ball
        Vector3 dir = new Vector3(Random.Range(-1f,1f),0, Random.Range(-1f, 1f));
        //Spawns the ball as a rigidbody so a force can be added
        rb = Instantiate(Ball.GetComponent<Rigidbody>(), pos, Quaternion.identity) as Rigidbody;
        rb.gameObject.SetActive(true);
        
        rb.AddForce(dir * 100f);
    }

    //Method to spawn a ball without any random direction
    void spawnBall(Vector3 test)
    {
        GameObject ball = Instantiate(Ball);
        ball.SetActive(true);
        Rigidbody ball_rb = ball.GetComponent<Rigidbody>();
        ball_rb.AddForce(test * 300f);
    }
}
