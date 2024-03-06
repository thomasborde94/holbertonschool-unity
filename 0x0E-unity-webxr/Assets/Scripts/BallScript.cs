using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public bool isInAlley = false;
    [HideInInspector] public bool isInSpeedBoost = false;
    [HideInInspector] public bool hasBeenThrown = false;

    public bool hasBeenPickedUp = false;

    public void PickedUpBall()
    {
        VRInteractions.Instance.ball = this.gameObject;
        hasBeenPickedUp = true;
    }
    public void ThrewBall()
    {
        hasBeenPickedUp = false;
        VRInteractions.Instance.throwingBall = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Alley"))
        {
            isInAlley = true;
            hasBeenThrown = true;
        }

        if (other.CompareTag("SpeedBoost"))
        {
            isInSpeedBoost = true;
        }
        if (other.CompareTag("Reset"))
        {
            StartCoroutine(DisableBall());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Alley"))
        {
            isInAlley = false;
        }
        if (other.CompareTag("SpeedBoost"))
            isInSpeedBoost = false;
    }

    private IEnumerator DisableBall()
    {
        yield return new WaitForSeconds(3);
        this.gameObject.SetActive(false);
    }
}