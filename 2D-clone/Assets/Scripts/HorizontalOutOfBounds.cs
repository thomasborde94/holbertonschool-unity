using UnityEngine;

public class HorizontalOutOfBounds : MonoBehaviour
{
    public static HorizontalOutOfBounds instance;
    #region Show In Inspector

    [SerializeField] private Transform _boundYLevel;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] public Transform _respawnPoint;
    [SerializeField] private Color _gizmosColor;
    [SerializeField] private IntVariable _score;

    #endregion


    #region Unity Lifecycle
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        if (IsBelow(_playerTransform.position))
        {
            _score.Value -= 30;
            _playerTransform.position = _respawnPoint.position;
            if (_playerTransform.TryGetComponent(out Rigidbody2D rigidbody))
            {
                rigidbody.velocity = Vector2.zero;
            }
        }
    }

    #endregion


    #region Public methods
    public bool IsBelow(Vector2 point)
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
