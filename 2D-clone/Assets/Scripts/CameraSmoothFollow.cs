using UnityEngine;

public class CameraSmoothFollow : MonoBehaviour
{
    #region Show In Inspector

    [SerializeField] private Transform _target;
    [SerializeField] private float _smoothTime;
    [SerializeField] private float _maxSpeed;

    #endregion


    #region Unity Lifecycle

    private void Awake()
    {
        _transform = transform;
    }

    private void FixedUpdate()
    {
        Vector3 origin = _transform.position;

        Vector3 target = _target.position;
        target.z = origin.z;

        _transform.position = Vector3.SmoothDamp(origin, target, ref _velocity, _smoothTime, _maxSpeed);
    }

    #endregion


    #region Private

    private Transform _transform;
    private Vector3 _velocity;

    #endregion
}
