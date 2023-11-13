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

    [SerializeField] AudioClip wallPaper;

    public int clipState = 1;
    public int musicState = 1;
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
        }

        if (canPlayLoop)
        {
            canPlayLoop = false;
            if (musicState == 1)
            {
                loopAudio.clip = wallPaper;
                loopAudio.Play();
                loopAudio.loop = true;
            }
        }
    }

    private void ChooseLoop()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "MainMenu")
        {
            musicState = 1;
        }
    }

    private AudioSource loopAudio;
}
