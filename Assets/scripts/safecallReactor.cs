using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class safecallReactor : MonoBehaviour
{
    public safevisionPoliceAgent cp;
    
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other){
        
        if(other.tag == "popo"){
            cp.nextCall();
        }
    }
}
