using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinTrigger : MonoBehaviour
{
    public static WinTrigger instance;

    [HideInInspector] public bool hasFinished = false;
    [SerializeField] private GameObject winCanvas;
    [SerializeField] GameObject timerUi;

    private void Awake()
    {
        instance = this;
    }

    /// <summary>Displays win canvas at the end of the level</summary>
    /// <param name="other">player collider</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            hasFinished = true;
            winCanvas.SetActive(true);
            timerUi.SetActive(false);
            Time.timeScale = 0f;
        }
    }
}
