using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Make apples fall from tree after some time maybe ? 
public class AppleTree : MonoBehaviour {
    public GameObject preApple;
    public GameObject Apple;

    List<GameObject> unityGameObjects = new List<GameObject>();
    List<GameObject> appels = new List<GameObject>();
    List<int> appleMatureRate = new List<int>();

    private float offsetX = 0.9f;
    private float offsetY = 0.7f;
    private float offsetZ = 0.9f;
    private int maxNumApples;
    
    //Size from center to side
    private float objectSize = 0.1f;
    
    void Start () {

        //Sets the number of apples the tree can produce 
        float scale = transform.parent.localScale.x;
        if (scale < 1.2) maxNumApples = 3;
        else if (scale < 1.4) maxNumApples = 5;
        else if (scale <= 1.5) maxNumApples = 7;
        else if (scale <= 2) maxNumApples = 10;
        else print("Scale must be lower than 2 or the offset wint fit!");

        //Makes sure that the apples are spawned right depending on the scale of the tree
        offsetX -= Mathf.Abs(transform.parent.localScale.x - 2) * 0.4f;
        offsetY -= Mathf.Abs(transform.parent.localScale.y - 2) * 0.4f;
        offsetZ -= Mathf.Abs(transform.parent.localScale.z - 2) * 0.4f;

        //Spawns the first apple
        StartCoroutine(spawnMatureApple());
        objectSize *= 2;
    }
	
	
	void Update () {
        //Use for test - Spawn apples manual
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
        
        //Checks if the new apple is going to overlap another existing apple
        for (int i = 0; i < unityGameObjects.Count; i++) {
            
            float x1 = unityGameObjects[i].transform.position.x;
            float y1 = unityGameObjects[i].transform.position.y;
            float z1 = unityGameObjects[i].transform.position.z;

            //If it overlaps return out of the method and dont spawn the apple
            if (z <= z1 + objectSize && z >= z1 - objectSize && x <= x1 + objectSize && x >= x1 - objectSize && y <= y1 + objectSize && y >= y1 - objectSize)
            {
                return;
            }
        }

        //Spawns the apple and makes it a child of this gameobject 
        GameObject newApple = Instantiate(preApple,new Vector3(x,y,z),transform.rotation);
        newApple.transform.parent = gameObject.transform;
        unityGameObjects.Add(newApple);

        //Adds a random time it will take for the apple to mature  - REMEMVER THAT THE LAST INT IS NOT INCLUSIVE!
        appleMatureRate.Add(Random.Range(5,11));
        
    }

    //Ages the apple
    IEnumerator spawnMatureApple()
    {

        if (transform.childCount < maxNumApples) { SpawnApples(); }

        //waits 2 seconds 
        yield return new WaitForSeconds(2);

        //If there is an apple that is not yet mature, age it by 1 
        //Also checks if it is mature and adds a mature apple if this is the case 
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
        //If the players takes an apple the array is updated
        for (int i = 0; i < appels.Count; i++) { if (appels[i] == null) { appels.RemoveAt(i); } }

        //Run the same method again so it is a infinit loop 
        StartCoroutine(spawnMatureApple());
    }


}
