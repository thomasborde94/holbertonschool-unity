using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public static OptionsMenu instance;
    [HideInInspector] public string toggleId = "yInvertedId";
    [SerializeField] public Toggle yInverted;
    

    private void Awake()
    {
        instance = this;
        Time.timeScale = 1f;
    }
    private void Start()
    {
        if (SceneManager.GetActiveScene().name != "Options")
        {
            PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
            PlayerPrefs.Save();
        }
        lastSceneName = PlayerPrefs.GetString("LastScene");

        if (SceneManager.GetActiveScene().name == "Options")
            LoadToggleState();
    }
    #region Public methods

    public void Back()
    {
        SceneManager.LoadScene(lastSceneName);
    }

    public void Apply()
    {
        toggleState = yInverted.isOn ? 1 : 0;
        PlayerPrefs.SetInt(toggleId, toggleState);
        PlayerPrefs.Save();
        Back();
    }
    #endregion

    private void LoadToggleState()
    {
        int toggleState = PlayerPrefs.GetInt(toggleId, 0);
        if (toggleState == 1)
            yInverted.isOn = true;
        else
            yInverted.isOn = false;
    }

    #region Private
    private string lastSceneName = "MainMenu";
    private int toggleState;
    #endregion
}
