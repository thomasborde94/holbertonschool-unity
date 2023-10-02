using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    private string lastSceneName = "MainMenu";

    private void Awake()
    {
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
    }
    #region Public methods
    public void Back()
    {
        SceneManager.LoadScene(lastSceneName);
    }
    #endregion
}
