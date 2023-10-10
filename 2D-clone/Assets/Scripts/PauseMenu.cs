using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseCanvas;
    [SerializeField] GameObject winCanvas;
    private bool isPaused = false;
    private string currentScene;

    private void Awake()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !winCanvas.activeSelf)
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    /// <summary>Unpauses the game</summary>
    public void Resume()
    {
        pauseCanvas.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    /// <summary>Pauses the game</summary>
    public void Pause()
    {
        pauseCanvas.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    /// <summary>Restarts level</summary>
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(currentScene);
    }
    /// <summary>Back to main menu</summary>
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    /// <summary>Exits Game</summary>
    public void Exit()
    {
        Application.Quit();
        Debug.Log("Exited");
    }
}
