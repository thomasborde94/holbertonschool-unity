using ExitGames.Demos.DemoAnimator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region Private

    private Rigidbody _rigidbody;
    private Transform _transform;
    private float _bulletSpeed;

    #endregion

    #region Unity Lifecycle
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        // Moves the bullet when shot
        Vector3 bulletVelocity = transform.forward * _bulletSpeed;
        Vector3 currentBulletPosition = _transform.position;
        Vector3 bulletPosition = currentBulletPosition + bulletVelocity * Time.fixedDeltaTime;
        _rigidbody.MovePosition(bulletPosition);
    }
    #endregion
    /// <summary>
    /// Gives the ball its speed
    /// </summary>
    /// <param name="Speed">speed of the ball</param>
    public void Shoot(float Speed)
    {
        _bulletSpeed = Speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerManager opponent = other.GetComponent<PlayerManager>();
            opponent.Health -= 0.05f;
            Debug.Log("opponent got hit");
        }
    }
}
