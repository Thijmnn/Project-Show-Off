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
    void Update()
    {
        _moveDirection = move.action.ReadValue<Vector2>();
        _camMoveDirection = camMove.action.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(_moveDirection.x * moveSpeed, 0, _moveDirection.y * moveSpeed);
        _cameraTransform.Rotate(new Vector3(0, _camMoveDirection.x,0));
    }

    void Interact()
    {
        print("Boop");
    }
}
