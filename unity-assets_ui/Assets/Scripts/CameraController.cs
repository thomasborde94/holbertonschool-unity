using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Show in Inspector

    [SerializeField] private float _mouseSensitivity;
    [SerializeField] Transform _target;

    #endregion

    private void Awake()
    {
        _offset = transform.position - _target.position;
    }
    private void Update()
    {
        // Gets the mousePosition inputs
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity;

        // calculates the new camera offset based on mouse movement
        // using -transform.right so the camera z offset does not depend on where the camera is looking.
        _offset = Quaternion.AngleAxis(mouseX * _mouseSensitivity, Vector3.up) *
            Quaternion.AngleAxis(mouseY * _mouseSensitivity, -transform.right) * _offset;

        //After calculating the new camera offset, updates the camera's position to be relative to the target.
        transform.position = _target.position + _offset;

        // Ensures that the camera always faces the target object
        transform.LookAt(_target.position);
    }

    #region Private

    private Vector3 _offset;

    #endregion
}
