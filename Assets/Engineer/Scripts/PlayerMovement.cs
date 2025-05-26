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
    

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        playerInput = GetComponent<PlayerInput>();
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        _moveDirection = playerInput.actions["Movement"].ReadValue<Vector2>();
        _rotation = Quaternion.LookRotation(rb.velocity);

        Vector3 movementDirection = new Vector3(_moveDirection.x, 0, _moveDirection.y).normalized;

        rb.velocity = new Vector3(_moveDirection.x * moveSpeed, 0, _moveDirection.y * moveSpeed);

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
}
