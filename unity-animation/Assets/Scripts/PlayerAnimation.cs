using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    #region Show in Inspector

    [SerializeField] PlayerController _player;

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

    #region Private

    private Animator _anim;

    #endregion
}
