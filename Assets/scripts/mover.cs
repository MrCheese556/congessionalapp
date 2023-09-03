using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mover : MonoBehaviour
{

    public GameObject target;
    public Vector3 besti;
    public int day;
   // public int hour  = 0;
    // Start is called before the first frame update
    void Start()
    {
        day = 0;
        run();
    }

    // Update is called once per frame
    public void run() {
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
