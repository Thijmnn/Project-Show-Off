using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BlowingScript : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private InputActionReference fire;

    private PlayerInput playerInput;

    bool fireEnabled = false;

    private void Start()
    {
        playerInput = GetComponentInParent<PlayerInput>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (fireEnabled)
        {
            if (other.GetComponent<BubbleBehaviour>())
            {
                rb = other.GetComponent<Rigidbody>();
                rb.AddForce(transform.forward, ForceMode.Force);
            }
        }  
        
    }

    private void Update()
    {
        BlowBubbles();
    }

    void BlowBubbles()
    {
        if (playerInput.actions["Fire"].inProgress)
        {
            fireEnabled = true;
        }
        else
        {
            fireEnabled = false;
        }
    }
}
