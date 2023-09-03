using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oldCalc : MonoBehaviour
{
    public Transform callParent;
    public Transform carParent;
    public clusterer clusterCalc;
    public Material[] mats;
    public int numClusters;
    public ideaNavMesh id;
    public pinHandler ph;
    public int[][] resultsDay;
    // Start is called before the first frame update
    public void begin(){
       if(ph.specificOn == false) {
            resultsDay = new int[7][];
            double [][][] allData = new double[7][][];
            double [][] rawCars = new double[carParent.childCount][];

            for(int j =0; j < carParent.childCount; j++) {
                    Transform t = carParent.Find("Sphere (" + (j+83) + ")");
                    rawCars[j] = new double[] {t.position.x, t.position.z};
            }

            for(int i =0; i < 7; i++){
                allData[i] = rawCars;
            }
            for(int i = 0; i < callParent.childCount; i++){

                GameObject g = callParent.transform.Find("Sphere (" + i + ")").gameObject;
                int day = g.GetComponent<mover>().day;

                Array.Resize(ref allData[day], allData[day].Length +1);
                allData[day][allData[day].Length -1] = new double[] {g.transform.position.x, g.transform.position.z};
                Debug.Log("call" + i + ", day " + day + ", " + g.transform.position.x + g.transform.position.z);
            }
            for(int i =0; i < 7; i++){
                redo(allData[i], i);
                Debug.Log(resultsDay[i][0]);
            }

            
       }
       
    }
    public void redo(double[][] rawData, int day){
        int mini =0;
        float minfloat = 1000000f;
        int go = 8+numClusters;
        if(numClusters>10){
             go = 70;
        }
        for(int i = 0; i < go; i++){
            float f = run(i, false, rawData, day);
            if(f < minfloat) {
                minfloat = f;
                mini = i;
            }
        }
        run(mini, true, rawData, day);
    }
    public float run(int seed, bool yes, double[][] rawData, int day)
    {
        int callAmount = rawData.Length - carParent.childCount;
        // double[][] rawData = new double[callParent.childCount + carParent.childCount][];
         int[][] sortedData = new int[numClusters][];
        int[][] sortedCars = new int[numClusters][];

        for(int i =0; i < numClusters; i++){
             sortedData[i] = new int[0];
             sortedCars[i] = new int[0];
         }

        // for(int i =0; i < callParent.childCount; i++) {
        //     Transform t = callParent.Find("Sphere (" + i + ")");
        //     rawData[i] = new double[] {t.position.x, t.position.z};
        // }
        // for(int i =0; i < carParent.childCount; i++) {
        //     Transform t = carParent.Find("Sphere (" + (i+83) + ")");
        //     rawData[i + callParent.childCount] = new double[] {t.position.x, t.position.z};
        // }
        int[] clustering = clusterCalc.Cluster(rawData, numClusters, seed);

         for(int i =0; i < carParent.childCount; i++) {
              
            int val = clustering[i];
             
            Array.Resize( ref sortedCars[val], sortedCars[val].Length + 1);
            sortedCars[val][sortedCars[val].Length - 1] = i;

            //Renderer rend = carParent.Find("Sphere (" + (sortedCars[val][sortedCars[val].Length - 1]+83) + ")").GetComponent<Renderer>();
             //rend.sharedMaterial = mats[val];
         }
         for(int i =0; i < callAmount; i++) {
            //Renderer rend = callParent.Find("Sphere (" + i + ")").GetComponent<Renderer>();
            int val = clustering[i+carParent.childCount];
            //rend.sharedMaterial = mats[val];
            Array.Resize(ref sortedData[val], sortedData[val].Length + 1);
            sortedData[val][sortedData[val].Length - 1] = i;
         }
         
        
        int[] carPos = new int[numClusters];
         for(int a = 0; a < numClusters; a++) {
            float minSum = 1000000f;
            int minI = 1;
            
            for(int i =0; i < sortedCars[a].Length; i++) {
                float sum = 0f;
                for(int j = 0; j < sortedData[a].Length; j++) {
                    Vector3 v = new Vector3((float)rawData[sortedData[a][j]][0], 0.64f, (float)rawData[sortedData[a][j]][1]);
                    sum += id.GetPathLength(carParent.Find("Sphere (" + (sortedCars[a][i]+83) + ")").position, v);
                }
                
                if(sum < minSum){
                    minI = sortedCars[a][i];
                    minSum = sum;
                }
            }
            // Debug.Log(minSum/sortedCars[a].Length);
            // Debug.Log(a);
         carParent.Find("Sphere (" + (minI+83) + ")").gameObject.SetActive(true);
         carPos[a] = minI;
         }
        float summ = 0f;
        for(int i = 0; i < callAmount; i++) {
             Vector3 v = new Vector3((float)rawData[i][0], 0.64f, (float)rawData[i][1]);
            float length = id.GetPathLength(carParent.Find("Sphere (" + (carPos[clustering[i]]+83) + ")").position, v);

            summ += length;
        }
        if(yes){
            resultsDay[day] = carPos;
        }
        // if(!yes){
        //     for(int a = 0; a < numClusters; a++) {
        //     carParent.Find("Sphere (" + (carPos[a]+83) + ")").gameObject.SetActive(false);
        //     }
            
        // }
        
        return summ;
     }
}
