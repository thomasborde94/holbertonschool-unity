using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Teleporting : MonoBehaviour
{
    [SerializeField] float _fadeSpeed;
    [SerializeField] Image[] fadeScreens;

    public void TeleportFade()
    {
        StartCoroutine(StartTeleportWithFade());
    }

    private IEnumerator StartTeleportWithFade()
    {
        foreach (var image in fadeScreens)
        {
            image.gameObject.SetActive(true);
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
        }

        float timer = 1f;


        while (timer > 0f) // Transition inverse
        {
            timer -= Time.deltaTime * _fadeSpeed;

            foreach (var image in fadeScreens)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, timer);
            }

            yield return null;
        }



        foreach (var image in fadeScreens)
        {
            image.gameObject.SetActive(false); // Désactiver les fadescreens à la fin de la transition
        }
    }
}
