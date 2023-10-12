
using UnityEngine;

/// <summary>
/// Vertical Movement State Machine. Contains all possible states
/// </summary>
public enum VerticalMovementState
{
    GROUNDED,
    JUMPING,
    EXTRA_JUMPING,
    FALLING
}

public class VerticalMovementStateMachine : MonoBehaviour
{
    #region Show in inspector

    [SerializeField] private PlayerMoveControllerWithStateMachine _playerMoveController;
    [SerializeField] private AnimationControllerWithStateMachine _animationController;
    [SerializeField] private GroundCheckerWithOverlapArea _groundChecker;

    #endregion


    #region Unity Lifecycle

    private void Update()
    {
        OnStateUpdate(_currentState);
    }

    #endregion


    #region State Machine

    /// <summary>
    /// When we enter a state
    /// </summary>
    /// <param name="state">State to enter</param>
    private void OnStateEnter(VerticalMovementState state)
    {
        switch (state)
        {
            case VerticalMovementState.GROUNDED:
                DoGroundedEnter();
                break;

            case VerticalMovementState.JUMPING:
                DoJumpingEnter();
                break;

            case VerticalMovementState.EXTRA_JUMPING:
                DoExtraJumpingEnter();
                break;

            case VerticalMovementState.FALLING:
                DoFallingEnter();
                break;

            default:
                Debug.LogError("OnStateEnter: Invalid state " + state.ToString());
                break;
        }
    }

    /// <summary>
    /// When we exit a state
    /// </summary>
    /// <param name="state">State to exit</param>
    private void OnStateExit(VerticalMovementState state)
    {
        switch (state)
        {
            case VerticalMovementState.GROUNDED:
                DoGroundedExit();
                break;

            case VerticalMovementState.JUMPING:
                DoJumpingExit();
                break;

            case VerticalMovementState.EXTRA_JUMPING:
                DoExtraJumpingExit();
                break;

            case VerticalMovementState.FALLING:
                DoFallingExit();
                break;

            default:
                Debug.LogError("OnStateExit: Invalid state " + state.ToString());
                break;
        }
    }

    /// <summary>
    /// When we are in a state
    /// </summary>
    /// <param name="state">Current state</param>
    private void OnStateUpdate(VerticalMovementState state)
    {
        switch (state)
        {
            case VerticalMovementState.GROUNDED:
                DoGroundedUpdate();
                break;

            case VerticalMovementState.JUMPING:
                DoJumpingUpdate();
                break;

            case VerticalMovementState.EXTRA_JUMPING:
                DoExtraJumpingUpdate();
                break;

            case VerticalMovementState.FALLING:
                DoFallingUpdate();
                break;

            default:
                Debug.LogError("OnStateUpdate: Invalid state " + state.ToString());
                break;
        }
    }

    /// <summary>
    /// Allows transition to another state
    /// </summary>
    /// <param name="fromState">State we exit</param>
    /// <param name="toState">State we enter</param>
    private void TransitionToState(VerticalMovementState fromState, VerticalMovementState toState)
    {
        OnStateExit(fromState);
        _currentState = toState;
        OnStateEnter(toState);
    }

    #endregion


    #region State Grounded
    /// <summary> Methods to call when entering grounded state</summary>
    private void DoGroundedEnter()
    {
        _playerMoveController.ResetExtraJumps();
        _animationController.EnterStateGrounded();
    }
    /// <summary> Method to call when leaving grounded state</summary>
    private void DoGroundedExit()
    {
        _animationController.ExitStateGrounded();
    }
    /// <summary> Methods to call when in grounded state and wanting to change state</summary>
    private void DoGroundedUpdate()
    {
        if (Input.GetButtonDown("Jump"))
        {
            TransitionToState(_currentState, VerticalMovementState.JUMPING);
        }
        else if (!_groundChecker.CheckGround())
        {
            TransitionToState(_currentState, VerticalMovementState.FALLING);
        }
    }

    #endregion


    #region State Jumping
    /// <summary> Methods to call when entering jump state</summary>
    private void DoJumpingEnter()
    {
        _playerMoveController.DoJump();
        _animationController.EnterStateJumping();
    }
    /// <summary> Method to call when leaving jump state</summary>
    private void DoJumpingExit()
    {
        _animationController.ExitStateJumping();
    }
    /// <summary> Methods to call when in jump state and wanting to change state</summary>
    private void DoJumpingUpdate()
    {
        if (Input.GetButtonDown("Jump") && _playerMoveController.ExtraJumpsCount > 0)
        {
            TransitionToState(_currentState, VerticalMovementState.EXTRA_JUMPING);
        }
        else if (_playerMoveController.SpeedY < -0.1f)
        {
            TransitionToState(_currentState, VerticalMovementState.FALLING);
        }
    }

    #endregion


    #region State Extra Jumping

    /// <summary> Methods to call when entering extrajump state</summary>
    private void DoExtraJumpingEnter()
    {
        _playerMoveController.DoExtraJump();
        _animationController.EnterStateJumping();
    }
    /// <summary> Method to call when leaving extrajump state</summary>
    private void DoExtraJumpingExit()
    {
        _animationController.ExitStateJumping();
    }
    /// <summary> Methods to call when in extrajump state and wanting to change state</summary>
    private void DoExtraJumpingUpdate()
    {
        if (Input.GetButtonDown("Jump") && _playerMoveController.ExtraJumpsCount > 0)
        {
            TransitionToState(_currentState, VerticalMovementState.EXTRA_JUMPING);
        }
        else if (_playerMoveController.SpeedY < -0.1f)
        {
            TransitionToState(_currentState, VerticalMovementState.FALLING);
        }
    }

    #endregion


    #region State Falling
    /// <summary> Method to call when entering falling state</summary>
    private void DoFallingEnter()
    {
        _animationController.EnterStateFalling();
    }
    /// <summary> Method to call when leaving falling state</summary>
    private void DoFallingExit()
    {
        _animationController.ExitStateFalling();
    }
    /// <summary> Methods to call when in falling state and wanting to change state</summary>
    private void DoFallingUpdate()
    {
        if (Input.GetButtonDown("Jump") && _playerMoveController.ExtraJumpsCount > 0)
        {
            TransitionToState(_currentState, VerticalMovementState.EXTRA_JUMPING);
        }
        else if (_groundChecker.CheckGround())
        {
            TransitionToState(_currentState, VerticalMovementState.GROUNDED);
        }
    }

    #endregion


    /*#region Debug

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle("button");
        style.fontSize = 28;
        style.alignment = TextAnchor.MiddleLeft;

        using (new GUILayout.AreaScope(new Rect(Screen.width - 400, 50, 350, 100)))
        {
            using (new GUILayout.VerticalScope())
            {
                GUILayout.Button($"State: {_currentState}", style, GUILayout.ExpandHeight(true));
                GUILayout.Button($"Extra jumps: {_playerMoveController.ExtraJumpsCount}", style, GUILayout.ExpandHeight(true));
            }
        }
    }

    #endregion*/


    #region Private

    private VerticalMovementState _currentState;

    #endregion
}
