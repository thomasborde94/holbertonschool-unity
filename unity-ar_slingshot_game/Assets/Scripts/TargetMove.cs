using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TargetMove : MonoBehaviour
{
    [SerializeField] private float speed = 0.01f;

    void Update()
    {
        if (!startMoving)
            return;
        if (hasDestination)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed);
            if (Vector3.Distance(transform.position, destination) < 0.01f)
            {
                hasDestination = false;
            }
        }
        else
            hasDestination = RandomPoint(planeCenter, rayYoffset, range, out destination);
    }

    public void StartMoving(ARPlane plane)
    {
        movePlane = plane;
        planeCenter = plane.center;
        range = Mathf.Max(plane.size.x, plane.size.y);
        //rayYoffset = 0.5f;
        //colliderHeight = transform.localScale.y * GetComponent<CapsuleCollider>().height;
        //transform.position = planeCenter + Vector3.up * colliderHeight / 2;
        hasDestination = RandomPoint(planeCenter, rayYoffset, range, out destination);
        startMoving = true;
    }

    public bool RandomPoint(Vector3 center, float rayYoffset, float range, out Vector3 result)
    {
        Vector3 next = center + Random.insideUnitSphere * range;
        RaycastHit hit;
        if (Physics.Raycast(next, Vector3.down, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject == movePlane.gameObject)
            {
                result = hit.point + Vector3.up * colliderHeight / 2;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

    private float rayYoffset;
    private Vector3 planeCenter;
    private float range;
    private bool hasDestination;
    private Vector3 destination;
    public bool startMoving = false;
    private ARPlane movePlane;
    private float colliderHeight;
}