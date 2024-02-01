using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCollisions : MonoBehaviour
{
    public static AmmoCollisions instance;

    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;

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
            GameManager.instance._points += 10;
            ExplodeCrystal(other.gameObject);
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

    private void ExplodeCrystal(GameObject crystal)
    {
        // Instantiate and activate explosion parts
        foreach (Transform child in crystal.transform)
        {
            GameObject explosionPart = Instantiate(child.gameObject, child.position, child.rotation);
            explosionPart.SetActive(true);

            // Add force to simulate explosion
            Rigidbody rb = explosionPart.GetComponent<Rigidbody>();
            if (rb == null && explosionPart.CompareTag("SmokeTarget"))
                explosionPart.SetActive(false);
            else if (rb == null && explosionPart.CompareTag("ExplosionTarget"))
            {
                explosionPart.SetActive(true);
            }
            else
            {
                rb.isKinematic = false;
                rb.AddExplosionForce(_explosionForce, crystal.transform.position, _explosionRadius);
                explosionPart.GetComponent<FadeParts>()._shouldFade = true;
            }
        }

        StartCoroutine(DestroyOriginalCrystal(crystal));
    }

    

    private IEnumerator DestroyOriginalCrystal(GameObject crystal)
    {
        
        Debug.Log("called destroy original");
        yield return new WaitForSeconds(2f);
        Destroy(crystal);
        Debug.Log("destroy after 2 sec");
    }
}
