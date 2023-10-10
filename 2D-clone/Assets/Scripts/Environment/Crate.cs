using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    #region Show in Inspector

    [SerializeField] private Sprite _crateOpen;
    [SerializeField] private SpriteRenderer _sr;

    #endregion
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _sr.sprite = _crateOpen;
            PlayerAttackController.instance._hasWeapon = true;
        }
    }

    
}
