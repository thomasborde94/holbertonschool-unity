using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("hit by attack hitbox");
            other.GetComponent<PlayerMoveControllerWithStateMachine>().Damage(1);
        }
    }
}
