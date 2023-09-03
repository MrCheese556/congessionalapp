using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fivecallReactor : MonoBehaviour
{
    public fivecarPoliceAgent cp;
    
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other){
        Debug.Log("works");
        if(other.tag == "popo"){
            cp.nextCall();
        }
    }
}
