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
    //[SerializeField] Material selectedMaterial;

    [SerializeField] GameObject _selectPlaneUI;
    [SerializeField] GameObject _startGameUI;
    #endregion

    private void Awake()
    {
        _selectPlaneUI.SetActive(true);
        _startGameUI.SetActive(false);
    }

    void Update()
    {
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

                foreach (ARPlane plane in _planeManager.trackables)
                {
                    if (plane != selectedPlane)
                    {
                        plane.gameObject.SetActive(false);
                    }
                    /*else
                        plane.GetComponent<Renderer>().material = selectedMaterial;*/
                }
                _selectPlaneUI.SetActive(false);
                _startGameUI.SetActive(true);
            }
        }
    }

    #region Private
    private static ARPlane selectedPlane = null;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    #endregion
}
