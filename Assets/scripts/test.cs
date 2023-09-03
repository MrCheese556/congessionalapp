using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public visionNavMesh vs;
    public GameObject target;
    public float k;
    public Vector3 location;
    public Vector3 besti;

    public void OnDrawGizmos()
    {
        var collider = target.GetComponent<Collider>();
        BoxCollider[] children;
        children = target.GetComponentsInChildren<BoxCollider>();
        float best = 1000000f;
        besti = new Vector3(0,0,0);
        
        for(int i=0;i<children.Length - 1;i++){
            Vector3 closestPoint = children[i].ClosestPoint(transform.position);
            float length = Vector3.Distance(closestPoint, transform.position);
            best = Mathf.Min(best, length);
            if(best == length){
                besti = closestPoint;
            }
        }
       

        Gizmos.DrawSphere(transform.position, 0.1f);
        Gizmos.DrawWireSphere(besti, 0.4f);
    }
    void Update(){
       // k = vs.GetPathLength(besti);
        vs.movePositionTransform = besti;
    } 
}
