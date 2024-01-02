using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPush : MonoBehaviour
{
    [SerializeField] private GameObject _particles;

    private void Awake()
    {
        particlesShowing = false;
    }

    public void ToggleParticles()
    {
        if (!particlesShowing)
        {
            _particles.SetActive(true);
            particlesShowing = true;
        }
        else
        {
            _particles.SetActive(false);
            particlesShowing = false;
        }
    }

    private bool particlesShowing;
}
