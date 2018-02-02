using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour {
    public GameObject preApple;
    public GameObject Apple;

    List<GameObject> unityGameObjects = new List<GameObject>();
    List<GameObject> appels = new List<GameObject>();
    List<int> appleMatureRate = new List<int>();

    private float offsetX = 1;
    private float offsetZ = 1;
    
    //Size from center to side
    private float objectSize = 0.1f;
    
    void Start () {
        StartCoroutine(spawnMatureApple());
        objectSize *= 2;
    }
	
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space) && transform.childCount < 10) { SpawnApples(); }
    }

    

    void SpawnApples()
    {
        float x = transform.position.x;
        float z = transform.position.z;
        if(Random.Range(1, 3) == 1)
        {
            if(Random.Range(1, 3) == 1) { x += offsetX; } else { x -= offsetX; }
            z += Random.Range(-offsetZ,offsetZ);
        }
        else
        {   
            if (Random.Range(1, 3) == 1) { z += offsetZ; } else { z -= offsetZ; }
            x += Random.Range(-offsetX, offsetX);
        }
        
        for (int i = 0; i < unityGameObjects.Count; i++) {
            float x1 = unityGameObjects[i].transform.position.x;
            float z1 = unityGameObjects[i].transform.position.z;

            if (z <= z1 + objectSize && z >= z1 - objectSize && x <= x1 + objectSize && x >= x1 - objectSize)
            {
                print("They overlap");
                //if () { }
            }
        }

        GameObject newApple = Instantiate(preApple,new Vector3(x,1f,z),transform.rotation);
        newApple.transform.parent = gameObject.transform;
        unityGameObjects.Add(newApple);
        appleMatureRate.Add(Random.Range(5,11));
        
    }

    IEnumerator spawnMatureApple()
    {

        yield return new WaitForSeconds(2);
        if (unityGameObjects.Count > 0 && appleMatureRate.Count > 0)
        {
            for (int i = 0; i < appleMatureRate.Count; i++)
            {

                if (appleMatureRate[i] > 0) { appleMatureRate[i]--; /* print("Apple_" + i + " : " + appleMatureRate[i]); */}
                else
                {
                    GameObject Apple1 = Instantiate(Apple, new Vector3(unityGameObjects[i].transform.position.x, 1f, unityGameObjects[i].transform.position.z), transform.rotation);
                    Apple1.transform.parent = gameObject.transform;
                    appels.Add(Apple1);
                    appleMatureRate.Remove(i);
                    Destroy(unityGameObjects[i]);
                    unityGameObjects.RemoveAt(i);
                    break;
                }

            }
        }
        StartCoroutine(spawnMatureApple());
    }


}
