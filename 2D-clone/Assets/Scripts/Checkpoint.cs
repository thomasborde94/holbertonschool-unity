﻿using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    #region Show In Inspector

    [SerializeField] private Transform _spawPoint;


    #endregion


    #region Collisions

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _spawPoint.position = transform.position;
        }
    }

    #endregion
}
