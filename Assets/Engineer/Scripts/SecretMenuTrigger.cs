using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class SecretMenuTrigger : MonoBehaviour
{
    public PlayerInput[] playerInputs;

    public UnityEvent secretMenu;
    // Update is called once per frame
    void Update()
    {
        playerInputs = FindObjectsOfType<PlayerInput>();

        foreach (PlayerInput playerInput in playerInputs)
        {
            if (playerInput.actions["SecretButton1"].IsInProgress() && playerInput.actions["SecretButton2"].IsInProgress() && playerInput.actions["SecretButton3"].IsInProgress() && playerInput.actions["SecretButton4"].IsInProgress())
            {
                secretMenu.Invoke();
            }
        }
    }
}
