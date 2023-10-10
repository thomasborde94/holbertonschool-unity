using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Bool")]
public class BoolVariable : ScriptableObject
{
    #region Show In Inspector

    [SerializeField]
    private bool _value;

    #endregion


    #region Public properties

    public bool Value
    {
        get => _value;
        set => _value = value;
    }

    #endregion
}
