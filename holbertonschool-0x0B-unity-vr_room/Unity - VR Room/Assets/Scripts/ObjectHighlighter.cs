using UnityEngine;

public class ObjectHighlighter : MonoBehaviour
{
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private Transform controllerTransform;
    [SerializeField] private float maxDistance = 20f;

    private void Update()
    {
        RaycastForInteractableObjects();
    }

    private void RaycastForInteractableObjects()
    {
        RaycastHit hit;
        InteractableObject newInteractableObject = null;

        if (Physics.Raycast(controllerTransform.position, controllerTransform.forward, out hit, maxDistance, interactableLayer) && hit.distance > 0.2f)
        {
            newInteractableObject = hit.collider.GetComponent<InteractableObject>();

            // Highlight the new interactable object
            if (newInteractableObject != null)
            {
                newInteractableObject.Highlight();
            }
        }

        // Reset the highlight if no valid interactable object is hit
        if (interactableObject != null && interactableObject != newInteractableObject)
        {
            interactableObject.ResetHighlight();
        }

        // Update the interactableObject reference
        interactableObject = newInteractableObject;
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(controllerTransform.position, controllerTransform.forward * maxDistance, Color.blue);
    }

    private InteractableObject interactableObject;
}
