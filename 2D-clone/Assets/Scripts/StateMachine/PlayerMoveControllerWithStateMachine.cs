using UnityEngine;
using System.Collections;

public class PlayerMoveControllerWithStateMachine : MonoBehaviour, IDamageable
{
    #region Show In Inspector

    [SerializeField] private VerticalMovementStateMachine _movementStateMachine;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private int _maxExtraJumpsCount;
    [SerializeField] private float _fallGravityScale;
    [SerializeField] private float _shortJumpGravityScale;
    [SerializeField] private float _longJumpGravityScale;

    [SerializeField] public int _health;
    [SerializeField] private IntVariable score;

    public int Health
    {
        get { return _health; } // Getter returns the value of _health
        set { _health = value; } // Setter assigns the value to _health
    }

    #endregion


    #region Public properties

    public float SpeedXNormalized
    {
        get
        {
            return _rigidbody.velocity.x / _movementSpeed;
        }
    }

    public float SpeedY
    {
        get
        {
            return _rigidbody.velocity.y;
        }
    }

    public int ExtraJumpsCount
    {
        get
        {
            return _extraJumpsCount;
        }
    }

    #endregion


    #region Unity Lifecycle

    private void Awake()
    {
        _theSr = GetComponentsInChildren<SpriteRenderer>();
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody2D>();
        _extraJumpsCount = _maxExtraJumpsCount;
        Time.timeScale = 1f;
    }

    private void Update()
    {
        // Déplacement horizontal
        Vector2 inputDirection = new Vector2(Input.GetAxis("Horizontal"), 0);
        _horizontalVelocity = inputDirection * _movementSpeed;

        // Flip.x en fonction de la direction du déplacement
        if (!Mathf.Approximately(inputDirection.x, 0))
        {
            _transform.right = inputDirection;
        }

        // Est-ce qu'on fait un saut "long" ? (on laisse le bouton saut appuyé)
        if (Input.GetButton("Jump"))
        {
            _isLongJump = true;
        }
    }

    private void FixedUpdate()
    {
        // Applique le mouvement horizontal au rigidbody, en reportant le mouvement vertical calculé précédemment par Unity
        Vector2 verticalVelocity = new Vector2(0, _rigidbody.velocity.y);

        Vector2 velocity = _horizontalVelocity + verticalVelocity;

        _rigidbody.velocity = velocity;

        // Applique le saut
        if (_doJump)
        {
            // On coupe d'abord le déplacement vertical existant
            velocity.y = 0;
            _rigidbody.velocity = velocity;

            // Puis on applique la force
            Vector2 jumpForce = Vector2.up * _jumpForce;
            _rigidbody.AddForce(jumpForce, ForceMode2D.Impulse);

            _doJump = false;
        }

        if (_rigidbody.velocity.y < -0.1f)
        {
            //_rigidbody.velocity += Physics2D.gravity * (_fallGravityScale - 1) * Time.fixedDeltaTime;
            _rigidbody.gravityScale = _fallGravityScale;
        }
        else if (_rigidbody.velocity.y > 0.1f && !_isLongJump)
        {
            //_rigidbody.velocity += Physics2D.gravity * (_longJumpGravityScale - 1) * Time.fixedDeltaTime;
            _rigidbody.gravityScale = _shortJumpGravityScale;
        }
        else
        {
            _rigidbody.gravityScale = _longJumpGravityScale;
        }

        _isLongJump = false;
    }

    #endregion


    #region Public methods

    public void DoJump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
        _doJump = true;
    }

    public void DoExtraJump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
        _extraJumpsCount--;
        _doJump = true;
    }

    public void ResetExtraJumps()
    {
        _extraJumpsCount = _maxExtraJumpsCount;
    }

    public void Damage(int damage)
    {
        Health -= damage;
        score.Value -= 10;
        StartCoroutine(BlinkSprite());

        if (Health < 1)
        {
            _transform.position = HorizontalOutOfBounds.instance._respawnPoint.position;
            if (_transform.TryGetComponent(out Rigidbody2D rigidbody))
            {
                rigidbody.velocity = Vector2.zero;
            }
            Health = 5;
        }
    }
    #endregion

    #region Coroutines
    private IEnumerator BlinkSprite()
    {
        float elapsedTime = 0f;
        bool isVisible = true;

        while (elapsedTime < blinkDuration)
        {
            foreach (SpriteRenderer spriteRenderer in _theSr)
                spriteRenderer.enabled = isVisible;

            isVisible = !isVisible; // Toggle visibility

            yield return new WaitForSeconds(blinkRate);
            elapsedTime += blinkRate;
        }

        foreach (SpriteRenderer spriteRenderer in _theSr)
            spriteRenderer.enabled = true;
    }



    #endregion
    #region Private

    private Transform _transform;
    private Rigidbody2D _rigidbody;
    private Vector2 _horizontalVelocity;
    private bool _doJump;
    private bool _isLongJump;
    private int _extraJumpsCount;

    private SpriteRenderer[] _theSr;
    private float blinkDuration = 1.2f;
    private float blinkRate = 0.15f;

    #endregion
}
