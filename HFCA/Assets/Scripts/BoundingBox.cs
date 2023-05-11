using UnityEngine;

public class BoundingBox : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        var mesh = GetComponent<MeshFilter>().sharedMesh;
        var bounds = new Bounds(mesh.bounds.center, mesh.bounds.size);
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(bounds.center, bounds.size);
    }
}
