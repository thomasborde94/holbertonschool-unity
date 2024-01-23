using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoTrajectory : MonoBehaviour
{
    public static AmmoTrajectory instance;
    private LineRenderer lineRenderer;
    [SerializeField] private int pointsCount = 10;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        instance = this;
    }
    private void Start()
    {
        lineRenderer.positionCount = pointsCount;
    }

    public void UpdateTrajectory(Vector3 initialPosition, Vector3 initialVelocity, float timeStep)
    {
        // Calcule les positions le long de la trajectoire et les assigne au LineRenderer
        for (int i = 0; i < pointsCount; i++)
        {
            float time = i * timeStep;
            Vector3 position = CalculateTrajectoryPoint(initialPosition, initialVelocity, time);
            lineRenderer.SetPosition(i, position);
        }
    }

    private Vector3 CalculateTrajectoryPoint(Vector3 initialPosition, Vector3 initialVelocity, float time)
    {
        Vector3 gravity = Physics.gravity;
        Vector3 position = initialPosition + initialVelocity * time + 0.5f * gravity * time * time;
        return position;
    }
}
