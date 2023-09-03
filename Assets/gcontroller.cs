using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class gcontroller : MonoBehaviour
{
    public GameObject callParent;
    public GameObject pinParent;
    public GameObject[] dayControllers;
    public GameObject[] dayPins;
    public int[][] results;
    public int carNum;
    public Transform carParent;
    public TMP_Dropdown s;
    public TMP_InputField lat;
    public TMP_InputField longi;
    public TMP_InputField maps;
    public int carSelected;
    public Material sel;
    public Material desel;
    public GameObject picker;
    public GameObject resulter;
    public TMP_InputField enterer;
    public string textt;
    public GameObject loading;
    public pinHandler ph;
    public placeHandler plh;
    public calculator thisone;

    public void Start(){

        textt = maps.text;
        maps.onValueChanged.AddListener(s => maps.text = textt);
        carNum = int.Parse(enterer.text);

    }
    public void sfddf(){
        carNum = int.Parse(enterer.text);
    }
    public void Calculate(){
        
        if(ph.specificOn){
            results = new int[7][];
            for(int i= callParent.transform.childCount-1 ;i>=0;  i--){
                //Debug.Log(i);
                callParent.transform.GetChild(i).gameObject.name = "Sphere (" + dayControllers[callParent.transform.GetChild(i).gameObject.GetComponent<mover>().day].transform.childCount + ")";
                pinParent.transform.GetChild(i).SetParent(dayPins[callParent.transform.GetChild(i).gameObject.GetComponent<mover>().day].transform);
                callParent.transform.GetChild(i).SetParent(dayControllers[callParent.transform.GetChild(i).gameObject.GetComponent<mover>().day].transform);
                
                
            }
            for(int j = 0; j <7; j++){
                dayControllers[j].GetComponent<calculator>().numClusters = carNum;
                results[j]  = dayControllers[j].GetComponent<calculator>().redo();
                //Debug.Log(results[j][0]);
            }
            viewDayResults();
        } else {
            results = new int[1][];
            thisone.numClusters = carNum;
            results[0] = thisone.redo();
            for(int i = 0; i < carNum; i++){
                carParent.Find("Sphere (" + (results[0][i]+83) + ")").gameObject.SetActive(true);
            }
            carSelected = carNum -1;
            SelectBackward();
        }
       
        picker.SetActive(false);
        resulter.SetActive(true);
        loading.SetActive(true);
        Invoke("fsa", 3f);  
         ph.enabled = false;
        plh.enabled = false;
    }
    public void fsa(){
        loading.SetActive(false);
    }
    public void viewDayResults(){
        int d = s.value;
        lat.text = "";
        longi.text = "";
        textt = "";
        maps.text = textt;
        for(int i = 0; i < 7; i++){
            
            if(i == d){
                dayControllers[i].SetActive(true);
                dayPins[i].SetActive(true);
            } else {
                dayControllers[i].SetActive(false);
                dayPins[i].SetActive(false);
            }
        }
        for(int j = 0; j < carParent.childCount; j++){
            carParent.GetChild(j).gameObject.SetActive(false);
        }
        for(int u = 0; u < carNum; u++){
            if(results[d][u] == -1){
                Debug.Log(d);
                //break;
            } else{
                carParent.Find("Sphere (" + (results[d][u]+83) + ")").gameObject.SetActive(true);
            }
        }
        carSelected = carNum -1;
        SelectForward();
    }
    public void SelectForward(){
        Debug.Log(carSelected);
        if(results[s.value][carSelected] == -1){
            
            return;
        }
        carParent.Find("Sphere (" + (results[s.value][carSelected] + 83) + ")").gameObject.GetComponent<Renderer>().sharedMaterial = desel;

        if(carSelected == carNum -1){
            carSelected = 0;
        } else{
            carSelected++;
        }

        GameObject selected = carParent.Find("Sphere (" + (results[s.value][carSelected] + 83) + ")").gameObject;

        selected.GetComponent<Renderer>().sharedMaterial = sel;

        double xPerc = (27.51-selected.transform.localPosition.x)/(27.51 + 27.51);
        double yPerc = (32.2-selected.transform.localPosition.z)/(32.2 + 32.14);

        double longLeft = -75.2752198;
        double longRight =  -74.9795010;
        
        double latBottom = 40.16080762;
        double latTop =  40.42902459;

        double longitude = longLeft + (xPerc * (longRight-longLeft));
        double latitude = latBottom + (yPerc * (latTop-latBottom));
        lat.text = longitude.ToString();
        longi.text = latitude.ToString();
        textt = "https://www.google.com/maps/place/" + latitude + ","+longitude;
        maps.text = textt;
    }
    public void SelectBackward(){
        if(results[s.value][carSelected] == -1){
            return;
        }
        carParent.Find("Sphere (" + (results[s.value][carSelected] + 83) + ")").gameObject.GetComponent<Renderer>().sharedMaterial = desel;

        if(carSelected == 0){
            carSelected = carNum - 1;
        } else{
            carSelected--;
        }

        GameObject selected = carParent.Find("Sphere (" + (results[s.value][carSelected] + 83) + ")").gameObject;

        selected.GetComponent<Renderer>().sharedMaterial = sel;

        double xPerc = (27.51-selected.transform.localPosition.x)/(27.51 + 27.51);
        double yPerc = (32.2-selected.transform.localPosition.z)/(32.2 + 32.14);

        double longLeft = -75.2752198;
        double longRight =  -74.9795010;
        
        double latBottom = 40.16080762;
        double latTop =  40.42902459;

        double longitude = longLeft + (xPerc * (longRight-longLeft));
        double latitude = latBottom + (yPerc * (latTop-latBottom));
        lat.text = longitude.ToString();
        longi.text = latitude.ToString();
        textt = "https://www.google.com/maps/place/" + latitude + ","+longitude;
        maps.text = textt;
    }
}
