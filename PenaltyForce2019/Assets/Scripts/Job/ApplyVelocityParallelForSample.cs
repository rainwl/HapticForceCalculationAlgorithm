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
        // Jobs declare all data that will be accessed int the job 
        // By declaring it as read only ,multiple jobs are allowed to access the data in parallel
        [ReadOnly] public NativeArray<Vector3> velocity;

        // By default containers are assumed to be read & write
        public NativeArray<Vector3> position;

        // Delta time must be copied to the job since jobs generally dont have concept of frame
        // The main thread waits for the job same frame or next frame,but the job should do work deterministically
        // independent on when the job happens to run on the worker threads
        public float deltaTime;

        // The code actually running on the job
        public void Execute(int i)
        {
            position[i] = position[i] + velocity[i] * deltaTime;
        }
    }

    public void Update()
    {
        // Persistent 持续分配
        var position = new NativeArray<Vector3>(500, Allocator.Persistent);
        var velocity = new NativeArray<Vector3>(500, Allocator.Persistent);
        for (var i = 0; i < velocity.Length; i++)
        {
            velocity[i] = new Vector3(0, 10 + i, 0);

            //initialize the job data
            var job = new VelocityJob()
            {
                deltaTime = Time.deltaTime,
                position = position,
                velocity = velocity
            };
            
            // Schedule a parallel-for job.First parameter is how many for-each
            // iterations to perform
            // The second parameter is the batch size,
            // essentially the no-overhead innerloop that just invokes Execute(i) in a loop
            // When there is a lot of work in each iteration then a value of 1 can be sensible
            // When there is very little work values of 32 or 64 can make sense
            var jobHandle = job.Schedule(position.Length, 64);
            
            
            // Ensure the job has completed
            // it is not recommended to complete a job immediately
            // since that reduces the change of having other jobs run in parallel with this one
            // You optimally want to schedule a job early in a frame and then wait for it later in the frame.
            jobHandle.Complete();

            Debug.Log(job.position[0]);

            
            // Native arrays must be disposed manually
            position.Dispose();
            velocity.Dispose();
        }
    }
}