using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class calculator : MonoBehaviour
{
    public Transform callParent;
    public Transform carParent;
    public clusterer clusterCalc;
    public Material[] mats;
    public int numClusters;
    public ideaNavMesh id;
    public pinHandler ph;
    public int[] carPos; 
    // Start is called before the first frame update
    public void begin(){
       
       
    }
    public int[] redo(){
        if(callParent.childCount == 0){
            int[] c = new int[numClusters];
            for(int i = 0; i < numClusters; i++){
                c[i] = -1;
            }
            return c;
        } else {

        
        int mini =0;
        float minfloat = 1000000f;
        int go = 8+numClusters;
        if(numClusters>10){
             go = 70;
        }
        for(int i = 0; i < go; i++){
            float f = run(i, false);
            if(f < minfloat) {
                minfloat = f;
                mini = i;
            }
        }
        run(mini, true);
        return carPos;

        }
    }
    public float run(int seed, bool yes)
    {

        double[][] rawData = new double[callParent.childCount + carParent.childCount][];
        int[][] sortedData = new int[numClusters][];
        int[][] sortedCars = new int[numClusters][];

        for(int i =0; i < numClusters; i++){
            sortedData[i] = new int[0];
            sortedCars[i] = new int[0];
        }

        for(int i =0; i < callParent.childCount; i++) {
            Transform t = callParent.Find("Sphere (" + i + ")");
            rawData[i] = new double[] {t.position.x, t.position.z};
        }
        for(int i =0; i < carParent.childCount; i++) {
            Transform t = carParent.Find("Sphere (" + (i+83) + ")");
            rawData[i + callParent.childCount] = new double[] {t.position.x, t.position.z};
        }
        int[] clustering = clusterCalc.Cluster(rawData, numClusters, seed);

         for(int i =0; i < callParent.childCount; i++) {
           // Renderer rend = callParent.Find("Sphere (" + i + ")").GetComponent<Renderer>();
            int val = clustering[i];
            //rend.sharedMaterial = mats[val];
            Array.Resize(ref sortedData[val], sortedData[val].Length + 1);
            sortedData[val][sortedData[val].Length - 1] = i;
         }
         for(int i =0; i < carParent.childCount; i++) {
              
            int val = clustering[i+callParent.childCount];
             
            Array.Resize( ref sortedCars[val], sortedCars[val].Length + 1);
            sortedCars[val][sortedCars[val].Length - 1] = i;

            //Renderer rend = carParent.Find("Sphere (" + (sortedCars[val][sortedCars[val].Length - 1]+83) + ")").GetComponent<Renderer>();
             //rend.sharedMaterial = mats[val];
         }
        
         carPos = new int[numClusters];
         for(int a = 0; a < numClusters; a++) {
            float minSum = 1000000f;
            int minI = 1;
            
            for(int i =0; i < sortedCars[a].Length; i++) {
                float sum = 0f;
                for(int j = 0; j < sortedData[a].Length; j++) {
                
                    sum += id.GetPathLength(carParent.Find("Sphere (" + (sortedCars[a][i]+83) + ")").position,callParent.Find("Sphere (" + sortedData[a][j] + ")").position);
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
        for(int i = 0; i < callParent.childCount; i++) {

            float length = id.GetPathLength(carParent.Find("Sphere (" + (carPos[clustering[i]]+83) + ")").position,callParent.Find("Sphere (" + i + ")").position);

            summ += length;
        }
        
            for(int a = 0; a < numClusters; a++) {
            carParent.Find("Sphere (" + (carPos[a]+83) + ")").gameObject.SetActive(false);
            }
            
        
        
        return summ;
     }
}
