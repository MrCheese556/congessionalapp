using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class fivecarPoliceAgent : Agent
{
    public int amtCars;

    public Transform carParent;
    public Transform callParent;

    public float car1;
    public float car2;
    public float car3;
    public float car4;
    public float car5;

    public GameObject navAgent1;
    public GameObject navAgent2;
    public GameObject navAgent3;
    public GameObject navAgent4;
    public GameObject navAgent5;
    public PlayerNavMesh navScript1;
    public PlayerNavMesh navScript2;
    public PlayerNavMesh navScript3;
    public PlayerNavMesh navScript4;
    public PlayerNavMesh navScript5;
 
    public int[] result = new int[5];
    public int callNum;

    public float timer;
    public bool timeOn;

    public Transform carPos1;
    public Transform carPos2;
    public Transform carPos3;
    public Transform carPos4;
    public Transform carPos5;

    // Start is called before the first frame update
    void Start()
    {
        timeOn = false;
        RequestDecision();
    }

    // Update is called once per frame
    public override void OnEpisodeBegin() {

        int[] numbers = new int[85];
        for (int i = 0; i <= numbers.Length-1; i++)
        {       
         numbers[i] = i;
        }   

//Shuffle Array
        for (int i = 0; i < 5; i++ )
        {
            int tmp = numbers[i];
            int r = UnityEngine.Random.Range(i, numbers.Length);
            numbers[i] = numbers[r];
            numbers[r] = tmp;
        }
        
         Array.Copy(numbers, 0, result, 0, 5); 
    }
    void Update(){
        if(timeOn){
            timer += Time.deltaTime;
        }
        if(timer >= 21f){
            endScene();
        }
    }
    public void nextCall() {
    
        switch (callNum)
        {
            case 0:
                callParent.Find("Sphere (" + result[callNum] + ")").gameObject.SetActive(true);
                navScript1.movePositionTransform = callParent.Find("Sphere (" + result[callNum] + ")");
        navScript2.movePositionTransform = callParent.Find("Sphere (" + result[callNum] + ")");
        navScript3.movePositionTransform = callParent.Find("Sphere (" + result[callNum] + ")");
        navScript4.movePositionTransform = callParent.Find("Sphere (" + result[callNum] + ")");
        navScript5.movePositionTransform = callParent.Find("Sphere (" + result[callNum] + ")");
                
                break;
            case 1:
                callParent.Find("Sphere (" + result[callNum] + ")").gameObject.SetActive(true);
                callParent.Find("Sphere (" + result[callNum-1] + ")").gameObject.SetActive(false);
                navScript1.movePositionTransform = callParent.Find("Sphere (" + result[callNum] + ")");
        navScript2.movePositionTransform = callParent.Find("Sphere (" + result[callNum] + ")");
        navScript3.movePositionTransform = callParent.Find("Sphere (" + result[callNum] + ")");
        navScript4.movePositionTransform = callParent.Find("Sphere (" + result[callNum] + ")");
        navScript5.movePositionTransform = callParent.Find("Sphere (" + result[callNum] + ")");


                    break;
            case 2:
                callParent.Find("Sphere (" + result[callNum] + ")").gameObject.SetActive(true);
                callParent.Find("Sphere (" + result[callNum-1] + ")").gameObject.SetActive(false);
                navScript1.movePositionTransform = callParent.Find("Sphere (" + result[callNum] + ")");
        navScript2.movePositionTransform = callParent.Find("Sphere (" + result[callNum] + ")");
        navScript3.movePositionTransform = callParent.Find("Sphere (" + result[callNum] + ")");
        navScript4.movePositionTransform = callParent.Find("Sphere (" + result[callNum] + ")");
navScript5.movePositionTransform = callParent.Find("Sphere (" + result[callNum] + ")");

                    break;
            case 3:
                callParent.Find("Sphere (" + result[callNum] + ")").gameObject.SetActive(true);
                callParent.Find("Sphere (" + result[callNum-1] + ")").gameObject.SetActive(false);
                navScript1.movePositionTransform = callParent.Find("Sphere (" + result[callNum] + ")");
        navScript2.movePositionTransform = callParent.Find("Sphere (" + result[callNum] + ")");
        navScript3.movePositionTransform = callParent.Find("Sphere (" + result[callNum] + ")");
        navScript4.movePositionTransform = callParent.Find("Sphere (" + result[callNum] + ")");
        navScript5.movePositionTransform = callParent.Find("Sphere (" + result[callNum] + ")");

                    break;
            case 4:
                callParent.Find("Sphere (" + result[callNum] + ")").gameObject.SetActive(true);
                callParent.Find("Sphere (" + result[callNum-1] + ")").gameObject.SetActive(false);
                navScript1.movePositionTransform = callParent.Find("Sphere (" + result[callNum] + ")");
        navScript2.movePositionTransform = callParent.Find("Sphere (" + result[callNum] + ")");
        navScript3.movePositionTransform = callParent.Find("Sphere (" + result[callNum] + ")");
        navScript4.movePositionTransform = callParent.Find("Sphere (" + result[callNum] + ")");
        navScript5.movePositionTransform = callParent.Find("Sphere (" + result[callNum] + ")");

                    break;
            case 5:
            callParent.Find("Sphere (" + result[callNum-1] + ")").gameObject.SetActive(false);
            
                AddReward((-1 * timer) + 20);
                // timeOn = false;
                timer=0f;
                endScene();
                break;
        }
        navAgent1.SetActive(false);
        navAgent2.SetActive(false);
        navAgent3.SetActive(false);
        navAgent4.SetActive(false);
        navAgent5.SetActive(false);
        
        navAgent1.transform.position = carPos1.position;
        navAgent2.transform.position = carPos2.position;
        navAgent3.transform.position = carPos3.position;
        navAgent4.transform.position = carPos4.position;
        navAgent5.transform.position = carPos5.position;
        
        navAgent1.SetActive(true);
        navAgent2.SetActive(true);
        navAgent3.SetActive(true);
        navAgent4.SetActive(true);
        navAgent5.SetActive(true);
        callNum++; 
    }
    public override void OnActionReceived(ActionBuffers actions) {
        car1 = actions.DiscreteActions[0];
        car2 = actions.DiscreteActions[1];
        car3 = actions.DiscreteActions[2];
        car4 = actions.DiscreteActions[3];
        car5 = actions.DiscreteActions[4];
        Debug.Log(car1);
        if(car2 == car1) car2--;
        if(car3 == car1) car3++;
        if(car3 == car2) car3--;
        if(car4 == car1 || car4 == car2 || car4 == car3) car4 ++;
        if(car4 == car1 || car4 == car2 || car4 == car3) car4 ++;
        if(car4 == car1 || car4 == car2 || car4 == car3) car4 ++;
        if(car5 == car1 || car5 == car2 || car5 == car3 || car5 == car4) car5 ++;
        if(car5 == car1 || car5 == car2 || car5 == car3 || car5 == car4) car5 ++;
        if(car5 == car1 || car5 == car2 || car5 == car3 || car5 == car4) car5 ++;
        car1 += 83;
        car2 += 83;
        car3 += 83;
        car4 += 83;
        car5 += 83;

        carPos1 = carParent.Find("Sphere (" + car1.ToString() + ")");
        carPos2 = carParent.Find("Sphere (" + car2.ToString() + ")");
        carPos3 = carParent.Find("Sphere (" + car3.ToString() + ")");
        carPos4 = carParent.Find("Sphere (" + car4.ToString() + ")");
        carPos5 = carParent.Find("Sphere (" + car5.ToString() + ")");
         
        carPos1.gameObject.SetActive(true);
        carPos2.gameObject.SetActive(true);
        carPos3.gameObject.SetActive(true);
        carPos4.gameObject.SetActive(true);
        carPos5.gameObject.SetActive(true);
        timeOn = true;
        timer=0f;
         callNum = 0; 
        nextCall(); 
    }
    public override void CollectObservations(VectorSensor sensor) {
        sensor.AddObservation(amtCars); 
    }
    public void endScene()
    {
        carParent.Find("Sphere (" + car1.ToString() + ")").gameObject.SetActive(false);
        carParent.Find("Sphere (" + car2.ToString() + ")").gameObject.SetActive(false);
        carParent.Find("Sphere (" + car3.ToString() + ")").gameObject.SetActive(false);
        carParent.Find("Sphere (" + car4.ToString() + ")").gameObject.SetActive(false);
        carParent.Find("Sphere (" + car5  .ToString() + ")").gameObject.SetActive(false);
        EndEpisode();
        RequestDecision();
    }
}
