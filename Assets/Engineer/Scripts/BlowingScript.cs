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

    PlayerMovement _playerMov;

    bool slowed;

    public float blowMulti;
    private void Awake()
    {
        _playerMov = GetComponentInParent<PlayerMovement>();
    }
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
                rb.AddForce(transform.forward * blowMulti, ForceMode.Force);
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
            if(!slowed) { _playerMov.moveSpeed *= 0.5f; slowed = true; }
            
        }
        else
        {
            fireEnabled = false;
            if (slowed) { _playerMov.moveSpeed *= 2f; slowed = false; }
            
        }
    }
}
