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
        Material mat = new Material(Shader.Find("Sprites/Default"));
        mat.shader = Shader.Find("Unlit/Color");
        mat.color = Color.red;
        lineRenderer.material = mat;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.positionCount = 2;
    }

    private void Update()
    {
        startPosition = DynamicCommon.OriginVector;
        direction = DynamicCommon.PenaltyForce;
        lineLength = DynamicCommon.PenaltyForce.magnitude/250;
        Vector3 endPosition = startPosition + (direction.normalized * lineLength);

        lineRenderer.SetPosition(0, startPosition);
        lineRenderer.SetPosition(1, endPosition);
    }
}
