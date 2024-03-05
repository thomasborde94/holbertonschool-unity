using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            this.gameObject.SetActive(false);
            Destroy(other.gameObject);
            ObstacleSpawner.Instance.DisableObstacles();
        }
    }
}
