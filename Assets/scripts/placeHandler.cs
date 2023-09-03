using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class placeHandler : MonoBehaviour
{
    public Vector3 screenPosition;
    public Vector3 worldPosition;
    public Object prefab;
    public GameObject parent;
    public float zvalue;
    public GameObject target;
    //public pinHandler ph;
    public GameObject callParent;
    public GameObject dayButton;
    public GameObject hourButton;
   // public int[0] dayTable;

    void Update()
    {
        screenPosition = Input.mousePosition;
        screenPosition.z = zvalue;

        worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);


        transform.position = worldPosition;

        if (Input.GetMouseButtonDown(1)){
            if(callParent.transform.childCount != 0){
                timeDay();
            }
            GameObject g  = Instantiate(prefab, transform.position, Quaternion.identity, parent.transform) as GameObject;
            g.SetActive(true);
            g.name = "Sphere (" + (parent.transform.childCount-1) + ")";
            g.GetComponent<mover>().target = target;
            g.layer = 7;
        }
    }
    public void timeDay(){
        //Debug.Log(callParent.transform.childCount-1 + ", " + dayButton.GetComponent<TMP_Dropdown>().value);
        callParent.transform.Find("Sphere (" + (callParent.transform.childCount-1) + ")").gameObject.GetComponent<mover>().day = dayButton.GetComponent<TMP_Dropdown>().value;
    }
    // public void sdo(){
    //     callParent.transform.Find("Sphere (" + (callParent.transform.childCount-1) + ")").gameObject.GetComponent<mover>().day = dayButton.GetComponent<TMP_Dropdown>().value;
    // }

    // public void timeHour(int n){
    //     callParent.transform.Find("Sphere (" + (callParent.transform.childCount-1) + ")").gameObject.GetComponent<mover>().hour = hourButton.GetComponent<TMP_Dropdown>().value;
    // }
}
