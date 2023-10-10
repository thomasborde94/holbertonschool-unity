using UnityEngine;

public class ThrowWeaponAnimationController : MonoBehaviour
{
    #region Show In Inspector

    [SerializeField]
    private Transform _weapon;

    [SerializeField]
    private Transform _weaponGraphics;

    #endregion


    #region Main methods
    /// <summary>Displays throwed weapon</summary>
    public void ThrowWeapon()
    {
        if (PlayerAttackController.instance._hasWeapon)
        {
            _weapon.gameObject.SetActive(true);
            _weaponGraphics.gameObject.SetActive(false);
        }
    }

    #endregion
}
