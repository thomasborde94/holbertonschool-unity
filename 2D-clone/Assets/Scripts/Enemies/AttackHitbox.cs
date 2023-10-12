using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    /// <summary>Damages the player if he touched the enemy hitbox</summary>
    /// <param name="other">Player collider</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            other.GetComponent<PlayerMoveControllerWithStateMachine>().Damage(1);
    }
}
