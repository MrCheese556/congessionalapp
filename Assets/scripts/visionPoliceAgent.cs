using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class visionPoliceAgent : Agent
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
    public visionNavMesh navScript1;
    public visionNavMesh navScript2;
    public visionNavMesh navScript3;
 
    public int[] result;



    public Transform carPos1;
    public Transform carPos2;
    public Transform carPos3;
    public int a;
    public int b;

    // Start is called before the first frame update
    void Start()
    {
        RequestDecision();
    }

    // Update is called once per frame
    public override void OnEpisodeBegin() {

        a = UnityEngine.Random.Range(10, 138);
        result = new int[a];
        int[] numbers = new int[138];
        for (int i = 0; i <= numbers.Length-1; i++)
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
         
         b = UnityEngine.Random.Range(5, Mathf.Min(20, a));
    }
    void Update(){
        // if(timeOn){
        //     timer += Time.deltaTime;
        // }
    }
    public void nextCall() {
        //     if(callNum == 0) {
        //         callParent.Find("Cube (" + result[callNum] + ")").GetChild(0).gameObject.SetActive(true);
        //         navScript1.movePositionTransform = callParent.Find("Sphere (" + result[callNum] + ")");
        //         navScript2.movePositionTransform = callParent.Find("Sphere (" + result[callNum] + ")");
        //         navScript3.movePositionTransform = callParent.Find("Sphere (" + result[callNum] + ")");
        //     } else if(callNum == b){
        //         callParent.Find("Cube (" + result[callNum - 1] + ")").GetChild(0).gameObject.SetActive(false);
        //             AddReward((-1 * timer) + 20);
        //             // timeOn = false;
        //             timer=0f;
        //             endScene();
        //     } else {
        //         callParent.Find("Cube (" + result[callNum] + ")").GetChild(0).gameObject.SetActive(true);
        //         callParent.Find("Cube (" + result[callNum - 1] + ")").GetChild(0).gameObject.SetActive(false);
        //         navScript1.movePositionTransform = callParent.Find("Sphere (" + result[callNum] + ")");
        //         navScript2.movePositionTransform = callParent.Find("Sphere (" + result[callNum] + ")");
        //         navScript3.movePositionTransform = callParent.Find("Sphere (" + result[callNum] + ")");
        //     }

            
        //     navAgent1.SetActive(false);
        //     navAgent2.SetActive(false);
        //     navAgent3.SetActive(false);
            
        //     navAgent1.transform.position = carPos1.position;
        //     navAgent2.transform.position = carPos2.position;
        //     navAgent3.transform.position = carPos3.position;
            
        //     navAgent1.SetActive(true);
        //     navAgent2.SetActive(true);
        //     navAgent3.SetActive(true);
        //     callNum++; 
     }
    public override void OnActionReceived(ActionBuffers actions) {
        car1 = actions.DiscreteActions[0];
        car2 = actions.DiscreteActions[1];
        car3 = actions.DiscreteActions[2];

        if(car2 == car1) car2--;
        if(car3 == car1) car3++;
        if(car3 == car2) car3--;
        car1 += 83;
        car2 += 83;
        car3 += 83;

        carPos1 = carParent.Find("Sphere (" + car1.ToString() + ")");
        carPos2 = carParent.Find("Sphere (" + car2.ToString() + ")");
        carPos3 = carParent.Find("Sphere (" + car3.ToString() + ")");
         
        carPos1.gameObject.SetActive(true);
        carPos2.gameObject.SetActive(true);
        carPos3.gameObject.SetActive(true);
         

        navAgent1.transform.position = carPos1.position;
        navAgent2.transform.position = carPos2.position;
        navAgent3.transform.position = carPos3.position;
        float reward = 0f;
        for(int i =0; i <b; i++) {
            GameObject g = callParent.Find("Cube (" + result[i] + ")").GetChild(0).gameObject;
            g.SetActive(true);
            float min = Mathf.Min(navScript1.GetPathLength(g.transform.position), navScript2.GetPathLength(g.transform.position));
            min = Mathf.Min(min, navScript3.GetPathLength(g.transform.position));
            reward += min;
            g.SetActive(false);
        }
        reward = reward/b;
        AddReward((-1 * reward) + 20);
        Debug.Log(reward);
        endScene();
    }

    public void endScene()
    {
        carParent.Find("Sphere (" + car1.ToString() + ")").gameObject.SetActive(false);
        carParent.Find("Sphere (" + car2.ToString() + ")").gameObject.SetActive(false);
        carParent.Find("Sphere (" + car3.ToString() + ")").gameObject.SetActive(false);
        for (int i = 0; i < a; i++ )
        {
            callParent.Find("Cube (" + result[i] + ")").gameObject.SetActive(false);
        }
        EndEpisode();
        RequestDecision();
    }
}
