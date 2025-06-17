using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BlowingScript : MonoBehaviour
{

    public InputActionReference fire;

    private PlayerInput playerInput;

    bool fireEnabled = false;

    PlayerMovement _playerMov;

    bool slowed;

    public float blowMulti;

    public GameObject soundVortex;

    bool startedUp;

    float startUpDelay = 0.2f;

    public bool canSprint;

    Animator animator;

    private List<GameObject> bubblesInTrigger = new List<GameObject>();
    private void Awake()
    { 
        _playerMov = GetComponentInParent<PlayerMovement>();
    }
    private void Start()
    {
        playerInput = GetComponentInParent<PlayerInput>();
        Invoke(nameof(EnableInputCheck), startUpDelay);
        animator = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BubbleBehaviour>())
        {
            bubblesInTrigger.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        RemoveBubble(other.gameObject);
    }

    private void Update()
    {
        BlowBubbles();


        Blowing();

    }


    private void Blowing()
    {
        if (fireEnabled && bubblesInTrigger.Count > 0)
        {
            GameObject closestBubble = null;
            float shortestDistance = float.MaxValue;

            foreach (GameObject bubble in bubblesInTrigger)
            {
                float distance = Vector3.Distance(transform.position, bubble.transform.position);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    closestBubble = bubble;
                }
            }

            if (closestBubble != null)
            {
                Rigidbody rb = closestBubble.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    print("PUSH");
                    rb.AddForce(transform.forward * blowMulti, ForceMode.Force);
                }
            }
        }
    }

    void EnableInputCheck() => startedUp = true;

    void BlowBubbles()
    {
        if (startedUp) {
            if (playerInput.actions["Fire"].inProgress)
            {
                animator.SetBool("IsBlowing",true);
                fireEnabled = true;
                soundVortex.SetActive(true);
                if (!slowed) { _playerMov.originalSpeed *= 0.5f; slowed = true; }
                canSprint = false;
            }
            else
            {
                animator.SetBool("IsBlowing", false);
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

    public void RemoveBubble(GameObject other)
    {
        if (bubblesInTrigger.Contains(other))
        {
            bubblesInTrigger.Remove(other);
        }
    }
}
