using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderManager : MonoBehaviour
{
    private Vector3 prevPos;
    public float totalDistance;

    private bool isFirstEnter = false;
    private void OnTriggerEnter(Collider other)
    {
        if (isFirstEnter == false)
        {
            isFirstEnter = true;
            prevPos = transform.position;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        var currPos = transform.position;
        var delta = currPos - prevPos;
        totalDistance += delta.magnitude;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            totalDistance = 0;
            Debug.Log("exit");
        }
    }
    
}