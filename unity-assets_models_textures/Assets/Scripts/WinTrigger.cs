using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    public static WinTrigger instance;

    [HideInInspector] public bool hasFinished = false;

    private void Awake()
    {
        instance = this;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            hasFinished = true;
        }
    }
}
