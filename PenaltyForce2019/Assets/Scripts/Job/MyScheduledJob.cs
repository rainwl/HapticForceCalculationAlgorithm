using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class MyScheduledJob : MonoBehaviour
{
    private NativeArray<float> result;
    private JobHandle handle;
    public struct MyJob : IJob
    {
        public float a;
        public float b;
        public NativeArray<float> result;
        public void Execute()
        {
            result[0] = a + b;
        }
    }

    private void Update()
    {
        result = new NativeArray<float>(1, Allocator.TempJob);
        MyJob jobData = new MyJob()
        {
            a = 10,
            b = 10,
            result = result
        };
        handle = jobData.Schedule();
        
        
    }
    

    private void LateUpdate()
    {
        handle.Complete();
        float aPlusB = result[0];
        Debug.Log(aPlusB);
        result.Dispose();
    }
}

