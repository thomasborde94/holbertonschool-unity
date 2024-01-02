using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractions : MonoBehaviour
{
    [SerializeField] private Animator _doorAnimator;

    private void Awake()
    {
        _doorAnimator.SetBool("character_nearby", false);
    }
    public void SelectingDoor()
    {
        _doorAnimator.SetBool("character_nearby", true);
        Debug.Log("selecting door");
    }
}
