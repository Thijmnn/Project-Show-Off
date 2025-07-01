using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableMovement : MonoBehaviour
{

    public void freezeTime()
    {
        Time.timeScale = 0f;
    }

    public void UnfreezeTime()
    {
        Time.timeScale = 1f;
    }
}
