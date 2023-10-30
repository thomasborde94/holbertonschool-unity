using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    #region Public methods
    public void LevelSelect(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void Options()
    {
        SceneManager.LoadScene("Options");
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("Exited");
    }
    #endregion
}
