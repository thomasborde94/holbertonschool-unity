using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    #region Show in Inspector

    [SerializeField] private GameObject _mainCamera;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private GameObject _timerCanvas;
    [SerializeField] private GameObject _cutsceneCamera;

    #endregion
    #region Public Methods

    public void CutsceneEnd()
    {
        _mainCamera.SetActive(true);
        _playerController.enabled = true;
        _timerCanvas.SetActive(true);
        _cutsceneCamera.SetActive(false);
    }

    #endregion
}
