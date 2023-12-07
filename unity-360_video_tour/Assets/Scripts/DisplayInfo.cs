using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DisplayInfo : MonoBehaviour
{
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject textInfo;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    public void PointerEnter()
    {
        _anim.SetBool("Hovering", true);
        _anim.SetBool("HasHovered", true);
    }

    public void PointerExit()
    {
        _anim.SetBool("Hovering", false);
        if (background.activeSelf && textInfo.activeSelf)
        {
            background.SetActive(false);
            textInfo.SetActive(false);
        }
    }

    public void DisplayText()
    {
        if (textDisplaying)
        {
            background.SetActive(false);
            textInfo.SetActive(false);
            textDisplaying = false;
        }
        else
        {
            background.SetActive(true);
            textInfo.SetActive(true);
            textDisplaying = true;
        }
    }

    private Animator _anim;
    private bool textDisplaying;
}
