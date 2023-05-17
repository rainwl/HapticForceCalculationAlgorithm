using System;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    private Dictionary<GameObject, Vector3> collisionPositions = new Dictionary<GameObject, Vector3>();


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MyTag"))
        {
            Vector3 diff = other.transform.position - transform.position;
            collisionPositions[other.gameObject] = other.transform.position;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("MyTag"))
        {
            Vector3 currentPos = transform.position;
            Vector3 lastPos = currentPos - GetComponent<Rigidbody>().velocity * Time.deltaTime;
            Vector3 totalDiff = Vector3.zero;
            foreach (var collision in collisionPositions)
            {
                Vector3 initialPos = collision.Value;
                Vector3 latestPos = collision.Key.transform.position;
                totalDiff += latestPos - initialPos;
            }

            Vector3 diff = other.transform.position - currentPos;
            totalDiff += diff;
            collisionPositions[other.gameObject] = other.transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MyTag"))
        {
           // collisionPositions.Remove(other.transform.position - transform.position);
        }

    }
}
