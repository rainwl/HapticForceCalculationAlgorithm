using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class ApplyVelocityParallelForSample : MonoBehaviour
{
    struct VelocityJob : IJobParallelFor
    {
        [ReadOnly] public NativeArray<Vector3> velocity;
        public NativeArray<Vector3> position;
        public float deltaTime;
        

        public void Execute(int index)
        {
            position[index] = position[index] + velocity[index] * deltaTime;
        }
    }

    public void Update()
    {
        var position = new NativeArray<Vector3>(500, Allocator.Persistent);
        var velocity = new NativeArray<Vector3>(500, Allocator.Persistent);
        for (int i = 0; i < velocity.Length; i++)
        {
            velocity[i] = new Vector3(0, 10, 0);
            
            //initialize the job data
            var job = new VelocityJob()
            {
                deltaTime = Time.deltaTime,
                position = position,
                velocity = velocity
            };
            JobHandle jobHandle = job.Schedule(position.Length, 64);
            jobHandle.Complete();
            
            Debug.Log(job.position[0]);

            position.Dispose();
            velocity.Dispose();
            


        }
    }
}
























