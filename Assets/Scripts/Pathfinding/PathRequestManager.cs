using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PathRequestManager : MonoBehaviour 
{
    Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
    PathRequest currentPathRequest;
    Pathfinding pathFinding;
    bool isProcessing;

   public static PathRequestManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        pathFinding = GetComponent<Pathfinding>();
    }

    public static void RequestPath(Vector2 _start,Vector2 _end,Action<Vector2[],bool>callback)
    {
        PathRequest newPathRequest = new PathRequest(_start, _end, callback);
        instance.pathRequestQueue.Enqueue(newPathRequest);
        instance.TryProcessNext();
    }

     void TryProcessNext()
    {
         if (!isProcessing && pathRequestQueue.Count>0)
         {
             currentPathRequest = pathRequestQueue.Dequeue();
             isProcessing = true;
             pathFinding.StartFindPath(currentPathRequest.pathStart,currentPathRequest.pathEnd);
         }
    }
    public void FinishedProcessingPath(Vector2[] path, bool success)
     {
         currentPathRequest.callback(path, success);
         isProcessing = false;
         TryProcessNext();
     }
    struct PathRequest
    {
        public Vector2 pathStart;
        public Vector2 pathEnd;
        public Action<Vector2[], bool> callback;
        public PathRequest(Vector2 _start, Vector2 _end, Action<Vector2[], bool> _callback)
        {
            pathStart = _start;
            pathEnd = _end;
            callback = _callback;
        }

    }
}
