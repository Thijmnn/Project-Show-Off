using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LeaveGameScript : MonoBehaviour
{
    private PlayerInput playerInput;
    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }
    void Update()
    {
        /*if (playerInput.actions["SelfDestruct"].triggered)
        {
            MultipleTargetCamera.Instance.targets.Remove(gameObject.transform);
            Destroy(gameObject, 0.2f);  
        }*/
    }
}
