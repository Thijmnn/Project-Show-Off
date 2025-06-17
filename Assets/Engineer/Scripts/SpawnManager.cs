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

    private void Update()
    {
        if(playerManager.playerCount == 0)
        {
            playerManager.playerPrefab = player1;
            player1.transform.position = spawnLoc1.transform.position;
        }
        else if(playerManager.playerCount == 1)
        {
            playerManager.playerPrefab = player2;
            player1.transform.position = spawnLoc2.transform.position;
        }
        else
        {
            playerManager.gameObject.SetActive(false);
        }

        
    }

}
