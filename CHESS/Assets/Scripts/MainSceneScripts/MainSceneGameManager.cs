using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneGameManager : MonoBehaviour
{
    public GameObject UICanvas;

    MainSceneGameManager() {

    }

    public void closeUI() {
        Debug.Log("closing");
        UICanvas.transform.gameObject.SetActive(false);
    }
}
