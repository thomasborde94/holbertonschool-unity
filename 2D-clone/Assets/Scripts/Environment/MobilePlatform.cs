using UnityEngine;

public class MobilePlatform : MonoBehaviour
{
    #region Show In Inspector

    [SerializeField] private Transform _transform;
    [SerializeField] private Transform _origin;
    [SerializeField] private Transform _target;
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed;

    #endregion


    #region Unity Lifecycle

    private void FixedUpdate()
    {
        Vector2 newPosition = Vector2.MoveTowards(_transform.position, _target.position, _speed * Time.fixedDeltaTime);

        _transform.position = newPosition;

        if (Vector3.Distance(_transform.position, _target.position) < .1f)
        {
            SwitchTargets();
        }
    }

    #endregion


    #region Private methods
    /// <summary>Gives new destination to platform when reaches its target</summary>
    private void SwitchTargets()
    {
        Transform temp = _origin;
        _origin = _target;
        _target = temp;
    }

    #endregion


    #region Private

    private Transform _currentTarget;
    private int _currentIndex;
    private bool _incr;

    #endregion
}
