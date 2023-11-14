using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public static OptionsMenu instance;
    [HideInInspector] public string toggleId = "yInvertedId";
    [HideInInspector] public string bgmVolumeId = "bgmVolumeId";
    [HideInInspector] public string sfxVolumeId = "sfxVolumeId";
    [SerializeField] public Toggle yInverted;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private AudioMixer audioMixer;


    private void Awake()
    {
        instance = this;
        Time.timeScale = 1f;
    }
    private void Start()
    {
        if (SceneManager.GetActiveScene().name != "Options")
        {
            PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
            PlayerPrefs.Save();
        }
        lastSceneName = PlayerPrefs.GetString("LastScene");

        if (SceneManager.GetActiveScene().name == "Options")
        {
            LoadToggleState();
            float linearBGMVolume = DecibelToLinear(PlayerPrefs.GetFloat(bgmVolumeId, 0));
            bgmSlider.value = linearBGMVolume;

            float linearSFXVolume = DecibelToLinear(PlayerPrefs.GetFloat(sfxVolumeId, 0));
            sfxSlider.value = linearSFXVolume;
        }
    }

    private void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "Options")
        {
            audioMixer.SetFloat("BGMVolume", LinearToDecibel(bgmSlider.value));
            audioMixer.SetFloat("SFXVolume", LinearToDecibel(sfxSlider.value));
        }
    }
    #region Public methods

    public void Back()
    {
        SceneManager.LoadScene(lastSceneName);
    }

    public void Apply()
    {
        toggleState = yInverted.isOn ? 1 : 0;

        PlayerPrefs.SetInt(toggleId, toggleState);
        PlayerPrefs.SetFloat(bgmVolumeId, LinearToDecibel(bgmSlider.value));
        PlayerPrefs.SetFloat(sfxVolumeId, LinearToDecibel(sfxSlider.value));
        PlayerPrefs.Save();
        Back();
    }
    #endregion

    #region Private Methods

    private void LoadToggleState()
    {
        int toggleState = PlayerPrefs.GetInt(toggleId, 0);
        if (toggleState == 1)
            yInverted.isOn = true;
        else
            yInverted.isOn = false;
    }

    public float LinearToDecibel(float linear)
    {
        float dB;

        if (linear != 0)
            dB = 20.0f * Mathf.Log10(linear);
        else
            dB = -144.0f;

        return dB;
    }

    private float DecibelToLinear(float dB)
    {
        float linear = Mathf.Pow(10.0f, dB / 20.0f);

        return linear;
    }
    #endregion
    #region Private
    private string lastSceneName = "MainMenu";
    private int toggleState;
    #endregion
}
