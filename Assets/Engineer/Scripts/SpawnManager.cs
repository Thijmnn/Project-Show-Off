using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnManager : MonoBehaviour
{
    public PlayerInputManager playerManager;

    public GameObject player1;
    public GameObject player2;

    public GameObject spawnLoc1;
    public GameObject spawnLoc2;


    PlayerInput player1Input; 
    private void Update()
    {
        if(playerManager.playerCount == 0)
        {
            player1Input = player1.GetComponent<PlayerInput>();
            playerManager.playerPrefab = player1;
            player1.transform.position = spawnLoc1.transform.position;
        }
        else if(playerManager.playerCount == 1)
        {
            playerManager.playerPrefab = player2;
            player2.transform.position = spawnLoc2.transform.position;
        }
        else
        {
            /*playerManager.gameObject.SetActive(false);*/
        }

        
    }
    void OnPlayerJoined(PlayerInput newPlayer)
    {
        foreach (var player in FindObjectsOfType<PlayerInput>())
        {
            if (player != newPlayer)
            {
                foreach (var action in player.actions)
                {
                    Debug.Log($"Fire state before: {player.actions["Fire"].phase}");
                    action.Disable();
                    action.Enable();
                    Debug.Log($"Fire state after: {player.actions["Fire"].phase}");

                    Debug.Log("Player 1 devices:");
                    foreach (var device in player1Input.devices)
                    {
                        Debug.Log(device.displayName);
                    }
                }
            }
        }
    }


}
