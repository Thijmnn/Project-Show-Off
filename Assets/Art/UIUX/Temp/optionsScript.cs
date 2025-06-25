using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class optionsScript : MonoBehaviour
{

    public UnityEvent optionsSwitch;
    public InputActionReference menu;


    // Update is called once per frame
    void Update()
    {
        PlayerInput[] playerInputs = FindObjectsOfType<PlayerInput>();

        foreach (PlayerInput playerInput in playerInputs)
        {
            if (playerInput.actions["Menu"].triggered)
            {
                StartCoroutine(SwitchScreen());
            }

        }
        //if(Input.GetKey(KeyCode.O))
        //{
        //    PopUpShowOne.Invoke();
        //}
        //if (Input.GetKey(KeyCode.P))
        //{
        //    PopUpShowTwo.Invoke();
        //}

    }

    IEnumerator SwitchScreen()
    {
        yield return new WaitForSeconds(0.2f);
        optionsSwitch.Invoke();
        yield return new WaitForSeconds(0.2f);
    }
}
