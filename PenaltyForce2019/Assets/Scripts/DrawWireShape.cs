using System;
using UnityEngine;
using UnityEngine.Serialization;

public enum Shape
{
    Sphere,
    Box,
    Mesh
}
public class DrawWireShape : MonoBehaviour
{
    public Color drawColor;
    [Header("Choose Shape")]
    public Shape wireShape;
    [Header("Sphere")]
    public float sphereRadius = 0.25f;
    [Header("Box")]
    public Vector3 boxSize;
    [Header("Mesh")]
    public Mesh meshMesh;

    public float cylinderRadius;
    public float cylinderHeight;
    private void OnDrawGizmos()
    {
        Gizmos.color = drawColor;

        switch (wireShape)
        {
            case Shape.Sphere:
                Gizmos.DrawWireSphere(transform.position, sphereRadius);
                break;
            case Shape.Box:
                Gizmos.DrawWireCube(transform.position,boxSize);
                break;
            case Shape.Mesh:
                if (meshMesh != null)
                {
                    Gizmos.DrawWireMesh(meshMesh,0,transform.position,transform.rotation,new Vector3(cylinderRadius,cylinderHeight,cylinderRadius));
                }
                else
                {
                    Gizmos.DrawWireSphere(Vector3.zero, cylinderRadius);
                    Gizmos.DrawLine(new Vector3(0, cylinderHeight / 2f, 0), new Vector3(0, -cylinderHeight / 2f, 0));
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}


