using UnityEngine;

public class SingleCollision : MonoBehaviour
{
    // Record the position at the time of the first collision
    private Vector3 prevPos;
    // magnitude of Vector3
    public float TotalDistance { get; set; }
    public bool IsFirstEnter { get; set; } = false;
    
    
    /// <summary>
    /// Only record the first collision position
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (IsFirstEnter != false || other.gameObject.CompareTag("Box")) return;
        IsFirstEnter = true;
        prevPos = transform.position;
    }
    
    /// <summary>
    /// I choose distance "a" for calculate and use ”=“ instead of ”+=“
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Box")) return;
        var currPos = transform.position;
        var delta = currPos - prevPos;
        TotalDistance = delta.magnitude;
    }

    /// <summary>
    /// When each sphere collider exit the "Box",its distance turns 0
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            TotalDistance = 0;
        }
    }
    
}