using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public static CameraControl Instance { get; private set; }
    [SerializeField] Transform cameraTargetTransform;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private Camera mainCamera;

    public bool shouldAnim = false;
    public bool animBack = false;

    private void Awake()
    {
        Instance = this;

    }
    private void Start()
    {
        inputHandler = PlayerInputHandler.Instance;
        mainCamera.fieldOfView = originalFov;
        targetRotation = cameraTargetTransform.rotation;
    }

    void Update()
    {
        if (inputHandler.ZoomIn)
            ZoomCamera(-1);
        if (inputHandler.ZoomOut)
            ZoomCamera(1);

        if (shouldAnim && !playedCoroutine)
        {
            initialPosition = transform.position;
            initialRotation = transform.rotation;
            playedCoroutine = true;
            StartCoroutine(MoveCamera(cameraTargetTransform.position));
        }
    }

    public IEnumerator MoveCamera(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(5f);
        StartCoroutine(MoveCameraBack(initialPosition));

    }

    IEnumerator MoveCameraBack(Vector3 targetPosition)
    {
        float distanceThreshold = 0.01f;
        while (Vector3.Distance(transform.position, targetPosition) > distanceThreshold)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, initialRotation, rotationSpeed * Time.deltaTime);

            yield return null;
        }
        PlayerController.Instance.canMove = true;
    }

    private void ZoomCamera(int zoomDirection)
    {
        float newFOV = Camera.main.fieldOfView + (zoomDirection * zoomSpeed * Time.deltaTime);
        newFOV = Mathf.Clamp(newFOV, minZoom, maxZoom);
        mainCamera.fieldOfView = newFOV;

    }
    private PlayerInputHandler inputHandler;
    private int originalFov = 60;
    private int minZoom = 40;
    private int maxZoom = 80;
    private int zoomSpeed = 30;
    
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    public bool playedCoroutine = false;
    private Quaternion targetRotation;

}
