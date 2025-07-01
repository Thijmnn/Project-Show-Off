using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCam : MonoBehaviour
{
    public GameObject cam;
    public GameObject spawner;
    public GameObject UI;
    public void SwitchCamera()
    {
        cam.SetActive(false);
        spawner.SetActive(true);
        UI.SetActive(true);
    }
}
