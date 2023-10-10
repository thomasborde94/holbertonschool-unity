using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region Show In Inspector

    [SerializeField] private IntVariable _totalAleCount;
    [SerializeField] private IntVariable _aleCount;
    [SerializeField] private IntVariable _score;
    [SerializeField] private BoolVariable _isLevelWin;

    #endregion


    #region Unity Lifecycle

    private void Awake()
    {
        _totalAleCount.Value = GameObject.FindGameObjectsWithTag("Ale").Length;
        _aleCount.Value = 0;
        _isLevelWin.Value = false;
        _score.Value = 0;
    }

    #endregion
}
