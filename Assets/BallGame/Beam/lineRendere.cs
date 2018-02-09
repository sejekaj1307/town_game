using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineRendere : MonoBehaviour {
    public Transform startPoint;
    public Transform endPoint;
    public float width = 0.3f;
    LineRenderer laserLine;
	// Use this for initialization
	void Start () {
        laserLine = GetComponent<LineRenderer>();
        //laserLine.SetWidth(width, width);
        laserLine.startWidth = width;
        laserLine.endWidth = width;
    }
	
	// Update is called once per frame
	void Update () {
        laserLine.SetPosition(0, startPoint.position);
        laserLine.SetPosition(1, endPoint.position);
	}
}
