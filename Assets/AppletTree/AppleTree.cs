using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour {
    public GameObject preApple;
    public GameObject Apple;

    List<GameObject> unityGameObjects = new List<GameObject>();
    List<GameObject> appels = new List<GameObject>();
    List<int> appleMatureRate = new List<int>();

    private float offsetX = 0.9f;
    private float offsetY = 0.7f;
    private float offsetZ = 0.9f;
    
    
    //Size from center to side
    private float objectSize = 0.1f;
    
    void Start () {
        StartCoroutine(spawnMatureApple());
        objectSize *= 2;
    }
	
	
	void Update () {
        //if (Input.GetKeyDown(KeyCode.Space) && transform.childCount < 10) { SpawnApples(); }
    }

    

    void SpawnApples()
    {
        float x = transform.position.x;
        float y = transform.position.y + Random.Range(-offsetY,offsetY);
        float z = transform.position.z;
        if(Random.Range(1, 3) == 1)
        {
            if(Random.Range(1, 3) == 1) { x += offsetX; } else { x -= offsetX; }
            z += Random.Range(-offsetZ + objectSize, offsetZ - objectSize);
        }
        else
        {   
            if (Random.Range(1, 3) == 1) { z += offsetZ; } else { z -= offsetZ; }
            x += Random.Range(-offsetX + objectSize , offsetX - objectSize );
        }
        
        for (int i = 0; i < unityGameObjects.Count; i++) {
            float x1 = unityGameObjects[i].transform.position.x;
            float y1 = unityGameObjects[i].transform.position.y;
            float z1 = unityGameObjects[i].transform.position.z;

            if (z <= z1 + objectSize && z >= z1 - objectSize && x <= x1 + objectSize && x >= x1 - objectSize && y <= y1 + objectSize && y >= y1 - objectSize)
            {
                //print("They overlap");
                return;
                /*print(transform.position.x + offsetX + " : " + x);
                if (x != transform.position.x + offsetX && x != transform.position.x - offsetX) { 
                    if (x + objectSize < transform.position.x + offsetX && (x + objectSize < x1 - objectSize || x + objectSize > x1 + objectSize )) { print("0.1+x _ " + x); x += objectSize; }
                    else { print("0.1-x"); x -= objectSize; }
                }
                else {
                    if (z + objectSize < transform.position.z + offsetZ && (z + objectSize < z1 - objectSize || z + objectSize > z1 + objectSize)) { print("0.1+z _ " + z); z += objectSize; }
                    else { print("0.1-z"); z -= objectSize; }
                }*/
            }
        }

        GameObject newApple = Instantiate(preApple,new Vector3(x,y,z),transform.rotation);
        newApple.transform.parent = gameObject.transform;
        unityGameObjects.Add(newApple);
        appleMatureRate.Add(Random.Range(5,11));
        
    }

    IEnumerator spawnMatureApple()
    {
        if (transform.childCount < 10) { SpawnApples(); }
        yield return new WaitForSeconds(2);
        if (unityGameObjects.Count > 0 && appleMatureRate.Count > 0)
        {
            for (int i = 0; i < unityGameObjects.Count; i++)
            {

                if (appleMatureRate[i] > 0) { appleMatureRate[i]--; /*print("Apple_" + i + " : " + appleMatureRate[i]); */}
                else if (i - 1 < unityGameObjects.Count)
                {
                    GameObject Apple1 = Instantiate(Apple, new Vector3(unityGameObjects[i].transform.position.x, unityGameObjects[i].transform.position.y, unityGameObjects[i].transform.position.z), transform.rotation);
                    Apple1.transform.parent = gameObject.transform;
                    appels.Add(Apple1);
                    appleMatureRate.RemoveAt(i);
                    Destroy(unityGameObjects[i]);
                    unityGameObjects.RemoveAt(i);
                    break;
                }
                
            }
        }
        StartCoroutine(spawnMatureApple());
    }


}
