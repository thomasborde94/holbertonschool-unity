using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Show in Inspector
    [SerializeField] private ARRaycastManager _raycastManager;
    [SerializeField] private ARPlaneManager _planeManager;
    [SerializeField] private Camera _camera;

    [SerializeField] private GameObject _targetPrefab;
    [SerializeField] private int _targetAmount;

    [Header("UI")]
    [SerializeField] GameObject _selectPlaneUI;
    [SerializeField] GameObject _startGameUI;
    #endregion

    private void Awake()
    {
        _selectPlaneUI.SetActive(true);
        _startGameUI.SetActive(false);
        _planeManager.enabled = true;
    }

    void Update()
    {
        Ray rays = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(rays, out hit, Mathf.Infinity))
        {
            GameObject hitObject = hit.collider.gameObject;
            Debug.Log("gameobject touché : " + hitObject.name);
        }
        Debug.DrawRay(rays.origin, rays.direction * 100f, Color.red );

        /// -------- For Build -------- ///
        if (Input.touchCount > 0 && selectedPlane == null && _planeManager.trackables.count > 0)
        {
            SelectPlane();
        }
        /// -------- For Editor ------- ///
        /*
        if (Input.GetMouseButtonDown(0) && selectedPlane == null && _planeManager.trackables.count > 0)
        {
            SelectPlane();
        }
        */
    }

    private void SelectPlane()
    {
        /// ------- For Build ----- ///
        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            if (_raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
            {
                ARRaycastHit hit = hits[0];
                selectedPlane = _planeManager.GetPlane(hit.trackableId);

                foreach (ARPlane plane in _planeManager.trackables)
                {
                    if (plane != selectedPlane)
                    {
                        plane.gameObject.SetActive(false);
                    }
                }
                _planeManager.enabled = false;
                _selectPlaneUI.SetActive(false);
                _startGameUI.SetActive(true);
            }
        }
        /// ---------- In Unity Editor ---------------- ///
        /*Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            // Check if the hit object is an AR plane
            ARPlane clickedPlane = hit.transform.GetComponent<ARPlane>();
            selectedPlane = clickedPlane;
            if (selectedPlane != null)
            {
                // Save the selected plane to the static variable in GameManager
                
                Debug.Log("selected plane is : " + selectedPlane.name);

                foreach (ARPlane plane in _planeManager.trackables)
                {
                    if (plane != selectedPlane)
                    {
                        plane.gameObject.SetActive(false);
                    }
                }
                _planeManager.enabled = false;
                _selectPlaneUI.SetActive(false);
                _startGameUI.SetActive(true);
            }
        }*/
    }

    public void StartGame()
    {
        for (int i = 1; i < _targetAmount; i++)
        {
            GameObject target = Instantiate(_targetPrefab, selectedPlane.center, selectedPlane.transform.rotation, selectedPlane.transform);
            target.GetComponent<TargetMove>().StartMoving(selectedPlane);
        }
        _startGameUI.SetActive(false);
    }

    #region Private
    private static ARPlane selectedPlane = null;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    #endregion
}
