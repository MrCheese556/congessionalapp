using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;

public class pinHandler : MonoBehaviour
{
    public Vector3 screenPosition;
    public Vector3 worldPosition;
    public float zvalue;
    public Object pin;
    public Transform parent;
    public ad ad;
    public GameObject selected;
    public Material mat;
    public Material defmat;
    public TMP_InputField latitudeText;
    public TMP_InputField longitudeText;
    public bool specificOn = true;
    public GameObject hourDropdown;
    public GameObject dayDropdown;
    public GameObject callParent;
    public TMP_Dropdown specificity;
    public placeHandler ph;

    void Start(){

    }
    void Update()
    {
        screenPosition = Input.mousePosition;
        screenPosition.z = zvalue;

        worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);


        transform.position = worldPosition;
        if (Input.GetMouseButtonDown(1)){
            Vector3 v= new Vector3(transform.position.x, transform.position.y, transform.position.z -2f);
            GameObject p = Instantiate(pin, v,Quaternion.identity, parent) as GameObject;
            p.name = "Pin (" + (parent.childCount-1) + ")";
            ad.plays();
            Select(parent.childCount-1);
           
        }
    }
    public void Delete(){
        string n = Regex.Replace(selected.name, "[^0-9]", "");
        int c = int.Parse(n);
        Destroy(selected);
        Destroy(callParent.transform.Find("Sphere (" + (callParent.transform.childCount-1) + ")").gameObject);
        Select(c-1);
    }
    public void Move(){
        double longLeft = -75.2752198;
        double longRight =  -74.9795010;
        
        double latBottom = 40.16080762;
        double latTop =  40.42902459;
        
        double xPerc = (longLeft-double.Parse(longitudeText.text))/(longLeft - longRight);
        double yPerc = (double.Parse(latitudeText.text)-latBottom)/(latTop - latBottom);

        
        float a = (float)(68.47 + (xPerc * (13.67-68.47)));
        float b = (float)(11.38 - (yPerc * (11.38 + 52.68)+2.5));
        Vector3 s = new Vector3(a,selected.transform.localPosition.y,b);
         selected.transform.localPosition = s;
         callParent.transform.Find("Sphere (" + (callParent.transform.childCount-1) + ")").position = selected.transform.position;
         callParent.transform.Find("Sphere (" + (callParent.transform.childCount-1) + ")").gameObject.GetComponent<mover>().run();
    }
    public void Select(int n){
        if(parent.childCount > 0){
            specificity.interactable = false;
        } else{
            specificity.interactable = true;
        }
     //Debug.Log(callParent.transform.Find("Sphere (" + (callParent.transform.childCount-1) + ")").gameObject.GetComponent<mover>().day);
        if(selected != null)
        {
            selected.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Renderer>().sharedMaterial = defmat;
        }
        

        selected = parent.Find("Pin (" + n + ")").gameObject;
        selected.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Renderer>().sharedMaterial = mat;

        double xPerc = (68.47-selected.transform.localPosition.x)/(68.47 - 13.67);
        double yPerc = (11.38-selected.transform.localPosition.z -3.5)/(11.38 + 52.68);

        double longLeft = -75.2752198;
        double longRight =  -74.9795010;
        
        double latBottom = 40.16080762;
        double latTop =  40.42902459;

        double longitude = longLeft + (xPerc * (longRight-longLeft));
        double latitude = latBottom + (yPerc * (latTop-latBottom));
        longitudeText.text = longitude.ToString();
        latitudeText.text = latitude.ToString();
        //Debug.Log(latitude+ "," + longitude);

        dayDropdown.GetComponent<TMP_Dropdown>().value = callParent.transform.Find("Sphere (" + n + ")").gameObject.GetComponent<mover>().day;
        
        //40.315682, -75.201839 bottom of peace valley

        //  longitude to right -75.27521988523509  -74.97950108268826
        //latitude to up 40.160807627796565,  40.429024594803735
    
    }
    public void TimeChange(){
        if(specificOn){
            specificOn = false;
           hourDropdown.SetActive(false);
            dayDropdown.SetActive(false);
             
        } else{
            specificOn = true;
            hourDropdown.SetActive(true);
            dayDropdown.SetActive(true);
        }
    }
}
