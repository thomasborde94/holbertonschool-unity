using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : MonoBehaviour
{
    #region Show In Inspector

    [SerializeField] private PlayerMoveControllerWithStateMachine _playerMove;

    #endregion


    #region Unity Lifecycle

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetFloat(_speedXId, Mathf.Abs(_playerMove.SpeedXNormalized));
        _animator.SetFloat(_speedYId, _playerMove.SpeedY);
    }

    #endregion


    #region Private

    private Animator _animator;

    private int _speedXId = Animator.StringToHash("SpeedX");
    private int _speedYId = Animator.StringToHash("SpeedY");

    #endregion
}
