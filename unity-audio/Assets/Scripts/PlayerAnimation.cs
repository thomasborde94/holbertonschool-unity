using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    #region Show in Inspector

    [SerializeField] PlayerController _player;
    [SerializeField] GroundCheckerWithOverlap _groundTester;

    #endregion
    #region Unity Lifecycle
    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        _anim.SetBool("Move", _player.Move);
        _anim.SetBool("Jumping", _player.IsJumping);
        _anim.SetBool("Falling", _player.Falling);
    }
    #endregion

    public void allowMoving()
    {
        _player.canMove = true;
    }

    #region Animation Events

    public void FootstepsSFX()
    {
        if (_groundTester.TestCollision())
        {
            if (GroundCheckerWithOverlap.instance._buffer[0].tag == "StoneGround")
                AudioManager.instance.clipState = 3;
            if (GroundCheckerWithOverlap.instance._buffer[0].tag == "GrassGround")
                AudioManager.instance.clipState = 4;
            AudioManager.instance.canPlayClip = true;
        }
    }

    public void LandingSFX()
    {
        if (GroundCheckerWithOverlap.instance._buffer[0].tag == "StoneGround")
            AudioManager.instance.clipState = 5;
        if (GroundCheckerWithOverlap.instance._buffer[0].tag == "GrassGround")
            AudioManager.instance.clipState = 6;
        AudioManager.instance.canPlayClip = true;
    }

    #endregion

    #region Private

    private Animator _anim;

    #endregion
}
