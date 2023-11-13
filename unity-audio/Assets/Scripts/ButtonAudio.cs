using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAudio : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.instance.clipState = 1;
        AudioManager.instance.canPlayClip = true;
    }

    public void OnClick()
    {
        AudioManager.instance.clipState = 2;
        AudioManager.instance.canPlayClip = true;
    }
}
