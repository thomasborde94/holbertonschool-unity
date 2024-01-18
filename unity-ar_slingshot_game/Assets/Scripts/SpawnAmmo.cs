using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAmmo : MonoBehaviour
{
    public static SpawnAmmo instance;
    [SerializeField] private GameObject _ammoPrefab;
    [SerializeField] private Camera _camera;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (_ammoNotTouched && spawnedPrefab != null)
            spawnedPrefab.transform.position = worldCenter;

    }

    public void Spawn()
    {
        Debug.Log("called spawn method");
        // Calcul de la position au milieu de l'écran
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, _camera.nearClipPlane + 1f);
        worldCenter = _camera.ScreenToWorldPoint(screenCenter);
        spawnedPrefab = Instantiate(_ammoPrefab, worldCenter, Quaternion.identity);
    }

    private GameObject spawnedPrefab;
    private Vector3 worldCenter;
    private bool _ammoNotTouched = true;
}
