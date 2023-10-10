using UnityEngine;

public class DestructibleColumn : MonoBehaviour
{
    #region Show In Inspector

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite[] _sprites;

    #endregion


    #region Collisions

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("PlayerWeapon"))
        {
            ColumnHit();
        }
    }

    #endregion


    #region Unity lifecycle

    private void Awake()
    {
        _spriteRenderer.sprite = _sprites[0];
    }

    #endregion


    #region Private methods

    /// <summary>Updates sprite of column when getting hit</summary>
    private void ColumnHit()
    {
        _hitCount++;
        if (_hitCount >= _sprites.Length)
            Destroy(gameObject);
        else
            _spriteRenderer.sprite = _sprites[_hitCount];
    }

    #endregion


    #region Private

    private int _hitCount;

    #endregion
}
