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

    public GameObject soundVortex;

    bool startedUp;
    float startUpDelay = 0.2f;

    public bool canSprint;
    private void Awake()
    {
        _playerMov = GetComponentInParent<PlayerMovement>();
    }
    private void Start()
    {
        playerInput = GetComponentInParent<PlayerInput>();
        Invoke(nameof(EnableInputCheck), startUpDelay);
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
                if (!slowed) { _playerMov.originalSpeed *= 0.5f; slowed = true; }
                canSprint = false;
            }
            else
            {
                soundVortex.SetActive(false);
                fireEnabled = false;
                if (slowed) { _playerMov.originalSpeed *= 2f; slowed = false; }
                canSprint = true;
            }
        }
        else
        {
            soundVortex.SetActive(false);
            fireEnabled = false;
            if (slowed) { _playerMov.moveSpeed *= 2f; slowed = false; }
            
        }
    }
}
