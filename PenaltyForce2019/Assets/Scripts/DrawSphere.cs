using UnityEngine;
using UnityEngine.Serialization;

public class DrawSphere : MonoBehaviour
{
    private Color drawColor;
    public float radius = 0.25f;

    private void OnValidate()
    {
        //_drawColor = UnityEngine.Random.ColorHSV();
        drawColor = new Color(0, 1, 0, 0.1f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = drawColor;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}