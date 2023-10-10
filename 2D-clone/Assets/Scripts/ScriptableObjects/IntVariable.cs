using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Int")]
public class IntVariable : ScriptableObject
{
    #region Show In Inspector

    [SerializeField]
    private int _value;

    #endregion


    #region Public properties

    public int Value
    {
        get => _value;
        set => _value = value;
    }

    #endregion
}
