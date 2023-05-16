using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderManager : MonoBehaviour
{
    // private Vector3 initialPosition;
    // private float collisionSum = 0f;
    // private static Dictionary<SphereCollider, ColliderManager> managers = new Dictionary<SphereCollider, ColliderManager>();

    private Vector3 prevPos;
    public float totalDistance;
    // private void Awake()
    // {
    //     SphereCollider sphereCollider = GetComponent<SphereCollider>();
    //     
    //     if (sphereCollider != null)
    //     {
    //         managers[sphereCollider] = this;
    //     }
    // }
    void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.CompareTag("MyTag"))
        //{
            //collisionSum = 0f;
            prevPos = transform.position;
        //}
    }
    void OnTriggerStay(Collider other)
    {
        //if (other.gameObject.CompareTag("MyTag"))
        //{
            Vector3 currPos = transform.position;
            Vector3 delta = currPos - prevPos;
            totalDistance += delta.magnitude;
            //prevPos = currPos;
            //collisionSum += Vector3.Distance(initialPosition, transform.position);
        //}
    }
    private float exitTime;
    void OnTriggerExit(Collider other)
    {
        exitTime = Time.time;
        StartCoroutine(ExecuteAfterDelay());
    }

    IEnumerator ExecuteAfterDelay()
    {
        // float waitTime = 1f;
        // yield return new WaitForSeconds(waitTime);
        //
        // // 确认等待时间已经超过1秒
        // float elapsedTime = 0f;
        // while (elapsedTime < waitTime)
        // {
        //     elapsedTime += Time.deltaTime;
        //     yield return null;
        // }
        // totalDistance = 0;
        
        
        // 等待1秒钟
        while (Time.time - exitTime < 1f)
        {
            yield return null;
        }

        totalDistance = 0;
    }
    
    // public float GetCollisionSum()
    // {
    //     return collisionSum;
    // }
    //
    // public static float GetTotalCollisionSum()
    // {
    //     float totalCollisionSum = 0f;
    //     foreach (KeyValuePair<SphereCollider, ColliderManager> pair in managers)
    //     {
    //         totalCollisionSum += pair.Value.GetCollisionSum();
    //     }
    //     return totalCollisionSum;
    // }
    //
}
