using UnityEngine;

public class PlatformParenting : MonoBehaviour
{
    #region Unity lifecycle

    private void Awake()
    {
        _transform = transform;
    }

    #endregion

    /// <summary>Parents the players to the platform</summary>
    /// <param name="collision">player collider</param>
    #region Collisions

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(_transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }

    #endregion

    #region Private

    private Transform _transform;

    #endregion
}
