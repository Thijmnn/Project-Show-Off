using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableMovement : MonoBehaviour
{

    public void freezeTime()
    {
        PlayerMovement[] movements = FindObjectsByType<PlayerMovement>(FindObjectsSortMode.None);

        foreach (PlayerMovement movement in movements) { 
        movement.enabled = false;
        }
    }

    public void UnfreezeTime()
    {
        PlayerMovement[] movements = FindObjectsByType<PlayerMovement>(FindObjectsSortMode.None);

        foreach (PlayerMovement movement in movements)
        {
            movement.enabled = true;
        }
    }
}
