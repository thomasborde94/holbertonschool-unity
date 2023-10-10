using UnityEngine;

public class WeaponMovement : MonoBehaviour
{
    #region Show In Inspector

    [SerializeField] private Transform _weaponGraphics;
    [SerializeField] private float _speed;
    [SerializeField] private float _returnSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Transform _origin;
    [SerializeField] private Transform _target;
    [SerializeField] private PlayerAttackController _attackController;

    #endregion


    #region Unity Lifecycle

    private void Awake()
    {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        _transform.position = _weaponGraphics.position;
        _transform.rotation = _weaponGraphics.rotation;
        _currentTarget = _target.position;
        _isBackTravel = false;
        _collider.enabled = true;
    }

    private void OnDisable()
    {
        _weaponGraphics.gameObject.SetActive(true);
        _attackController.CatchWeapon();
    }

    private void FixedUpdate()
    {
        Vector2 newPosition = Vector2.MoveTowards(_transform.position, _currentTarget, (_isBackTravel ? _returnSpeed : _speed) * Time.fixedDeltaTime);

        _rigidbody.MovePosition(newPosition);
        _transform.Rotate(0, 0, _rotationSpeed * Time.fixedDeltaTime);

        if (Vector3.Distance(_transform.position, _currentTarget) < .1f)
        {
            if (_isBackTravel)
            {
                gameObject.SetActive(false);
                return;
            }
            _currentTarget = _origin.position;
            _isBackTravel = true;
        }
    }

    #endregion


    #region Collisions

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _currentTarget = _origin.position;
        _isBackTravel = true;
        _collider.enabled = false;
    }

    #endregion


    #region Private

    private Transform _transform;
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    private bool _isBackTravel;
    private Vector2 _currentTarget;

    #endregion
}
