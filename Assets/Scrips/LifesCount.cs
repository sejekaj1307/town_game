using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is attached to "Right_wall", "Left_wall" and so on
//It is used to change the players helth when a ball gets past
public class LifesCount : MonoBehaviour {

    public GameObject player;
    private Movement movement;

    void Start()
    {
        movement = player.GetComponent<Movement>();
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Ball")){
            movement.lives--;
        }
    }
}
