using TMPro;

using UnityEngine;

public class UIController : MonoBehaviour
{
    #region Show In Inspector

    [Header("Ale counter")]
    [SerializeField] private IntVariable _aleCount;
    [SerializeField] private IntVariable _totalAleCount;
    [SerializeField] private GameObject aleUi;
    [SerializeField] private TMP_Text _aleCountText;
    [SerializeField] private TMP_Text _aleCountWin;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _scoreWinText;
    [SerializeField] private IntVariable score;

    [SerializeField] public TextMeshProUGUI timerText;
    [SerializeField] TMP_Text winTimer;
    [HideInInspector] public float timeValue = 0f;

    #endregion


    #region Unity Lifecycle

    private void Update()
    {
        TimeDisplay();
        Win();
        Score();
        _aleCountText.text = _aleCount.Value.ToString("D3");
    }

    #endregion

    #region Private Methods
    /// <summary>Displays timer on UI</summary>
    private void TimeDisplay()
    {
        if (!WinTrigger.instance.hasFinished)
            timeValue += Time.deltaTime;
        else
        {
            timerText.gameObject.SetActive(false);
        }
        float minutes = Mathf.FloorToInt(timeValue / 60);
        float seconds = Mathf.FloorToInt(timeValue % 60);
        int hundredths = Mathf.FloorToInt((timeValue * 100f) % 100f);

        timerText.text = string.Format("{0}:{1:00}:{2:00}", minutes, seconds, hundredths);
    }
    /// <summary>Displays score</summary>
    private void Score()
    {
        _scoreText.text = "Score: " + score.Value;
    }
    /// <summary>Displays updated ale count and timer count in win canvas</summary>
    private void Win()
    {
        float minutes = Mathf.FloorToInt(timeValue / 60);
        int seconds = Mathf.FloorToInt(timeValue % 60);
        int hundredths = Mathf.FloorToInt((timeValue * 100f) % 100f);
        if (WinTrigger.instance.hasFinished)
        {
            winTimer.text = string.Format("{0}:{1:00}:{2:00}", minutes, seconds, hundredths);
            _aleCountWin.text = _aleCount.Value.ToString("D3");
            aleUi.SetActive(false);
            _scoreText.gameObject.SetActive(false);
            if (updatedScore == false)
            {
                score.Value -= (int)Mathf.Round(timeValue);
                updatedScore = true;
            }
            _scoreWinText.text = "Score: " + score.Value;
        }
    }
    #endregion

    private bool updatedScore = false;

}
