using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected Animator _anim;
    [SerializeField] protected SpriteRenderer _theSr;
    [SerializeField] protected int _health;
    [SerializeField] protected float _speed;
    [SerializeField] protected Transform pointA, pointB;
    [SerializeField] protected IntVariable score;

    protected Vector3 _currentTarget;
    protected Transform _transform;

    protected bool isHit = false;
    protected PlayerMoveControllerWithStateMachine[] _player = new PlayerMoveControllerWithStateMachine[2];
    protected bool isDead = false;

    public int Health
    {
        get { return _health; } // Getter returns the value of _health
        set { _health = value; } // Setter assigns the value to _health
    }

    #region Unity Lifecycle

    private void Start()
    {
        Init();
    }

    public virtual void Update()
    {
        if (isDead)
            return;
        if (jumpAttacked)
        {
            _playerRb.velocity = new Vector2(_playerRb.velocity.x, 0);
            _playerRb.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
            jumpAttacked = false;
        }
        if (Health < 1)
        {
            isDead = true;
            _anim.SetBool("Death", true);
        }

        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && _anim.GetBool("InCombat") == false)
        {
            return;
        }
        if (isDead == false)
        {
            Movement();
            Attack();
        }
        
        
    }
    #endregion

    #region Public methods
    /// <summary>
    /// Enemy Movement behavior  
    /// </summary>
    public virtual void Movement()
    {
        if (_currentTarget == pointA.position)
            _theSr.flipX = true;
        else
            _theSr.flipX = false;

        if (_transform.position == pointA.position)
        {
            _currentTarget = pointB.position;
            _anim.SetTrigger("Idle");

        }
        else if (_transform.position == pointB.position)
        {
            _currentTarget = pointA.position;
            _anim.SetTrigger("Idle");
        }
        if (isHit == false)
        {
            _transform.position = Vector3.MoveTowards(_transform.position, _currentTarget, _speed * Time.deltaTime);
        }

        // Facing direction 
        Vector3 direction = _player[0].transform.localPosition - _transform.localPosition;
        if (direction.x > 0 && _anim.GetBool("InCombat") == true)
        {
            _theSr.flipX = false;
        }
        else if (direction.x < 0 && _anim.GetBool("InCombat") == true)
        {
            _theSr.flipX = true;
        }
    }
    /// <summary>Defines the attack of the enemy</summary>
    public virtual void Attack()
    {

    }
    /// <summary>Initializes enemies</summary>
    public virtual void Init()
    {
        _transform = GetComponent<Transform>();
        _player[0] = GameObject.Find("Player1").GetComponent<PlayerMoveControllerWithStateMachine>();
        GameObject player2GameObject = GameObject.Find("Player2");
        if (player2GameObject != null)
        {
            PlayerMoveControllerWithStateMachine player2Component = player2GameObject.GetComponent<PlayerMoveControllerWithStateMachine>();
            if (player2Component != null)
            {
                _player[1] = player2Component;
            }
        }
        _anim.SetBool("CanAttack", true);
    }
    #endregion

    #region Private methods
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isDead)
        {
            if (other.attachedRigidbody.velocity.y < 0)
            {
                Debug.Log("jumped on enemy");
                _playerRb = other.attachedRigidbody;
                jumpAttacked = true;
                Damage(1);
            }
            else
            {
                if (_canDamage == true)
                {
                    _canDamage = false;
                    other.GetComponent<PlayerMoveControllerWithStateMachine>().Damage(1);
                    StartCoroutine(ResetDamage());
                }
            }
        }
        if (other.CompareTag("PlayerWeapon") && !isDead)
        {
            Debug.Log("hit by weapon");
            Damage(2);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isDead)
        {
            Debug.Log("hit player");
            if (_canDamage == true)
            {
                _canDamage = false;
                other.GetComponent<PlayerMoveControllerWithStateMachine>().Damage(1);
                StartCoroutine(ResetDamage());
            }
        }
    }

    public void Damage(int damage)
    {
        if (isDead)
        {
            return;
        }
        Health -= damage;
        _anim.SetTrigger("Hit");
        isHit = true;
        Debug.Log("should be in combat");
        _anim.SetBool("InCombat", true);
        score.Value += damage * 5;
        
    }
    #endregion

    IEnumerator ResetDamage()
    {
        yield return new WaitForSeconds(1.2f);
        _canDamage = true;
    }
    #region Private
    private bool jumpAttacked = false;
    private Rigidbody2D _playerRb;
    private bool _canDamage = true;
    #endregion
}
