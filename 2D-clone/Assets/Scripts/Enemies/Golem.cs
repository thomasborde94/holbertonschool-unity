using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : Enemy, IDamageable
{
    /// <summary>
    /// Defines attacks behavior of the golem enemy
    /// </summary>
    public override void Attack()
    {
        float distance = Vector3.Distance(_transform.localPosition, _player.transform.localPosition);
        if (distance > 12f)
        {
            isHit = false;
            _anim.SetBool("InCombat", false);
        }
        if (distance < 8f)
        {
            isHit = true;
            _anim.SetBool("InCombat", true);
            _anim.SetBool("CanAttack", false);
        }
        if (_anim.GetBool("CanAttack") == false)
        {
            _elapsedTimeSinceAttack += Time.deltaTime;
            if (_elapsedTimeSinceAttack >= _timeBeforeAttack)
            {
                _anim.SetBool("CanAttack", true);
                _elapsedTimeSinceAttack = 0f;
            }
        }
    }

    private float _timeBeforeAttack = 1.5f;
    private float _elapsedTimeSinceAttack = 0f;
}
