using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AmmoEvolutions : MonoBehaviour
{
    [SerializeField] private ParticleSystem _innerParticles;
    [SerializeField] private ParticleSystem _outerParticles;
    [SerializeField] private ParticleSystem _smokeParticles;
    [SerializeField] private GameObject _sphere;

    private void Awake()
    {
        _innerColorOverLifetime = _innerParticles.colorOverLifetime;
        _outerMainModule = _outerParticles.main;
    }
    private void Update()
    {
        if (SpawnAmmo.instance.isAiming)
        {
            InnerParticlesAlpha(SpawnAmmo.instance.DragValue);
            OuterParticlesSize();
            SmokeSize();
        }
        
    }

    private void SmokeSize()
    {
        // Sphere radius
        float minRadius = 0.005f;
        float maxRadius = 0.026f;
        float newRadius = Mathf.Lerp(minRadius, maxRadius, SpawnAmmo.instance.DragValue);
        ParticleSystem.ShapeModule shapeModule = _smokeParticles.shape;
        shapeModule.radius = newRadius;

        // Smoke alpha
        float minSmokeAlpha = 0.07f;
        float maxSmokeAlpha = 0.15f;
        float newAlpha = Mathf.Lerp(minSmokeAlpha, maxSmokeAlpha, SpawnAmmo.instance.DragValue);
        Color color = new Color(0.81f, 1f, 0.59f, newAlpha);

        ParticleSystem.MainModule smokeMainModule = _smokeParticles.main;
        smokeMainModule.startColor = color;
    }

    private void OuterParticlesSize()
    {
        float minSize = 0.07f;
        float maxSize = 0.1f;
        float newSize = Mathf.Lerp(minSize, maxSize, SpawnAmmo.instance.DragValue);

        // Affectez la nouvelle taille à la propriété startSize du module principal
        _outerMainModule.startSize = new ParticleSystem.MinMaxCurve(newSize);

        float minScale = 0.8f;
        float maxScale = 1.5f;
        float newScale = Mathf.Lerp(minScale, maxScale, SpawnAmmo.instance.DragValue);
        _sphere.transform.localScale = new Vector3(newScale, newScale, newScale);
    }
    private void InnerParticlesAlpha(float alphaValue)
    {
        Gradient gradient = new Gradient();
        GradientColorKey[] colorKeys = new GradientColorKey[]
        {
            new GradientColorKey(new Color(0.98f, 0.93f, 0.25f), 0.0f),
            new GradientColorKey(new Color(0.98f, 0.93f, 0.25f), 1.0f)
        };

        // Modify the alpha value of the middle key based on DragValue.
        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[]
        {
            new GradientAlphaKey(0.0f, 0.0f),
            new GradientAlphaKey(alphaValue, 0.5f),  // Modify the alpha value at the middle of the gradient
            new GradientAlphaKey(0.0f, 1.0f)
        };

        gradient.SetKeys(colorKeys, alphaKeys);
        _innerColorOverLifetime.color = new ParticleSystem.MinMaxGradient(gradient);
    }


    private ParticleSystem.ColorOverLifetimeModule _innerColorOverLifetime;
    private ParticleSystem.MainModule _outerMainModule;
}
