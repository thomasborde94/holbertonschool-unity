using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    /// <summary>Loads level1</summary>
    public void LoadLevel()
    {
        SceneManager.LoadScene("Level1");
    }

    /// <summary>Loads MainMenu</summary>
    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    /// <summary>Loads Leaderboard</summary>
    public void Leaderboard()
    {
        SceneManager.LoadScene("Leaderboard");
    }
    /// <summary>Exits Game</summary>
    public void Exit()
    {
        Application.Quit();
        Debug.Log("Exited");
    }
}
