using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    public float moveSpeed;

    private Vector2 _moveDirection;

    [SerializeField] private InputActionReference move;

    private PlayerInput playerInput;

    [SerializeField] private float rotationSpeed;

    private Quaternion _rotation;

    Animator anim;

    [HideInInspector] public float originalSpeed;

    private Vector3 velocity;

    public float sprintMulti;
    [HideInInspector] public float newSpeed;

    public float smoothTime = 0.5f;

    private BlowingScript _blow;

    FlowerAnimation _flowerAnimation;


    public bool animationPlaying;
    private void Start()
    {
        originalSpeed = moveSpeed;
        newSpeed = moveSpeed * sprintMulti;
        anim = GetComponentInChildren<Animator>();
        playerInput = GetComponent<PlayerInput>();
        _blow = FindObjectOfType<BlowingScript>();
    }
    private void FixedUpdate()
    {
        if (!animationPlaying) {
            MovePlayer();
            IncreaseSpeed();
        }
        
    }

    private void MovePlayer()
    {
        _moveDirection = playerInput.actions["Movement"].ReadValue<Vector2>();
        _rotation = Quaternion.LookRotation(rb.velocity);

        Vector3 movementDirection = new Vector3(_moveDirection.x, 0, _moveDirection.y).normalized;

        rb.velocity = Vector3.SmoothDamp(rb.velocity, new Vector3(_moveDirection.x * moveSpeed, 0, _moveDirection.y * moveSpeed) , ref velocity, smoothTime);

        if (movementDirection.magnitude > 0.1f)
        {
            anim.SetBool("IsWalking", true);
            transform.rotation = Quaternion.Slerp(transform.rotation, _rotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }
    }

    private void IncreaseSpeed()
    {
        if (playerInput.actions["Sprint"].inProgress && _blow.canSprint)
        {
            moveSpeed = newSpeed;
        }
        else
        {
            moveSpeed = originalSpeed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<FlowerAnimation>())
        {
            _flowerAnimation = other.GetComponent<FlowerAnimation>();

            _flowerAnimation.UpdateFlower();
        }
    }
}
