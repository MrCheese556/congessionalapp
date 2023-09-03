using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class callReactor : MonoBehaviour
{
    public controlPoliceAgent cp;
    
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other){
        Debug.Log("works");
        if(other.tag == "popo"){
            cp.nextCall();
        }
    }
}
