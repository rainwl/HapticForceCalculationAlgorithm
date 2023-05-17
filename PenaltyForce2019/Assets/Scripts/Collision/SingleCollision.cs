using UnityEngine;
public class SingleCollision : MonoBehaviour
{
    #region Fields
    /// <summary>
    /// Record the position at the time of the first collision
    /// </summary>
    private Vector3 previousPos;

    /// <summary>
    /// magnitude of Vector3
    /// </summary>
    public float Distance { get; private set; } = 0;

    /// <summary>
    /// each sphere collider 's force
    /// </summary>
    public Vector3 SingleForce { get; set; } = Vector3.zero;
    
    /// <summary>
    /// each collider could enter once,and record the pos there
    /// </summary>
    public bool IsFirstEnter { get; set; } = false;
    
    #endregion

    #region Trigger Methods
    
    /// <summary>
    /// Only record the first collision position
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (IsFirstEnter != false) return;
        IsFirstEnter = true;
        previousPos = transform.position;
    }
    
    /// <summary>
    /// I choose distance "a" for calculate and use ”=“ instead of ”+=“
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        var currPos = transform.position;
        var delta = currPos - previousPos;
        SingleForce = delta;
        Distance = delta.magnitude;
    }

    /// <summary>
    /// When each sphere collider exit the "Box",its distance turns 0
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            Distance = 0;
        }
    }

    #endregion
    
}