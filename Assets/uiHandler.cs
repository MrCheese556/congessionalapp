using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiHandler : MonoBehaviour
{
    public GameObject numParent;
    public GameObject roadParent;
    public GameObject numButton;
    public GameObject roadButton;
    public Color senabled;
    public Color disabled;
    // Start is called before the first frame update
    public void Enablenums(){
        if(numParent.activeSelf){
            numParent.SetActive(false);
            numButton.GetComponent<Graphic>().color =disabled;
        } else {
            numParent.SetActive(true);
            numButton.GetComponent<Graphic>().color = senabled;
        }
        
    }
    public void Enableroad(){
        if(roadParent.transform.GetChild(0).gameObject.layer == 0){
             foreach (Transform child in roadParent.transform)
                {
                 child.gameObject.layer = 7;
                }
            roadButton.GetComponent<Graphic>().color =disabled;
        } else {
            foreach (Transform child in roadParent.transform)
                {
                 child.gameObject.layer = 0;
                }
            roadButton.GetComponent<Graphic>().color = senabled;
        }
        
    }
}
