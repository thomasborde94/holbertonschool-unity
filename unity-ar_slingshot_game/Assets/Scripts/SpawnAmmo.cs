using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnAmmo : MonoBehaviour
{
    public static SpawnAmmo instance;
    [SerializeField] private GameObject _ammoPrefab;
    [SerializeField] private Camera _camera;

    [SerializeField] private float _ammoSpeed;
    [SerializeField] private float _nearClipPlaneDistance = 1f;

    [Header("Trajectory Display")]
    [SerializeField][Range(10, 100)] private int linePoints = 25;
    [SerializeField][Range(0.01f, 0.25f)] private float timeBetweenPoints = 0.1f;

    
    [HideInInspector] public GameObject spawnedPrefab;
    [HideInInspector] public bool _ammoNotTouched = true;
    [HideInInspector] public bool isAiming;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        
        UpdateAmmoPosition();
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            GrabAmmo();
            if (isAiming)
                DrawProjection();
        }
    }

    private void FixedUpdate()
    {
        if (fired)
            Firing();
    }

    #region Public Methods
    public void Spawn()
    {
        // Calcul de la position au milieu de l'écran
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, _camera.nearClipPlane + _nearClipPlaneDistance);
        worldCenter = _camera.ScreenToWorldPoint(screenCenter);
        float cameraRotationY = _camera.transform.rotation.eulerAngles.y - 90f;

        spawnedPrefab = Instantiate(_ammoPrefab, worldCenter, Quaternion.Euler(0, cameraRotationY, 0));
        _ammoRb = spawnedPrefab.GetComponent<Rigidbody>();
        _lineRenderer = spawnedPrefab.GetComponent<LineRenderer>();
        _ammoNotTouched = true;
        
    }
    #endregion
    private void DrawProjection()
    {
        _lineRenderer.enabled = true;
        _lineRenderer.positionCount = Mathf.CeilToInt(linePoints / timeBetweenPoints) + 1;
        Vector3 startPosition = spawnedPrefab.transform.position;
        Vector3 startVelocity = GrabAmmo() / _ammoRb.mass;
        int i = 0;
        _lineRenderer.SetPosition(i, startPosition);
        for (float time = 0; time < linePoints; time += timeBetweenPoints)
        {
            i++;
            Vector3 point = startPosition + time * startVelocity;
            point.y = startPosition.y + startVelocity.y * time + (Physics.gravity.y / 2f * time * time);
            _lineRenderer.SetPosition(i, point);
        }
    }
    private Vector3 GrabAmmo()
    {
        /// -------- Unity Editor ---------- ///
#if UNITY_EDITOR
        if (spawnedPrefab != null)
        {
            Vector3 mousePos = Input.mousePosition;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Debug.Log(hit.collider.tag);
                if (hit.collider.CompareTag("Ammo") && (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)))
                {
                    isAiming = true;
                    // Gets the position of the original position of the ammo
                    if (Input.GetMouseButtonDown(0))
                    {
                        _ammoNotTouched = false;
                        Vector3 originalPosWorld = spawnedPrefab.transform.position;
                        originalPosCam = _camera.WorldToScreenPoint(originalPosWorld);
                    }
                    // Moves the ammo with the mouse
                    if (Input.GetMouseButton(0))
                    {
                        Vector3 newPosition = _camera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, _camera.nearClipPlane + _nearClipPlaneDistance));
                        newPositionCam = _camera.WorldToScreenPoint(newPosition);

                        // Clamps the ammo pos under original pos
                        if (newPositionCam.y <= originalPosCam.y)
                            spawnedPrefab.transform.position = newPosition;
                    }
                }
            }
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);
            if (Input.GetMouseButtonUp(0) && !_ammoNotTouched)
            {
                fired = true;
                isAiming = false;
            }
            float ammoRotationY = spawnedPrefab.transform.rotation.eulerAngles.y;
            dragVector = newPositionCam - originalPosCam;

            // Calcul de la quantité de drag vers le bas
            float temp = ((newPositionCam.y * 100f) / originalPosCam.y) / 100;
            dragValue = Mathf.Round((1 - temp) * 1000f) / 1000f;
            dragVector = new Vector3(dragVector.y, 0f, -dragVector.x);

            // Permets de se servir de la rotation Y de l'ammo pour donner l'impulsion à l'ammo
            rotatedDragVector = Quaternion.Euler(0, ammoRotationY, 0) * dragVector;
            totalSpeed = _ammoSpeed * dragValue;
            force = totalSpeed * -rotatedDragVector.normalized;
            return force;
        }

#else
        /// ------------ Build ------------- ///

        if (spawnedPrefab != null)
        {
            Vector3 touchPos = Input.GetTouch(0).position;
            Ray ray = _camera.ScreenPointToRay(touchPos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {

                if (hit.collider.CompareTag("Ammo") && (Input.GetTouch(0).phase == TouchPhase.Began || 
                    Input.GetTouch(0).phase == TouchPhase.Moved))
                {
                    isAiming = true;
                    // Gets the position of the original position of the ammo
                    if (Input.GetTouch(0).phase == TouchPhase.Began)
                    {
                        _ammoNotTouched = false;
                        Vector3 originalPosWorld = spawnedPrefab.transform.position;
                        originalPosCam = _camera.WorldToScreenPoint(originalPosWorld);
                    }

                    // Moves the ammo with the touch
                    Vector3 newPosition = _camera.ScreenToWorldPoint(new Vector3(touchPos.x, touchPos.y, _camera.nearClipPlane + _nearClipPlaneDistance));
                    newPositionCam = _camera.WorldToScreenPoint(newPosition);

                    // Clamps the ammo pos under original pos
                    if (newPositionCam.y <= originalPosCam.y)
                        spawnedPrefab.transform.position = newPosition;
                    
                }
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended && !_ammoNotTouched)
            {
                fired = true;
                isAiming = false;
            }
            float ammoRotationY = spawnedPrefab.transform.rotation.eulerAngles.y;
            dragVector = newPositionCam - originalPosCam;

            // Calcul de la quantité de drag vers le bas
            float temp = ((newPositionCam.y * 100f) / originalPosCam.y) / 100;
            dragValue = Mathf.Round((1 - temp) * 1000f) / 1000f;
            dragVector = new Vector3(dragVector.y, 0f, -dragVector.x);

            // Permets de se servir de la rotation Y de l'ammo pour donner l'impulsion à l'ammo
            rotatedDragVector = Quaternion.Euler(0, ammoRotationY, 0) * dragVector;
            totalSpeed = _ammoSpeed * dragValue;
            force = totalSpeed * -rotatedDragVector.normalized;
            return force;
        }
#endif
        return Vector3.zero;
    }

    private void Firing()
    {
        _ammoRb.isKinematic = false;
        _ammoRb.AddForce(-rotatedDragVector.normalized * totalSpeed, ForceMode.Impulse);
        fired = false;
        _lineRenderer.enabled = false;
        GameManager.instance._ammoCount--;
    }

    private void UpdateAmmoPosition()
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, _camera.nearClipPlane + _nearClipPlaneDistance);
        worldCenter = _camera.ScreenToWorldPoint(screenCenter);
        if (_ammoNotTouched && spawnedPrefab != null)
        {
            spawnedPrefab.transform.position = worldCenter;
        }
    }

    public float DragValue
    {
        get { return dragValue; }
    }

    #region Private
    private Rigidbody _ammoRb;

    private Vector3 newPositionCam;
    private Vector3 originalPosCam;
    private Vector3 dragVector;
    private Vector3 force;
    private float dragValue;
    private float totalSpeed;
    private Vector3 rotatedDragVector;
    private Vector3 worldCenter;
    private bool fired = false;
    
    private LineRenderer _lineRenderer;
    #endregion
}
