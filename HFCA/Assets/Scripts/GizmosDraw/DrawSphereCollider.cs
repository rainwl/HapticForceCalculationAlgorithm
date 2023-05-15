using UnityEngine;

public class DrawSphereCollider : MonoBehaviour
{
    private Color _drawColor;

    private void OnValidate()
    {
        _drawColor = UnityEngine.Random.ColorHSV();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _drawColor;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}