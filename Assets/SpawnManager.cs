using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnManager : MonoBehaviour
{
    public PlayerInputManager playerManager;

    public GameObject player1;
    public GameObject player2;

    private void Update()
    {
        if(playerManager.playerCount == 0)
        {
            playerManager.playerPrefab = player1;
        }
        else if(playerManager.playerCount == 1)
        {
            playerManager.playerPrefab = player2;
        }
        else
        {
            playerManager.gameObject.SetActive(false);
        }
    }
}
