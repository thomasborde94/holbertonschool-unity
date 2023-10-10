using UnityEngine;

public class AlePickup : MonoBehaviour
{
    #region Show In Inspector

    [SerializeField] private IntVariable _aleCount;
    [SerializeField] private IntVariable _score;

    #endregion


    #region Collisions

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ale"))
        {
            _aleCount.Value++;
            _score.Value += 10;
            Destroy(collision.gameObject);
        }
    }

    #endregion
}
