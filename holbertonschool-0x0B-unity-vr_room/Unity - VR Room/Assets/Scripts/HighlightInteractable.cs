using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    private Material originalMaterial;
    private Renderer objectRenderer;

    [SerializeField] private Color emissiveColor = Color.white; // Adjust the emissive color as needed

    private void Start()
    {
        // Get the renderer component to change the material
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            // Store the original material of the object
            originalMaterial = objectRenderer.material;
        }
    }

    public void Highlight()
    {
        if (objectRenderer != null && originalMaterial != null)
        {
            // Create a new material to combine the original material and emissive effect
            Material combinedMaterial = new Material(originalMaterial);

            // Modify the new material to add emissive effect
            combinedMaterial.SetColor("_EmissionColor", emissiveColor);
            combinedMaterial.EnableKeyword("_EMISSION");

            // Apply the combined material to the object
            objectRenderer.material = combinedMaterial;
        }
    }

    public void ResetHighlight()
    {
        if (objectRenderer != null)
        {
            // Reset the material back to the original material
            objectRenderer.material = originalMaterial;
        }
    }
}
