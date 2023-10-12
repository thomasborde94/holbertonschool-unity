using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderboardUI : MonoBehaviour
{
    [SerializeField] private PlayerDataArray _playerDataArray;
    [SerializeField] private TMP_Text player1NameText;
    [SerializeField] private TMP_Text player1ScoreText;
    [SerializeField] private TMP_Text player2NameText;
    [SerializeField] private TMP_Text player2ScoreText;
    [SerializeField] private TMP_Text player3NameText;
    [SerializeField] private TMP_Text player3ScoreText;
    [SerializeField] private TMP_Text player4NameText;
    [SerializeField] private TMP_Text player4ScoreText;
    [SerializeField] private TMP_Text player5NameText;
    [SerializeField] private TMP_Text player5ScoreText;

    void Start()
    {
        LeaderboardSetUp();
    }

    private void Update()
    {
        LeaderboardSetUp();
    }
    /// <summary>Displays leaderboard in leaderboard scene</summary>
    private void LeaderboardSetUp()
    {
        player1NameText.text = _playerDataArray.playerRecords[0].playerName;
        player2NameText.text = _playerDataArray.playerRecords[1].playerName;
        player3NameText.text = _playerDataArray.playerRecords[2].playerName;
        player4NameText.text = _playerDataArray.playerRecords[3].playerName;
        player5NameText.text = _playerDataArray.playerRecords[4].playerName;

        player1ScoreText.text = _playerDataArray.playerRecords[0].score.ToString();
        player2ScoreText.text = _playerDataArray.playerRecords[1].score.ToString();
        player3ScoreText.text = _playerDataArray.playerRecords[2].score.ToString();
        player4ScoreText.text = _playerDataArray.playerRecords[3].score.ToString();
        player5ScoreText.text = _playerDataArray.playerRecords[4].score.ToString();
    }
    /// <summary>Reset leaderboard</summary>
    public void ClearLeaderboard()
    {
        _playerDataArray.playerRecords[0].playerName = "Player1";
        _playerDataArray.playerRecords[1].playerName = "Player2";
        _playerDataArray.playerRecords[2].playerName = "Player3";
        _playerDataArray.playerRecords[3].playerName = "Player4";
        _playerDataArray.playerRecords[4].playerName = "Player5";

        _playerDataArray.playerRecords[0].score = 0;
        _playerDataArray.playerRecords[1].score = 0;
        _playerDataArray.playerRecords[2].score = 0;
        _playerDataArray.playerRecords[3].score = 0;
        _playerDataArray.playerRecords[4].score = 0;
    }
}
