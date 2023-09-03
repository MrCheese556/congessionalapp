using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class safevisionPoliceAgent : Agent
{
    public int amtCars;

    public Transform carParent;
    public Transform callParent;

    public float car1;
    public float car2;
    public float car3;

    public GameObject navAgent1;
    public GameObject navAgent2;
    public GameObject navAgent3;
    public PlayerNavMesh navScript1;
    public PlayerNavMesh navScript2;
    public PlayerNavMesh navScript3;
 
    public int[] result = new int[5];
    public int callNum;

    public float timer;
    public bool timeOn;

    public Transform carPos1;
    public Transform carPos2;
    public Transform carPos3;
    public int a;
    public int b;
    public int[] numbers;

    // Start is called before the first frame update
    void Start()
    {
        timeOn = false;
         a = UnityEngine.Random.Range(10, 138);
        result = new int[a];
        numbers = new int[138];
        for (int i = 0; i < numbers.Length; i++)
        {       
         numbers[i] = i;
         
        } 

//Shuffle Array
        for (int i = 0; i < a; i++ )
        {
            int tmp = numbers[i];
            int r = UnityEngine.Random.Range(i, numbers.Length);
            numbers[i] = numbers[r];
            numbers[r] = tmp;
            
        }
        
         Array.Copy(numbers, 0, result, 0, a);
         
         b = UnityEngine.Random.Range(5, Mathf.Min(10, a));
        RequestDecision();
    }

    // Update is called once per frame
    public override void OnEpisodeBegin() {

         
    }
    void Update(){
        if(timeOn){
            timer += Time.deltaTime;
        }
        if(timer - (6 * b) > 7){

            endScene();
        }
    }
    public void nextCall() {
              if(callNum == 0) {
                callParent.Find("Cube (" + result[callNum] + ")").GetChild(0).gameObject.SetActive(true);
                navScript1.movePositionTransform = callParent.Find("Cube (" + result[callNum] + ")").GetChild(0);
                navScript2.movePositionTransform = callParent.Find("Cube (" + result[callNum] + ")").GetChild(0);
                navScript3.movePositionTransform = callParent.Find("Cube (" + result[callNum] + ")").GetChild(0);
            } else if(callNum == b){
                callParent.Find("Cube (" + result[callNum - 1] + ")").GetChild(0).gameObject.SetActive(false);
                    AddReward(-1 * (timer/b) + 20);
                    // timeOn = false;
                    timer=0f;
                    endScene();
            } else {
                callParent.Find("Cube (" + result[callNum] + ")").GetChild(0).gameObject.SetActive(true);
                callParent.Find("Cube (" + result[callNum - 1] + ")").GetChild(0).gameObject.SetActive(false);
                navScript1.movePositionTransform = callParent.Find("Cube (" + result[callNum] + ")").GetChild(0);
                navScript2.movePositionTransform = callParent.Find("Cube (" + result[callNum] + ")").GetChild(0);
                navScript3.movePositionTransform = callParent.Find("Cube (" + result[callNum] + ")").GetChild(0);
            }

            
            navAgent1.SetActive(false);
            navAgent2.SetActive(false);
            navAgent3.SetActive(false);
            
            navAgent1.transform.position = carPos1.position;
            navAgent2.transform.position = carPos2.position;
            navAgent3.transform.position = carPos3.position;
            
            navAgent1.SetActive(true);
            navAgent2.SetActive(true);
            navAgent3.SetActive(true);
            callNum++; 
    }
    public override void OnActionReceived(ActionBuffers actions) {
       // Debug.Log("recieved");
        car1 = actions.DiscreteActions[0];
        car2 = actions.DiscreteActions[1];
        car3 = actions.DiscreteActions[2];
       // Debug.Log(car1);
        if(car2 >= car1) car2++;
        if(car3 >= car1) car3++;
        if(car3 >= car2) car3++;
        car1 += 83;
        car2 += 83;
        car3 += 83;

        carPos1 = carParent.Find("Sphere (" + car1.ToString() + ")");
        carPos2 = carParent.Find("Sphere (" + car2.ToString() + ")");
        carPos3 = carParent.Find("Sphere (" + car3.ToString() + ")");
         
        carPos1.gameObject.SetActive(true);
        carPos2.gameObject.SetActive(true);
        carPos3.gameObject.SetActive(true);
        timeOn = true;
        timer=0f;
         callNum = 0; 
        nextCall(); 
    }
    public override void CollectObservations(VectorSensor sensor) {
        
    }
    public void endScene()
    {
        for (int i = 0; i < numbers.Length; i++)
        {    
            callParent.Find("Cube (" + numbers[i] + ")").gameObject.SetActive(false);
        }
        carParent.Find("Sphere (" + car1.ToString() + ")").gameObject.SetActive(false);
        carParent.Find("Sphere (" + car2.ToString() + ")").gameObject.SetActive(false);
        carParent.Find("Sphere (" + car3.ToString() + ")").gameObject.SetActive(false);
        EndEpisode();
         a = UnityEngine.Random.Range(10, 138);
        result = new int[a];
        numbers = new int[138];
        for (int i = 0; i < numbers.Length; i++)
        {       
         numbers[i] = i;
         
        } 

//Shuffle Array
        for (int i = 0; i < a; i++ )
        {
            int tmp = numbers[i];
            int r = UnityEngine.Random.Range(i, numbers.Length);
            numbers[i] = numbers[r];
            numbers[r] = tmp;
            callParent.Find("Cube (" + numbers[i] + ")").gameObject.SetActive(true);
        }
        
         Array.Copy(numbers, 0, result, 0, a);
         
         b = UnityEngine.Random.Range(5, Mathf.Min(10, a));
        RequestDecision();
    }
}
