using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class VRInteractions : MonoBehaviour
{
    public static VRInteractions Instance { get; private set; }
    [SerializeField] private InputActionAsset vrPlayerControls;

    [HideInInspector] public GameObject ball;
    [HideInInspector] public bool canMove = true;
    [HideInInspector] public bool throwingBall;

    [SerializeField][Range(0, 1)] private float ballSpeedBoost = 0.1f;
    [SerializeField] private float ballSensitivity = 5f;
    [SerializeField] DynamicMoveProvider locomotionSystem;
    [SerializeField] private string actionMapName = "XRI LeftHand Locomotion";
    [SerializeField] private string move = "Move";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
            Destroy(gameObject);

        mainCamera = Camera.main;
        moveAction = vrPlayerControls.FindActionMap(actionMapName).FindAction(move);
        RegisterInputActions();
    }

    private void Start()
    {
        ballsArray = GameObject.FindGameObjectsWithTag("Interactable");
        SaveInitialTransforms();
    }

    private void Update()
    {
        HandleMovement();

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
        Debug.Log("can move is " + canMove);
        if (canMove)
        {
            locomotionSystem.moveSpeed = 1;
            VRCameraControl.Instance.shouldAnim = false;
            VRCameraControl.Instance.playedCoroutine = false;
        }
        else
        {
            locomotionSystem.moveSpeed = 0;
            if (ball != null && ball.GetComponent<BallScript>().isInAlley && ball.activeSelf && canMove == false)
            {
                Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
                if (MoveInput.x < 0)
                    ballRigidbody.velocity += -Vector3.right * ballSensitivity * Time.deltaTime;

                else if (MoveInput.x > 0)
                    ballRigidbody.velocity += Vector3.right * ballSensitivity * Time.deltaTime;
                VRCameraControl.Instance.shouldAnim = true;
            }
        }
    }
    
    
    private void ThrowBall()
    {
        if (throwingBall == true)
        {
            canMove = false;
            
            if (ball != null)
                ball.GetComponent<BallScript>().hasBeenThrown = true;
            throwingBall = false;
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

    private bool AllBallsThrown()
    {
        for (int i = 0; i < ballsArray.Length; i++)
        {
            if (!ballsArray[i].GetComponent<BallScript>().hasBeenThrown)
                return false;
        }
        return true;
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

    private void RegisterInputActions()
    {
        moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => MoveInput = Vector2.zero;
    }
    private void OnEnable()
    {
        moveAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
    }
    public Vector2 MoveInput { get; private set; }

    private Camera mainCamera;
    private GameObject[] ballsArray = new GameObject[5];
    private Vector3[] initialBallsPositions;
    private Quaternion[] initialBallsRotations;

    private InputAction moveAction;
}
