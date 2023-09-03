using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ideaNavMesh : MonoBehaviour
{ 
    //[SerializeField] public Vector3 movePositionTransform;
    private NavMeshAgent nav;
    // Start is called before the first frame update
    private void Awake() {
        nav = GetComponent<NavMeshAgent>();
    }

    private void Update() {
      // nav.destination = movePositionTransform;
    }

       
    public float GetPathLength(Vector3 from ,Vector3 pos)
    {
        NavMeshPath Path = new NavMeshPath();

            if (NavMesh.CalculatePath(from, pos, NavMesh.AllAreas, Path))
            {
                float distance = Vector3.Distance(from, Path.corners[0]);

                for (int j = 1; j < Path.corners.Length; j++)
                {
                    distance += Vector3.Distance(Path.corners[j - 1], Path.corners[j]);
                    
                }

                 return distance;
            } else {
                 return 100f;
            }
           
    }
//     public float CalculatePathLength (Vector3 targetPosition)
//     {
//     // Create a path and set it based on a target position.
//     NavMeshPath path = new NavMeshPath();
//     if(nav.enabled)
//         nav.CalculatePath(targetPosition, path);
    
//     // Create an array of points which is the length of the number of corners in the path + 2.
//     Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];
    
//     // The first point is the enemy's position.
//     allWayPoints[0] = transform.position;
    
//     // The last point is the target position.
//     allWayPoints[allWayPoints.Length - 1] = targetPosition;
    
//     // The points inbetween are the corners of the path.
//     for(int i = 0; i < path.corners.Length; i++)
//     {
//         allWayPoints[i + 1] = path.corners[i];
//     }
    
//     // Create a float to store the path length that is by default 0.
//     float pathLength = 0;
    
//     // Increment the path length by an amount equal to the distance between each waypoint and the next.
//     for(int i = 0; i < allWayPoints.Length - 1; i++)
//     {
//         pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
//     }
    
//     return pathLength;
// }
}
