using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.SpatialTracking;

public class VRCameraControl : MonoBehaviour
{
    public static VRCameraControl Instance { get; private set; }
    [SerializeField] Transform cameraTargetTransform;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera movingCamera;

    public bool shouldAnim = false;
    public bool animBack = false;

    private void Awake()
    {
        Instance = this;

    }
    private void Start()
    {
        targetRotation = cameraTargetTransform.rotation;
        movingCamera.gameObject.SetActive(false);
    }

    void Update()
    {
        Debug.Log(shouldAnim);
        Debug.Log("can move in Camera controls is " + VRInteractions.Instance.canMove);
        if (!shouldAnim)
        {
            mainCamera.gameObject.SetActive(true);
            movingCamera.gameObject.SetActive(false);
            movingCamera.transform.position = mainCamera.transform.position;
            movingCamera.transform.rotation = mainCamera.transform.rotation;
        }
        else if (shouldAnim && !playedCoroutine)
        {
            Debug.Log("should play coroutine");
            initialPosition = mainCamera.transform.position;
            initialRotation = mainCamera.transform.rotation;
            playedCoroutine = true;
            StartCoroutine(MoveCamera(cameraTargetTransform.position));
        }
    }

    public IEnumerator MoveCamera(Vector3 targetPosition)
    {
        movingCamera.gameObject.SetActive(true);
        mainCamera.gameObject.SetActive(false);
        while (Vector3.Distance(movingCamera.transform.position, targetPosition) > 0.5f)
        {
            movingCamera.transform.position = Vector3.MoveTowards(movingCamera.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            movingCamera.transform.rotation = Quaternion.Lerp(movingCamera.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(5f);
        Debug.Log("will start second coroutine");
        StartCoroutine(MoveCameraBack(initialPosition));

    }

    IEnumerator MoveCameraBack(Vector3 targetPosition)
    {
        Debug.Log("starting second coroutine");
        while (Vector3.Distance(movingCamera.transform.position, targetPosition) > 0.5f)
        {
            movingCamera.transform.position = Vector3.MoveTowards(movingCamera.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            movingCamera.transform.rotation = Quaternion.Lerp(movingCamera.transform.rotation, initialRotation, rotationSpeed * Time.deltaTime);
            Debug.Log("in the second while loop");
            yield return null;
        }
        Debug.Log("about to end both coroutines");
        VRInteractions.Instance.canMove = true;
        Debug.Log("can move in Camera controls in coroutine is " + VRInteractions.Instance.canMove);
    }

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    public bool playedCoroutine = false;
    private Quaternion targetRotation;

}
