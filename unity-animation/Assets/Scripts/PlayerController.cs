using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Show in Inspector

    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _turnSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] GroundCheckerWithOverlap _groundTester;

    [SerializeField] Camera _camera;

    [SerializeField] Transform respawn;

    [HideInInspector] public bool canMove = true;
    #endregion
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (canMove)
            GetInputs();
        if (_isGrounded)
            _fell = false;
    }

    private void FixedUpdate()
    {
        _speed = 0f;
        _isGrounded = _groundTester.TestCollision();

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

        Vector3 camForward = _camera.transform.forward;

        // stops player from rotating upwards based on camera orientation
        camForward.y = 0;

        // IF I WANT THE PLAYER TO ALWAYS FACE THE SAME WAY AS THE CAMERA DOES
        /*if (inputDirection != Vector3.zero)
        {
            // Quaternion.LookRotation function is used to create a quaternion that represents the rotation needed to align an
            // object's forward direction with a specified direction vector.
            // Here, it creates a rotation that aligns the player's forward direction with the camera's horizontal forward direction(camForward).
            //This ensures that the player character will face the same direction as the camera's forward direction on the horizontal plane.
            Quaternion playerRotation = Quaternion.LookRotation(camForward);

            // Updates rotation of player
            transform.rotation = Quaternion.Slerp(transform.rotation, playerRotation, _turnSpeed *Time.deltaTime);
        }*/

        // IF I WANT THE PLAYER TO ROTATE ACCORDINGLY TO WHERE IS WALKS TO AND NOT ACCORDINGLY TO THE CAMERA
        if (inputDirection != Vector3.zero)
        {
            Quaternion playerRotation = Quaternion.LookRotation(inputDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, playerRotation, _turnSpeed * Time.deltaTime);
        }


    }

    #region Private Methods

    private void GetInputs()
    {
        // Horizontal movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Here, the quaternion operation allows the camera to affect the player direct movement
        inputDirection = Quaternion.Euler(0, _camera.transform.eulerAngles.y, 0) * new Vector3(horizontal, 0, vertical);
        inputDirection.Normalize();

        // Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_isGrounded)
            {
                _isJumping = true;
            }
        }
    }

    private void Fell()
    {
        transform.position = respawn.position;
        _fell = true;
        canMove = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Respawn")
        {
            Fell();
        }
    }

    #endregion


    #region Public Properties

    public bool Move
    {
        get
        {
            return _speed != 0;
        }
    }

    public bool IsJumping
    {
        get
        {
            return _isJumping;
        }
    }

    public bool Falling
    {
        get
        {
            return _fell;
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
    private bool _fell;

    #endregion
}
