using UnityEngine;

public class HorizontalOutOfBounds : MonoBehaviour
{
    #region Show In Inspector

    [SerializeField] private Transform _boundYLevel;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _respawnPoint;
    [SerializeField] private Color _gizmosColor;
    [SerializeField] private IntVariable _score;

    #endregion


    #region Unity Lifecycle
    private void Update()
    {
        if (IsBelow(_playerTransform.position))
        {
            _score.Value -= 50;
            _playerTransform.position = _respawnPoint.position;
            if (_playerTransform.TryGetComponent(out Rigidbody2D rigidbody))
            {
                rigidbody.velocity = Vector2.zero;
            }
        }
    }

    #endregion


    #region Private methods
    private bool IsBelow(Vector2 point)
    {
        return point.y < _boundYLevel.position.y;
    }

    #endregion


    #region Debug
    private void OnDrawGizmos()
    {
        Gizmos.color = _gizmosColor;
        Gizmos.DrawRay(_boundYLevel.position, Vector2.right * 1000);
        Gizmos.DrawRay(_boundYLevel.position, Vector2.left * 1000);
    }


    #endregion
}
