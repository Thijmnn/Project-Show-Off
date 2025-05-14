using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SinglePlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    [SerializeField] float moveSpeed;

    private Vector2 _moveDirection;
    private Vector2 _camMoveDirection;

    [SerializeField] private InputActionReference move;
    [SerializeField] private InputActionReference camMove;

    [SerializeField] Transform _cameraTransform;

    private PlayerInput playerInput;

    [SerializeField] private float rotationSpeed;

    private Quaternion _rotation;

    public GameObject playerModel;
    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }
    void Update()
    {
        _moveDirection = move.action.ReadValue<Vector2>();
        _camMoveDirection = camMove.action.ReadValue<Vector2>();
    }

    private void MovePlayer()
    {
        _moveDirection = playerInput.actions["Movement"].ReadValue<Vector2>();
        _rotation = Quaternion.LookRotation(rb.velocity);

        Vector3 movementDirection = new Vector3(_moveDirection.x, 0, _moveDirection.y).normalized;

        rb.velocity = new Vector3(_moveDirection.x * moveSpeed, 0, _moveDirection.y * moveSpeed);

        if (movementDirection.magnitude > 0.1f)
        {
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, _rotation, rotationSpeed * Time.deltaTime);
        }

        /*_cameraTransform.Rotate(new Vector3(0, _camMoveDirection.x, 0));*/
    }
}
