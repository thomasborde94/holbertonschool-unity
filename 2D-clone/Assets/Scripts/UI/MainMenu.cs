using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /// <summary>Loads level1</summary>
    public void LoadLevel()
    {
        SceneManager.LoadScene("Level1");
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
