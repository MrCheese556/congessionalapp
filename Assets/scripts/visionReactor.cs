using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class visionReactor : MonoBehaviour
{
    public visionPoliceAgent cp;
     public GameObject target;
     public Vector3 besti;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other){

        if(other.tag == "popo"){
            cp.nextCall();
        }
    }
    void Start(){
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
        transform.position = besti;
    }
}
