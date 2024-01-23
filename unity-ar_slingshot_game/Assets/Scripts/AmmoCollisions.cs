using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCollisions : MonoBehaviour
{
    public static AmmoCollisions instance;
    [HideInInspector] public GameObject[] targetsToDestroy;

    private void Awake()
    {
        instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        // If hit the plane
        if (other.name == GameManager.selectedPlane.name)
        {
            GameManager.instance.NewAmmo();
        }

        // If hit the target
        if (other.CompareTag("Target"))
        {
            Debug.Log(targetsToDestroy.Length);
            GameManager.instance._points += 10;
            Destroy(other.gameObject);
            targetsToDestroy = GameObject.FindGameObjectsWithTag("Target");
            
            if (targetsToDestroy.Length == 1)
            {
                Destroy(SpawnAmmo.instance.spawnedPrefab);
                GameManager.instance._playAgainUI.SetActive(true);
            }
            else
                GameManager.instance.NewAmmo();
        }
    }
}
