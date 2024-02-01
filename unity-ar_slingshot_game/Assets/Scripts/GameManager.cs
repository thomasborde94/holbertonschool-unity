using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    #region Show in Inspector
    [SerializeField] private ARRaycastManager _raycastManager;
    [SerializeField] private ARPlaneManager _planeManager;
    [SerializeField] private Camera _camera;

    [SerializeField] private GameObject _targetPrefab;
    [SerializeField] private int _targetAmount;
    [SerializeField] public int _ammoCount;

    [Header("UI")]
    [SerializeField] GameObject _selectPlaneUI;
    [SerializeField] GameObject _startGameUI;
    [SerializeField] GameObject _pointsUI;
    [SerializeField] Text _pointsText;
    [SerializeField] GameObject _ammoCountUI;
    [SerializeField] Text _ammoCountText;
    [SerializeField] public GameObject _playAgainUI;

    [HideInInspector] public static ARPlane selectedPlane = null;
    [HideInInspector] public float planeYPos;
    [HideInInspector] public int _points;
    #endregion

    private void Awake()
    {
        instance = this;
        _selectPlaneUI.SetActive(true);
        _startGameUI.SetActive(false);
        _planeManager.enabled = true;
        _points = 0;
    }

    void Update()
    {
        /// ---------- Display Ray -------- ///
        /*Ray rays = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(rays, out hit, Mathf.Infinity))
        {
            GameObject hitObject = hit.collider.gameObject;
            Debug.Log("gameobject touché : " + hitObject.name);
        }
        Debug.DrawRay(rays.origin, rays.direction * 100f, Color.red );
        */

        /// -------- For Build -------- ///
        /*
        if (Input.touchCount > 0 && selectedPlane == null && _planeManager.trackables.count > 0)
        {
            SelectPlane();
        }
        */

        /// -------- For Editor ------- ///

        if (Input.GetMouseButtonDown(0) && selectedPlane == null && _planeManager.trackables.count > 0)
        {
            SelectPlane();
        }


        // Destroys lost ammo and spawn a new one
        if (SpawnAmmo.instance.spawnedPrefab != null && SpawnAmmo.instance.spawnedPrefab.transform.position.y <= planeYPos)
        {
            NewAmmo();
        }

        _pointsText.text = _points.ToString();
        _ammoCountText.text = "Ammos left : " + _ammoCount.ToString();
    }

    private void SelectPlane()
    {
        /// ------- For Build ----- ///
        /*
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
        */


        /// ---------- In Unity Editor ---------------- ///

        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            // Check if the hit object is an AR plane
            ARPlane clickedPlane = hit.transform.GetComponent<ARPlane>();
            // Save the selected plane to the static variable in GameManager
            selectedPlane = clickedPlane;
            if (selectedPlane != null)
            {
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

    }

    public void StartGame()
    {
        _ammoCount = 7;
        _ammoCountUI.SetActive(true);
        _pointsUI.SetActive(true);

        float minRotationY = 0f;
        float maxRotationY = 360f;
        
        for (int i = 1; i <= _targetAmount; i++)
        {
            float randomRotationY = Random.Range(minRotationY, maxRotationY);
            GameObject target = Instantiate(_targetPrefab, selectedPlane.center, Quaternion.Euler(0f, randomRotationY, 0f), selectedPlane.transform);
            target.GetComponent<TargetMove>().StartMoving(selectedPlane);
        }
        _startGameUI.SetActive(false);
        planeYPos = selectedPlane.transform.position.y - 2f;
        SpawnAmmo.instance.Spawn();
    }

    public void RestartGame()
    {
        _points = 0;
        _ammoCount = 7;
        _ammoCountUI.SetActive(false);
        _pointsUI.SetActive(false);
        foreach (ARPlane plane in _planeManager.trackables)
        {
            plane.gameObject.SetActive(true);
        }
        GameObject[] targetsToDestroy = GameObject.FindGameObjectsWithTag("Target");
        foreach(GameObject target in targetsToDestroy)
            Destroy(target);
        Destroy(SpawnAmmo.instance.spawnedPrefab.gameObject);
        SpawnAmmo.instance._ammoNotTouched = true;
        _planeManager.enabled = true;
        _selectPlaneUI.SetActive(true);
        _startGameUI.SetActive(false);
        _playAgainUI.SetActive(false);
        selectedPlane = null;
    }

    public void PlayAgain()
    {
        _points = 0;
        _ammoCount = 7;
        GameObject[] targetsToDestroy = GameObject.FindGameObjectsWithTag("Target");
        foreach (GameObject target in targetsToDestroy)
            Destroy(target);
        for (int i = 1; i <= _targetAmount; i++)
        {
            GameObject target = Instantiate(_targetPrefab, selectedPlane.center, selectedPlane.transform.rotation, selectedPlane.transform);
            target.GetComponent<TargetMove>().StartMoving(selectedPlane);
        }
        SpawnAmmo.instance.Spawn();
        _playAgainUI.SetActive(false);
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public void NewAmmo()
    {
        Destroy(SpawnAmmo.instance.spawnedPrefab);
        if (_ammoCount > 0)
            SpawnAmmo.instance.Spawn();
        else
            _playAgainUI.SetActive(true);
    }
    #region Private

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    #endregion
}
