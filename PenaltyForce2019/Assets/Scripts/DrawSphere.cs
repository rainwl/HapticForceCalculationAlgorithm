using UnityEngine;
using UnityEngine.Serialization;

public class DrawSphere : MonoBehaviour
{
    public Color drawColor;
    public float radius = 0.25f;
    
    private void OnDrawGizmos()
    {
        Gizmos.color = drawColor;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}