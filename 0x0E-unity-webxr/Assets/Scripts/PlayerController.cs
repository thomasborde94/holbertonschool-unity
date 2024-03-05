using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }
    [Header("Movement Speed")]
    [SerializeField] private float speed = 1f;

    [Header("Look Sensitivity")]
    [SerializeField] private float mouseSensitivity = 2.0f;
    [SerializeField] private float upDownRange = 80f;

    [Header("Ball")]
    [SerializeField] private float ballSensitivity = 5f;
    [SerializeField][Range(0, 1)] private float ballSpeedBoost = 0.1f;

    [HideInInspector] public GameObject ball;
    [HideInInspector] public bool canMove = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;
    }
        
    private void Start()
    {
        inputHandler = PlayerInputHandler.Instance;
        ballsArray = GameObject.FindGameObjectsWithTag("Interactable");
        SaveInitialTransforms();
    }

    private void Update()
    {
        if (inputHandler != null)
        {
            PickupBall();
            HandleMovement();
            if (mainCamera.transform.parent == this.transform)
            {
                HandleRotation();
            }
        }
        else
            Debug.Log("input handler null");
        HandleGravity();

        if (AllBallsThrown())
            StartCoroutine(ResetBalls());
    }

    private void FixedUpdate()
    {
        ThrowBall();
        SpeedUpBall();
    }

    private void HandleMovement()
    {
        Vector3 inputDirection = new Vector3(inputHandler.MoveInput.x, 0f, inputHandler.MoveInput.y);
        Vector3 worldDirection = transform.TransformDirection(inputDirection);
        worldDirection.Normalize();

        currentMovement.x = worldDirection.x * speed;
        currentMovement.z = worldDirection.z * speed;
        if (ball != null && ball.GetComponent<BallScript>().isInAlley && ball.activeSelf && canMove == false)
        {
            Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
            if (inputHandler.MoveInput.x < 0)
                ballRigidbody.velocity += -Vector3.right * ballSensitivity * Time.deltaTime;

            else if (inputHandler.MoveInput.x > 0)
                ballRigidbody.velocity += Vector3.right * ballSensitivity * Time.deltaTime;
            CameraControl.Instance.shouldAnim = true;
        }
        //else
        if (canMove)
        {
            characterController.Move(currentMovement * Time.deltaTime);
            CameraControl.Instance.shouldAnim = false;
            CameraControl.Instance.playedCoroutine = false;
        }
            
    }

    private void HandleRotation()
    {
        if (!CameraControl.Instance.shouldAnim)
        {
            float mouseXRotation = inputHandler.LookInput.x * mouseSensitivity;
            transform.Rotate(0, mouseXRotation * 1.2f, 0);
            verticalRotation -= inputHandler.LookInput.y * mouseSensitivity;
            verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
            mainCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        }
    }

    private void PickupBall()
    {
        if (inputHandler.PickingUp)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 0.7f))
            {
                if (hit.collider.CompareTag("Interactable") && isDragging == false)
                {
                    ball = hit.collider.gameObject;
                    ball.GetComponent<Rigidbody>().isKinematic = true;
                    isDragging = true;
                    initialDistance = Vector3.Distance(ball.transform.position, transform.position) - 0.1f;
                }
            }
        }
        if (!inputHandler.PickingUp && isDragging == true)
        {
            ball.GetComponent<Rigidbody>().isKinematic = false;
            isDragging = false;
            throwingBall = true;
            ball.GetComponent<BallScript>().hasBeenThrown = true;
        }

        if (isDragging)
        {
            Vector3 targetPosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, initialDistance));
            ball.transform.position = targetPosition;
            ballVelocity = (ball.transform.position - previousBallPosition) / Time.deltaTime;
            previousBallPosition = ball.transform.position;
        }
    }

    private void ThrowBall()
    {
        if (throwingBall == true)
        {
            canMove = false;
            Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
            velocityQueue.Enqueue(ballVelocity);

            if (velocityQueue.Count > maxQueueSize)
            {
                velocityQueue.Dequeue();
            }

            Vector3 averageVelocity = Vector3.zero;
            foreach (Vector3 velocity in velocityQueue)
            {
                averageVelocity += velocity;
            }
            averageVelocity /= velocityQueue.Count;
            averageVelocity += new Vector3(0, 0, 0.3f);
            throwVelocity = averageVelocity;
            ballRigidbody.velocity = throwVelocity;
            throwingBall = false;
            ball.GetComponent<BallScript>().hasBeenThrown = true;
        }
    }

    private bool AllBallsThrown()
    {
        for (int i = 0; i < ballsArray.Length; i++)
        {
            if (!ballsArray[i].GetComponent<BallScript>().hasBeenThrown)
                return false;
        }
        return true;
    }

    private void SaveInitialTransforms()
    {
        initialBallsPositions = new Vector3[ballsArray.Length];
        initialBallsRotations = new Quaternion[ballsArray.Length];
        for (int i = 0; i < ballsArray.Length; i++)
        {
            initialBallsPositions[i] = ballsArray[i].transform.position;
            initialBallsRotations[i] = ballsArray[i].transform.rotation;
        }
    }

    private IEnumerator ResetBalls()
    {
        yield return new WaitForSeconds(4f);
        for (int i = 0; i < ballsArray.Length; i++)
        {
            ballsArray[i].transform.position = initialBallsPositions[i];
            ballsArray[i].transform.rotation = initialBallsRotations[i];
            ballsArray[i].SetActive(true);
            BallScript ballScript = ballsArray[i].GetComponent<BallScript>();
            if (ballScript != null)
            {
                ballScript.hasBeenThrown = false;

            }
        }
    }

    private void SpeedUpBall()
    {
        if (ball != null)
        {
            Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
            if (ball.GetComponent<BallScript>().isInSpeedBoost)
            {
                ballRigidbody.velocity += new Vector3(0, 0, ballSpeedBoost);
            }
        }
        
    }

    private void HandleGravity()
    {
        if (characterController.isGrounded)
        {
            currentMovement.y = -0.5f;
        }
        else
            currentMovement.y -= gravity * Time.deltaTime;
    }

    /*
    private void OnDrawGizmos()
    {
        if (inputHandler.PickingUp)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red);

        }
    }
    */
    

    private CharacterController characterController;
    private Camera mainCamera;
    private PlayerInputHandler inputHandler;
    private Vector3 currentMovement;
    private float verticalRotation;
    private bool isDragging;
    
    private float initialDistance;
    private float gravity = 9.81f;

    private Vector3 ballVelocity;
    private Vector3 previousBallPosition;
    private bool throwingBall;
    private int maxQueueSize = 5;
    private Queue<Vector3> velocityQueue = new Queue<Vector3>();
    private Vector3 throwVelocity;
    private GameObject[] ballsArray = new GameObject[5];
    private bool resetBalls;
    private Vector3[] initialBallsPositions;
    private Quaternion[] initialBallsRotations;
}
