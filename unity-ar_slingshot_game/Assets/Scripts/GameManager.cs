using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class GameManager : MonoBehaviour
{
    #region Show in Inspector
    [SerializeField] private ARRaycastManager _raycastManager;
    [SerializeField] private ARPlaneManager _planeManager;

    [SerializeField] Material changedMaterial;
    #endregion
    private void Awake()
    {
        //session.Reset();
    }

    private void Start()
    {
        
    }

    void Update()
    {
        foreach (ARPlane plane in _planeManager.trackables)
        {
            plane.GetComponent<Renderer>().material = changedMaterial;
        }
        if (Input.touchCount > 0 && selectedPlane == null && _planeManager.trackables.count > 0)
        {
            SelectPlane();
        }
    }

    private void SelectPlane()
    {
        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            if (_raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
            {
                ARRaycastHit hit = hits[0];
                selectedPlane = _planeManager.GetPlane(hit.trackableId);

                //selectedPlane.GetComponent<Renderer>().material = PlaneOcclusionMaterial;
                // SetMaterialTransparent(selectedPlane);

                foreach (ARPlane plane in _planeManager.trackables)
                {
                    if (plane != selectedPlane)
                    {
                        plane.gameObject.SetActive(false);
                    }
                    else
                        plane.GetComponent<Renderer>().material = changedMaterial;
                }
                _planeManager.enabled = false;
                //selectPlaneCanvas.SetActive(false);
                //OnPlaneSelected?.Invoke(selectedPlane);
            }
        }

    }

    #region Events
    public delegate void PlaneSelectedEventHandler(ARPlane thePlane);
    public event PlaneSelectedEventHandler OnPlaneSelected;
    #endregion

    #region Private
    private ARPlane selectedPlane = null;
    private ARSession session;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    #endregion
}
