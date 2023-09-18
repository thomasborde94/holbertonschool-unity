using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Show in Inspector

    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] GroundCheckerWithOverlap _groundTester;

    #endregion
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        GetInputs();
    }

    private void FixedUpdate()
    {
        _speed = 0f;
        _isGrounded = _groundTester.TestCollision();

        /*if (_isGrounded)
        {

        }
        else
        {
            _gravity = Physics.gravity * _gravityFallMultiplier * Time.fixedDeltaTime;
        }*/


        if (inputDirection.magnitude > 0.1)
        {
            _speed = _movementSpeed;
        }

        // Assigne la direction à la velocité du joueur
        Vector3 horizontalVelocity = inputDirection * _speed;
        verticalVelocity = new Vector3(0, _rb.velocity.y, 0);
        _rb.velocity = horizontalVelocity + verticalVelocity;

        // Si le player Jump
        if (_isJumping)
        {
            if (_isGrounded)
            {
                _rb.velocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);
                Vector3 jumpForce = Vector3.up * _jumpForce;
                _rb.AddForce(jumpForce, ForceMode.Impulse);

                _isJumping = false;
            }
        }
    }

    #region Private Methods

    private void GetInputs()
    {
        // Horizontal movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        inputDirection = new Vector3(horizontal, 0, vertical);
        inputDirection.Normalize();

        // Jump
        if (Input.GetButtonDown("Jump"))
        {
            if (_isGrounded)
            {
                _isJumping = true;
            }
        }
    }

    #endregion

    #region Private

    private Rigidbody _rb;
    private Vector3 inputDirection;
    private float _speed;
    private Vector3 playerDirection;
    private Vector3 verticalVelocity;
    private bool _isJumping;

    private bool _isGrounded;

    #endregion
}
