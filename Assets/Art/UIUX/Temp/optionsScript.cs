using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class optionsScript : MonoBehaviour
{

    public UnityEvent optionsSwitch;
    public InputActionReference menu;
    private PlayerInput playerInput;

    public UnityEvent noteSpawn;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = gameObject.GetComponentInParent<PlayerInput>();

    }

    // Update is called once per frame
    void Update()
    {
        if(playerInput.actions["Menu"].triggered)
        {
            StartCoroutine(SwitchScreen());
        }

        if (Input.GetKey(KeyCode.O)) 
        { 
            noteSpawn.Invoke();
        }
    }

    IEnumerator SwitchScreen()
    {
        yield return new WaitForSeconds(0.2f);
        optionsSwitch.Invoke();
        yield return new WaitForSeconds(0.2f);
    }
}
