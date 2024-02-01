using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeParts : MonoBehaviour
{
    [HideInInspector] public bool _shouldFade = false;
    [SerializeField] private Material _fadeCristalMat;
    [SerializeField] private float _fadeDuration = 2f;

    private void Update()
    {
        if (_shouldFade)
        {
            StartCoroutine(FadingParts());
        }
    }
    private IEnumerator FadingParts()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material = _fadeCristalMat;
        Color startColor = renderer.material.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 0.0f);

        float elapsedTime = 0.0f;
        while (elapsedTime < _fadeDuration)
        {
            renderer.material.color = Color.Lerp(startColor, targetColor, elapsedTime / _fadeDuration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // Set final color and destroy the explosion part
        renderer.material.color = targetColor;
        Destroy(gameObject);
    }
}
