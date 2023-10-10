using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationControllerWithStateMachine : MonoBehaviour
{
    #region Show In Inspector

    [SerializeField]
    private PlayerMoveControllerWithStateMachine _playerMove;

    #endregion


    #region Unity Lifecycle

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetFloat(_speedXId, Mathf.Abs(_playerMove.SpeedXNormalized));
    }

    #endregion

    /// <summary>
    /// Updates Player animator
    /// </summary>
    #region Public methods

    public void EnterStateGrounded()
    {
        _animator.SetBool(_groundedId, true);
    }

    public void ExitStateGrounded()
    {
        _animator.SetBool(_groundedId, false);
    }

    public void EnterStateJumping()
    {
        _animator.SetBool(_jumpingId, true);
    }

    public void ExitStateJumping()
    {
        _animator.SetBool(_jumpingId, false);
    }

    public void EnterStateFalling()
    {
        _animator.SetBool(_fallingId, true);
    }

    public void ExitStateFalling()
    {
        _animator.SetBool(_fallingId, false);
    }

    public void ThrowWeaponAnimation()
    {
        _animator.SetTrigger(_throwWeaponId);
    }

    public void CatchWeaponAnimation()
    {
        _animator.SetTrigger(_catchWeaponId);
    }

    #endregion


    #region Private

    private Animator _animator;

    private int _speedXId = Animator.StringToHash("SpeedX");
    private int _groundedId = Animator.StringToHash("StateGrounded");
    private int _jumpingId = Animator.StringToHash("StateJumping");
    private int _fallingId = Animator.StringToHash("StateFalling");
    private int _throwWeaponId = Animator.StringToHash("ThrowWeapon");
    private int _catchWeaponId = Animator.StringToHash("CatchWeapon");

    #endregion
}
