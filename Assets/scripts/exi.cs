using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class exi : MonoBehaviour
{
    public string namedf;

    public void exit(){
        SceneManager.LoadScene(namedf);
    }
}
