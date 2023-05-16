using UnityEngine;
using UnityEngine.Serialization;

public class DrawSphere : MonoBehaviour
{
    private Color _drawColor;
    public float radius = 0.25f;
    private void OnValidate()
    {
        //_drawColor = UnityEngine.Random.ColorHSV();
        _drawColor = new Color(0, 1, 0, 0.1f);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _drawColor;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
