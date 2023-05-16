using UnityEngine;

public class Collision : MonoBehaviour
{
    private void OnCollisionEnter(UnityEngine.Collision other)
    {
        Debug.Log("Collision detected between " + gameObject.name + " and " + other.collider.name);
    }
}
