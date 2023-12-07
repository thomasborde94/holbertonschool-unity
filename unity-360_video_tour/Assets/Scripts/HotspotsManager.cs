using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotspotsManager : MonoBehaviour
{
    #region Show in Inspector

    [SerializeField] Image _fadeScreen;
    [SerializeField] float _fadeSpeed;

    [SerializeField] private GameObject livingRoomSphere;
    [SerializeField] private GameObject cantinaSphere;
    [SerializeField] private GameObject cubeSphere;
    [SerializeField] private GameObject mezzanineSphere;

    [SerializeField] private GameObject livingRoomUI;
    [SerializeField] private GameObject cantinaUI;
    [SerializeField] private GameObject cubeUI;
    [SerializeField] private GameObject mezzanineUI;
    #endregion
    #region Unity Lifecycle
    private void Awake()
    {
        _fadeScreen.gameObject.SetActive(true);
    }

    private void Start()
    {
        _fadeOutBlack = true;
        _fadeToBlack = false;
    }

    private void Update()
    {
        // Displaying the right UI accordingly to the sphere
        HotspotsDisplay();
        // FadeScreen disabling
        if (_fadeOutBlack)
        {
            _fadeScreen.color = new Color(_fadeScreen.color.r, _fadeScreen.color.g, _fadeScreen.color.b, Mathf.MoveTowards(_fadeScreen.color.a, 0f, _fadeSpeed * Time.deltaTime));

            if (_fadeScreen.color.a == 0f)
            {
                _fadeOutBlack = false;
            }
        }

        // Fading out of the fadescreen
        if (_fadeToBlack)
        {
            _fadeScreen.color = new Color(_fadeScreen.color.r, _fadeScreen.color.g, _fadeScreen.color.b, Mathf.MoveTowards(_fadeScreen.color.a, 1f, _fadeSpeed * Time.deltaTime));

            if (_fadeScreen.color.a == 1f)
            {
                _fadeToBlack = false;
            }
        }
    }
    #endregion
    #region OnClick Events

    public void FromLivingToCantina()
    {
        StartFadeToBlack();
        livingRoomSphere.SetActive(false);
        cantinaSphere.SetActive(true);
        changingUI = true;
        StartCoroutine(FadeOutDelay(2f));
    }

    public void FromLivingToCube()
    {
        StartFadeToBlack();
        livingRoomSphere.SetActive(false);
        cubeSphere.SetActive(true);
        changingUI = true;
        StartCoroutine(FadeOutDelay(2f));
    }

    public void FromCantinaToLiving()
    {
        StartFadeToBlack();
        cantinaSphere.SetActive(false);
        livingRoomSphere.SetActive(true);
        changingUI = true;
        StartCoroutine(FadeOutDelay(2f));
    }

    public void FromCantinaToCube()
    {
        StartFadeToBlack();
        cantinaSphere.SetActive(false);
        cubeSphere.SetActive(true);
        changingUI = true;
        StartCoroutine(FadeOutDelay(2f));
    }

    public void FromCubeToLiving()
    {
        StartFadeToBlack();
        cubeSphere.SetActive(false);
        livingRoomSphere.SetActive(true);
        changingUI = true;
        StartCoroutine(FadeOutDelay(2f));
    }

    public void FromCubeToCantina()
    {
        StartFadeToBlack();
        cubeSphere.SetActive(false);
        cantinaSphere.SetActive(true);
        changingUI = true;
        StartCoroutine(FadeOutDelay(2f));
    }

    public void FromCubeToMezzanine()
    {
        StartFadeToBlack();
        cubeSphere.SetActive(false);
        mezzanineSphere.SetActive(true);
        changingUI = true;
        StartCoroutine(FadeOutDelay(2f));
    }

    public void FromMezzanineToCube()
    {
        StartFadeToBlack();
        mezzanineSphere.SetActive(false);
        cubeSphere.SetActive(true);
        changingUI = true;
        StartCoroutine(FadeOutDelay(2f));
    }

    private IEnumerator FadeOutDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        StartFadeOutBlack();
    }

    private IEnumerator DisplayUI(float delay, GameObject newUI)
    {
        yield return new WaitForSeconds(delay);
        newUI.SetActive(true);
    }

    #endregion
    #region Private methods
    private void StartFadeToBlack()
    {
        _fadeToBlack = true;
        _fadeOutBlack = false;
    }

    private void StartFadeOutBlack()
    {
        _fadeToBlack = false;
        _fadeOutBlack = true;
    }

    private void HotspotsDisplay()
    {
        if (livingRoomSphere.activeSelf && changingUI)
        {
            StartCoroutine(DisplayUI(1.5f, livingRoomUI));
            changingUI = false;
        }
        else if (!livingRoomSphere.activeSelf)
        {
            livingRoomUI.SetActive(false);
        }

        if (cantinaSphere.activeSelf && changingUI)
        {
            StartCoroutine(DisplayUI(1.5f, cantinaUI));
            changingUI = false;
        }
        else if (!cantinaSphere.activeSelf)
        {
            cantinaUI.SetActive(false);
        }

        if (cubeSphere.activeSelf && changingUI)
        {
            StartCoroutine(DisplayUI(1.5f, cubeUI));
            changingUI = false;
        }
        else if (!cubeSphere.activeSelf)
        {
            cubeUI.SetActive(false);
        }

        if (mezzanineSphere.activeSelf && changingUI)
        {
            StartCoroutine(DisplayUI(1.5f, mezzanineUI));
            changingUI = false;
        }
        else if (!mezzanineSphere.activeSelf)
        {
            mezzanineUI.SetActive(false);
        }
    }
    #endregion
    #region Private

    private bool _fadeToBlack;
    private bool _fadeOutBlack;

    private bool changingUI;

    #endregion
}
