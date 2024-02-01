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
            other.GetComponent<TargetMove>().startMoving = false;
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
        Transform[] children = crystal.GetComponentsInChildren<Transform>();

        foreach (Transform child in children)
        {
            if (child.CompareTag("SmokeTarget"))
                child.gameObject.SetActive(false);
            else if (child.CompareTag("ExplosionTarget"))
            {
                child.gameObject.SetActive(true);
            }
            else
            {
                Rigidbody rb = child.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = false;
                    rb.AddExplosionForce(_explosionForce, crystal.transform.position, _explosionRadius);
                    child.GetComponent<FadeParts>()._shouldFade = true;
                }
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
