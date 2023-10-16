using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class Leaderboard : MonoBehaviour
{
    #region Show in Inspector
    [SerializeField] private TMP_Text saveButtonText;
    [SerializeField] private TMP_InputField nameInputField;

    [SerializeField] private IntVariable score;
    [SerializeField] private GameObject newBestScoreUI;
    [SerializeField] private GameObject oldBestScoreUI;
    [SerializeField] private TMP_Text oldBestScoreText;
    [SerializeField] private TMP_Text bestScoreNameText;
    #endregion

    private void Awake()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "Level1")
            saveButtonText.text = "Save score";

        if (!PlayerPrefs.HasKey("PlayerBestScore"))
            PlayerPrefs.SetInt("PlayerBestScore", 0);

        //PlayerPrefs.SetInt("PlayerBestScore", 0);
        //PlayerPrefs.SetString("PlayerBestName", "Noone");
    }

    private void Start()
    {
        playerName = nameInputField.text;
        playerScore = score.Value;
        oldBestScoreText.text = "Best Score: " + PlayerPrefs.GetInt("PlayerBestScore");
    }

    private void Update()
    {
        if (hasFinished)
        {
            oldBestScoreText.text = "Best Score: " + PlayerPrefs.GetInt("PlayerBestScore");
            playerScore = score.Value;
            playerName = nameInputField.text;
            if (playerScore > PlayerPrefs.GetInt("PlayerBestScore"))
            {
                PlayerPrefs.SetString("PlayerBestName", playerName);
                bestScoreNameText.text = "By " + PlayerPrefs.GetString("PlayerBestName");
                newBestScoreUI.SetActive(true);
                oldBestScoreUI.SetActive(false);
            }
            else
            {
                newBestScoreUI.SetActive(false);
                oldBestScoreUI.SetActive(true);
            }
        }
    }
    #region Public Methods
    public void SaveScoreButton()
    {
        if (nameInputField.text != "")
        {
            playerName = nameInputField.text;
            playerScore = score.Value;
            saveButtonText.text = "Score saved";
            nameInputField.gameObject.SetActive(false);
            PlayerPrefs.SetString("PlayerBestName", playerName);
            PlayerPrefs.SetInt("PlayerBestScore", playerScore);
            PlayerPrefs.Save();
            Debug.Log("Best name : " + PlayerPrefs.GetString("PlayerBestName"));
            Debug.Log("Best score : " + PlayerPrefs.GetInt("PlayerBestScore"));
        }
    }
    #endregion

    #region Private Methods


    #endregion
    #region Public Properties
    public bool hasFinished
    {
        get => WinTrigger.instance.hasFinished;
    }
    #endregion

    #region Private


    private string playerName;
    private int playerScore;
    #endregion
}
