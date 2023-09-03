using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine.UI;
public class OpenLinks : MonoBehaviour
{
    public TMP_InputField tmpif;
    public void OpenCurLink(){
        OpenURL(tmpif.text);
    }
    public static void OpenURL(string url)
    {
        OpenTab(url);
    }
    [DllImport("__Internal")]
    private static extern void OpenTab(string url);
}
