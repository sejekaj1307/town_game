using UnityEngine;
using System.Collections;

public class Node : IHeapItem<Node>{

    public bool walkable; 
    public Vector3 worldPos;
    public int gridX;
    public int gridY; 

    public int gCost; // cost to start node
    public int hCost; // Cost to end node
    public Node parent; // Skal vide hvor den kom fra
    int heapIndex; //Kende site index i heapet

    public Node(bool walkable, Vector3 worldPos, int gridX, int gridY) {
        this.walkable = walkable;
        this.worldPos = worldPos;
        this.gridX = gridX;
        this.gridY = gridY;
    }

    //Total cost, lower is better
    public int fCost {
        get{
            return gCost + hCost;
        }
    }

    public int HeapIndex {
        get {
            return heapIndex;
        }
        set {
            this.heapIndex = value;
        }
    }

    public int CompareTo(Node nodeToCompare) {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if(compare == 0){
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }
        return -compare;
    }
}
