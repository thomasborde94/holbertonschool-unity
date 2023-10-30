using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerTrigger : MonoBehaviour
{
    #region Show in Inspector

    [SerializeField] private GameObject player;

    #endregion
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            player.GetComponent<Timer>().enabled = true;
    }
}
