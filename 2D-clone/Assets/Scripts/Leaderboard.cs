using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class Leaderboard : MonoBehaviour
{
    #region Show in Inspector
    [SerializeField] private TMP_Text saveButtonText;
    [SerializeField] private TMP_InputField nameInputField;

    [SerializeField] private PlayerDataArray _playerDataArray;
    [SerializeField] private PlayerData playerDataTemp;
    [SerializeField] private IntVariable score;
    #endregion

    private void Awake()
    {
        saveButtonText.text = "Save score";
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
            SaveScoreToPlayerDataArray();
        }
    }
    #endregion

    #region Private Methods
    private void SaveScoreToPlayerDataArray()
    {
        // Create a new PlayerData for the current player
        playerDataTemp.score = playerScore;
        playerDataTemp.playerName = playerName;

        // Add the new player's data to the array and sorts it
        _playerDataArray.playerRecords = AddPlayerDataToLeaderboard(playerDataTemp, _playerDataArray.playerRecords);
        SortLeaderboard(_playerDataArray.playerRecords);
    }

    /// <summary>
    /// Sorts the playerDataArray
    /// </summary>
    /// <param name="newPlayerData">new PlayerData</param>
    /// <param name="leaderboard">leaderBoard</param>
    /// <returns></returns>
    private PlayerData[] AddPlayerDataToLeaderboard(PlayerData newPlayerData, PlayerData[] leaderboard)
    {
        
        var updatedLeaderboard = leaderboard.ToList();
        for (int i = 0; i < updatedLeaderboard.Count; i++)
        {
            if (playerDataTemp.score >= updatedLeaderboard[i].score)
            {
                for (int j = updatedLeaderboard.Count - 1; j > i; j--)
                {
                    updatedLeaderboard[j].score = updatedLeaderboard[j - 1].score;
                    updatedLeaderboard[j].playerName = updatedLeaderboard[j - 1].playerName;
                }
                updatedLeaderboard[i].score = playerDataTemp.score;
                updatedLeaderboard[i].playerName = playerDataTemp.playerName;
                break;
            }
        }

        // Sort the leaderboard to ensure it's in descending order
        SortLeaderboard(updatedLeaderboard.ToArray());
        
        return updatedLeaderboard.ToArray();
    }

    private void SortLeaderboard(PlayerData[] leaderboard)
    {
        Array.Sort(leaderboard, (a, b) => b.score.CompareTo(a.score));
    }

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
