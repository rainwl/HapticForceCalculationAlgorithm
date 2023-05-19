using System.Collections;
using System.Collections.Generic;
using DynamicCollision;
using UnityEngine;

public class DrawForceLine : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 direction;
    public float lineLength;

    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = 2;
    }

    private void Update()
    {
        startPosition = DynamicCommon.OriginVector;
        direction = DynamicCommon.PenaltyForce;
        lineLength = DynamicCommon.PenaltyForce.magnitude/500;
        Vector3 endPosition = startPosition + (direction.normalized * lineLength);

        lineRenderer.SetPosition(0, startPosition);
        lineRenderer.SetPosition(1, endPosition);
    }
}
