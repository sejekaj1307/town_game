using System;
using UnityEngine;
using System.Collections.Generic;

public class PathRequestManager : MonoBehaviour {

    Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
    PathRequest currentPathRequest;

    PathFinding pathfinding;

    bool isProcessingPath;
    static PathRequestManager instance;

    void Awake() {
        instance = this;
        pathfinding = GetComponent<PathFinding>();
    }

    public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback) {
        //Nyt request
        PathRequest newRequest = new PathRequest(pathStart, pathEnd, callback);
        //Tilføjer til en kø:
        instance.pathRequestQueue.Enqueue(newRequest);
        instance.TryProcessNext();
    }
    //Tjekker om vi kan gå igang med den næste
    void TryProcessNext() {
        if (!isProcessingPath && pathRequestQueue.Count > 0) {
            //Får første ting i vores kø og tager det ud:
            currentPathRequest = pathRequestQueue.Dequeue();
            isProcessingPath = true;
            pathfinding.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);
        }
    }

    public void FinishedProcessingPath(Vector3[] path, bool success) {
        currentPathRequest.callback(path, success);
        isProcessingPath = false;
        TryProcessNext();
    }

    struct PathRequest {
        public Vector3 pathStart;
        public Vector3 pathEnd;
        public Action<Vector3[], bool> callback;

        public PathRequest(Vector3 start, Vector3 end, Action<Vector3[], bool> _callback) {
            pathStart = start;
            pathEnd = end;
            callback = _callback;
        }
    }
}
