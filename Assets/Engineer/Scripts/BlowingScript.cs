using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public GameObject soundVortex;

    bool startedUp;
    float startUpDelay = 0.2f;
    private void Awake()
    {
        
        _playerMov = GetComponentInParent<PlayerMovement>();
    }
    private void Start()
    {
        Invoke(nameof(EnableInputCheck), startUpDelay);
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

    void EnableInputCheck() => startedUp = true;

    void BlowBubbles()
    {
        if (startedUp) {
            if (playerInput.actions["Fire"].inProgress)
            {
                fireEnabled = true;
                soundVortex.SetActive(true);
                if (!slowed) { _playerMov.originalSpeed *= 0.4f; slowed = true; }

            }
            else
            {
                soundVortex.SetActive(false);
                fireEnabled = false;
                if (slowed) { _playerMov.originalSpeed *= 2.5f; slowed = false; }

            }
        }
        
    }
}
