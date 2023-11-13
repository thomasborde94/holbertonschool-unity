using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    #region Show in Inspector
    public static AudioManager instance;
    [SerializeField] public AudioSource clipAudio;
    [SerializeField] AudioClip buttonRollover;
    [SerializeField] AudioClip buttonClick;
    [SerializeField] AudioClip stoneFootstep;
    [SerializeField] AudioClip grassFootstep;
    [SerializeField] AudioClip stoneLanding;
    [SerializeField] AudioClip grassLanding;

    [SerializeField] AudioClip wallPaper;
    [SerializeField] AudioClip cherryMonday;

    public int clipState = 1;
    public int musicState = 0;
    [HideInInspector] public bool canPlayClip;
    [HideInInspector] public bool canPlayLoop;
    #endregion

    private void Awake()
    {
        instance = this;
        canPlayClip = false;
        canPlayLoop = true;
        loopAudio = GetComponent<AudioSource>();
    }

    private void Update()
    {
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
            Debug.Log(musicState);
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
        }
    }

    private void ChooseLoop()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (WinTrigger.instance.hasFinished)
        {
            musicState = 0;
        }
        if (sceneName == "Level01" && !WinTrigger.instance.hasFinished)
        {
            musicState = 2;
        }
        if (sceneName == "MainMenu")
        {
            musicState = 1;
        }
    }

    private AudioSource loopAudio;
}
