using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VRControlSwitcher : MonoBehaviour
{
    public GameObject normalCamera;
    public GameObject vrCamera;

    private void Update()
    {
        if (XRisPresent())
        {
            normalCamera.SetActive(false);
            vrCamera.SetActive(true);
        }
        else
        {
            normalCamera.SetActive(true);
            vrCamera.SetActive(false);
        }
    }
    public static bool XRisPresent()
    {
        var xrDisplaySubsystems = new List<XRDisplaySubsystem>();
        SubsystemManager.GetInstances<XRDisplaySubsystem>(xrDisplaySubsystems);
        foreach (var xrDisplay in xrDisplaySubsystems)
        {
            if (xrDisplay.running)
            {
                return true;
            }
        }
        return false;
    }

}
