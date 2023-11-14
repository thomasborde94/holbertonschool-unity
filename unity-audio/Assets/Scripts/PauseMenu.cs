using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;
    [SerializeField] GameObject pauseCanvas;
    [SerializeField] AudioMixerSnapshot pausedAudio;
    [SerializeField] public AudioMixerSnapshot unpausedAudio;
    private bool isPaused = false;
    private string currentScene;

    private void Awake()
    {
        instance = this;
        currentScene = SceneManager.GetActiveScene().name;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
                unpausedAudio.TransitionTo(0.01f);
            }
            else
            {
                Pause();
                pausedAudio.TransitionTo(0.01f);
            }
        }
    }

    public void Resume()
    {
        unpausedAudio.TransitionTo(0.01f);
        pauseCanvas.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        pauseCanvas.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Restart()
    {
        unpausedAudio.TransitionTo(0.01f);
        SceneManager.LoadScene(currentScene);
    }

    public void MainMenu()
    {
        unpausedAudio.TransitionTo(0.01f);
        SceneManager.LoadScene("MainMenu");
    }
}
