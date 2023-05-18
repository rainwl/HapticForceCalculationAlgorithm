using System;
using System.Collections;
using System.Collections.Generic;
using DynamicCollision;
using UnityEngine;

public class Test : MonoBehaviour
{
    private DynamicCollider[] collidersArray1;
    private DynamicCollider[] collidersArray2;
    public GameObject obj1;
    public GameObject obj2;
    private void Start()
    {
        collidersArray1 = obj1.GetComponentsInChildren<DynamicCollider>();
        collidersArray2 = obj2.GetComponentsInChildren<DynamicCollider>();
        Vector3 totalDir = Vector3.zero;
        for (int i = 0; i < collidersArray1.Length; i++)
        {
            for (int j = 0; j < collidersArray2.Length; j++)
            {
                var dis = collidersArray2[i].transform.position - collidersArray1[i].transform.position;
                totalDir += dis;
                
            }
        }
        Debug.Log($"totalDir:{totalDir}");
        Debug.Log($"totalDir.magnitude: {totalDir.magnitude}");
        var origin = new Vector3(0.5f, -0.5f, 0.3f);
        var dot = Vector3.Dot(origin, totalDir);
        Debug.Log($"dot: {dot}");
        //Debug.Log(DynamicCommon.Direction());
    }
}
