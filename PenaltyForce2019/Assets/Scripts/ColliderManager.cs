using UnityEngine;
using UnityEngine.Serialization;

public class ColliderManager : MonoBehaviour
{
    private Vector3 prevPos;
    public float TotalDistance { get; set; }
    public bool IsFirstEnter { get; set; } = false;

    private void OnTriggerEnter(Collider other)
    {
        if (IsFirstEnter == false && !other.gameObject.CompareTag("Box"))
        {
            IsFirstEnter = true;
            prevPos = transform.position;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        // I choose distance "a" 
        var currPos = transform.position;
        var delta = currPos - prevPos;
        // if there need "+=" ? use "=" instead ?
        //TotalDistance += delta.magnitude;
        TotalDistance = delta.magnitude;
        
        
        //distance "b"
        //code later
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            TotalDistance = 0;
        }
    }
    
}