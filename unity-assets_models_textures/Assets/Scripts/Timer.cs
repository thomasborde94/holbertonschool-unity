using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    #region Show in Inspector

    [SerializeField] public Text timerText;

    [HideInInspector] public float timeValue = 0f;

    #endregion

    void Update()
    {
        TimeDisplay();
    }

    private void TimeDisplay()
    {
        if (!WinTrigger.instance.hasFinished)
            timeValue += Time.deltaTime;
        else
        {
            timerText.color = Color.green;
            timerText.fontSize = 60;
        }
        float minutes = Mathf.FloorToInt(timeValue / 60);
        float seconds = Mathf.FloorToInt(timeValue % 60);
        int hundredths = Mathf.FloorToInt((timeValue * 100f) % 100f);

        timerText.text = string.Format("{0}:{1:00}:{2:00}", minutes, seconds, hundredths);
    }

    
}
