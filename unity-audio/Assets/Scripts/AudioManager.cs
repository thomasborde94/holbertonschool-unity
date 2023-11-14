using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    #region Show in Inspector
    public static AudioManager instance;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] public AudioSource clipAudio;
    [SerializeField] AudioClip buttonRollover;
    [SerializeField] AudioClip buttonClick;
    [SerializeField] AudioClip stoneFootstep;
    [SerializeField] AudioClip grassFootstep;
    [SerializeField] AudioClip stoneLanding;
    [SerializeField] AudioClip grassLanding;

    [SerializeField] AudioClip wallPaper;
    [SerializeField] AudioClip cherryMonday;
    [SerializeField] AudioClip victoryPiano;

    public int clipState = 1;
    public int musicState = 0;
    [HideInInspector] public bool canPlayClip;
    [HideInInspector] public bool canPlayLoop;
    [HideInInspector] public string bgmVolumeId = "bgmVolumeId";
    [HideInInspector] public string sfxVolumeId = "sfxVolumeId";
    #endregion

    private void Awake()
    {
        instance = this;
        canPlayClip = false;
        canPlayLoop = true;
        loopAudio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        float bgmVolume = PlayerPrefs.GetFloat(bgmVolumeId, 0);
        audioMixer.SetFloat("BGMVolume", bgmVolume);

        float sfxVolume = PlayerPrefs.GetFloat(sfxVolumeId, 0);
        audioMixer.SetFloat("SFXVolume", sfxVolume);
    }
    private void Update()
    {
        Debug.Log(PlayerPrefs.GetFloat(bgmVolumeId, 0));
        ChooseLoop();
        if (canPlayClip)
        {
            canPlayClip = false;
            if (clipState == 1)
            {
                clipAudio.clip = buttonRollover;
                clipAudio.Play();
                clipAudio.loop = false;
            }
            if (clipState == 2)
            {
                clipAudio.clip = buttonClick;
                clipAudio.Play();
                clipAudio.loop = false;
            }
            if (clipState == 3)
            {
                clipAudio.clip = stoneFootstep;
                clipAudio.Play();
                clipAudio.loop = false;
            }
            if (clipState == 4)
            {
                clipAudio.clip = grassFootstep;
                clipAudio.Play();
                clipAudio.loop = false;
            }
            if (clipState == 5)
            {
                clipAudio.clip = stoneLanding;
                clipAudio.Play();
                clipAudio.loop = false;
            }
            if (clipState == 6)
            {
                clipAudio.clip = grassLanding;
                clipAudio.Play();
                clipAudio.loop = false;
            }
        }

        if (canPlayLoop)
        {
            canPlayLoop = false;
            if (musicState == 0)
            {
                
                loopAudio.Stop();
            }
            if (musicState == 1)
            {
                loopAudio.clip = wallPaper;
                loopAudio.Play();
                loopAudio.loop = true;
            }
            if (musicState == 2)
            {
                loopAudio.clip = cherryMonday;
                loopAudio.Play();
                loopAudio.loop = true;
            }
            if (musicState == 3)
            {
                loopAudio.clip = victoryPiano;
                loopAudio.Play();
                loopAudio.loop = false;
            }
        }
    }

    private void ChooseLoop()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        
        if (sceneName == "Level01" && !WinTrigger.instance.hasFinished)
        {
            musicState = 2;
        }
        if (sceneName == "MainMenu")
        {
            musicState = 1;
        }
        if (isInLevel())
            if (WinTrigger.instance.hasFinished)
            {
                musicState = 3;
            }
    }
    private bool isInLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "Level01" || sceneName == "Level02" || sceneName == "Level03")
            return true;
        else return false;
    }

    private AudioSource loopAudio;
}
