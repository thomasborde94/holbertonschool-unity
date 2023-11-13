using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    #region Show in Inspector

    [SerializeField] public TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI winText;

    [HideInInspector] public float timeValue = 0f;

    #endregion

    void Update()
    {
        TimeDisplay();
        Win();
    }

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

    public void Win()
    {
        float minutes = Mathf.FloorToInt(timeValue / 60);
        float seconds = Mathf.FloorToInt(timeValue % 60);
        int hundredths = Mathf.FloorToInt((timeValue * 100f) % 100f);
        if (WinTrigger.instance.hasFinished)
            winText.text = string.Format("{0}:{1:00}:{2:00}", minutes, seconds, hundredths);
    }


}
