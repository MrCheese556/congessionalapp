using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class roadController : MonoBehaviour
{
    public int roadLevel = 0;
    public GameObject[] roads = new GameObject[3];
    public string[] Scenes;
    public TMP_Dropdown drop;
    public GameObject loading;

    public void changeLevel() {
        roadLevel = drop.value;
        if(roadLevel == 0){
            roads[1].SetActive(false);
            roads[2].SetActive(false);
        } else if(roadLevel == 1){
            roads[1].SetActive(true);
            roads[2].SetActive(false);
        } else {
            roads[1].SetActive(true);
            roads[2].SetActive(true);
        }
    }
    public void go(){
        Invoke("load", 2f);
        loading.SetActive(true);

    }
    public void load(){
        if(roadLevel == 0){
            SceneManager.LoadScene(Scenes[0]);
        }
        if(roadLevel == 1){
            SceneManager.LoadScene(Scenes[1]);
        }
        if(roadLevel == 2){
            SceneManager.LoadScene(Scenes[2]);
        }
    }
}
