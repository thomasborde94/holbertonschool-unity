using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    public static PlayerAttackController instance;
    [HideInInspector] public bool _hasWeapon = false;
    #region Show In Inspector

    [SerializeField] private AnimationControllerWithStateMachine _animator;
    [SerializeField] private SpriteRenderer _weaponSR;
    [SerializeField] private Sprite _weaponSprite;

    #endregion

    #region Unity Lifecycle

    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        if (_hasWeapon && !_isWeaponThrown)
        {
            _weaponSR.sprite = _weaponSprite;
            _weaponSR.gameObject.SetActive(true);
        }

        if (Input.GetButtonDown("Fire2") && !_isWeaponThrown)
        {
            ThrowWeapon();
        }
    }

    #endregion

    /// <summary>Deals with weapon animations</summary>
    #region Public methods

    public void ThrowWeapon()
    {
        if (_hasWeapon)
        {
            _animator.ThrowWeaponAnimation();
            _isWeaponThrown = true;
        }
    }

    public void CatchWeapon()
    {
        if (_hasWeapon)
        {
            _animator.CatchWeaponAnimation();
            _isWeaponThrown = false;
        }
    }

    #endregion

    #region Private

    private bool _isWeaponThrown;

    #endregion
}
