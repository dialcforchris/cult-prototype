using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour 
{
    public Transform[] targets;
    int targetsIndex=0;
    public float speed = 5;
    Vector2[] path;
    int targetIndex;
    bool finishedPath = true;

       
    private void OnPathFound(Vector2[] newPath, bool pathSuccess)
    {
     if (pathSuccess)
     {
         path = newPath;
         StopCoroutine("FollowPath");
         StartCoroutine("FollowPath");

     }
    }
    void Update()
{
    SelectTarget();
}
    void SelectTarget()
    {
        if (finishedPath)
        {
            PathRequestManager.RequestPath(transform.position, targets[targetsIndex].position, OnPathFound);
            targetsIndex++;
            if (targetsIndex>targets.Length-1)
            {
                targetsIndex = 0;
            }
            finishedPath = false;
        }
    }
    IEnumerator FollowPath()
    {
        Vector2 currentWaypoint = path[0];
        while(true)
        {
            if (Vector2.Distance(transform.position,currentWaypoint)<0.02f)
           {
                targetIndex++;
                if (targetIndex>=path.Length)
                {
                    finishedPath = true;
                    targetIndex = 0;
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }
            transform.position = Vector2.MoveTowards(transform.position, currentWaypoint, speed*Time.deltaTime);
            yield return null;
        }

    }
    public void OnDrawGizmos()
    {
        if (path!=null)
        {
            for (int i=targetIndex;i<path.Length;i++)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawCube(path[i], Vector3.one);
                if (i==targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
}
