using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using SDF;
using SDF.Hierarchy;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

public class MyNativeSDF : MonoBehaviour
{
    [Range(4, 64)] public int Resolution = 16;
    public float GridSize = 0.1f;
    public int Chunks = 2;
    public bool UseVoxelStyle;
    public Material previewMat;
    private float _resultDist;
    //private Mesh _mesh;

    private void OnValidate()
    {
        Resolution = (Resolution / 4) * 4;
        //_mesh = new Mesh();
    }

    private void Start()
    {
        var sdf = GetComponent<SDFBehaviour>().GetNode().Compile();
        var pairArrays = new NativeArray<KeyValuePair<int3, float3>>[Chunks * Chunks];
        for (var i = 0; i < pairArrays.Length; i++) 
        {
            pairArrays[i] = new NativeArray<KeyValuePair<int3, float3>>(Resolution * Resolution * Resolution, Allocator.TempJob);
        }
        var totalVerts = 0;
        var handles = new NativeArray<JobHandle>(Chunks * Chunks, Allocator.TempJob);
        var counts = new NativeArray<int>(Chunks * Chunks, Allocator.TempJob);
        var triQueue = new NativeQueue<int3>(Allocator.TempJob);
        var map = new NativeHashMap<int3, int>(totalVerts, Allocator.TempJob);

        //STEP 4
        {
            var pairIndex = 0;
            for (var dx = 0; dx < Chunks; dx++) {
                for (var dz = 0; dz < Chunks; dz++) {
                    new BuildTriQueue() {
                        InVertices = pairArrays[pairIndex],
                        Counts = counts,
                        CountIndex = pairIndex,
                        TriQueue = triQueue.AsParallelWriter(),
                        VertexMap = map,
                        MySDF = sdf,
                        GridCorner = transform.position,
                        GridSize = GridSize
                    }.Schedule().Complete();

                    pairIndex++;
                }
            }
        }
        
        //Dispose
        foreach (var pairArray in pairArrays) 
        {
            pairArray.Dispose();
        }
        sdf.Dispose();
        handles.Dispose();
    }


    [BurstCompile]
    public struct BuildTriQueue : IJob
    {
        public NativeSDF MySDF;
        [ReadOnly] public NativeArray<KeyValuePair<int3, float3>> InVertices;
        [ReadOnly] public NativeArray<int> Counts;
        public int CountIndex;
        [ReadOnly] public NativeHashMap<int3, int> VertexMap;
        public NativeQueue<int3>.ParallelWriter TriQueue;
        public float3 GridCorner;
        public float GridSize;

        public void Execute()
        {
            var count = Counts[CountIndex];
            for (var i = 0; i < count; i++)
            {
                var key = InVertices[i].Key;
                TryBuildInDirection(key, new int3(1, 0, 0), new int3(0, 1, 0), new int3(0, 0, 1));
                TryBuildInDirection(key, new int3(0, 0, 1), new int3(1, 0, 0), new int3(0, 1, 0));
                TryBuildInDirection(key, new int3(0, 1, 0), new int3(0, 0, 1), new int3(1, 0, 0));
            }
        }

        private void TryBuildInDirection(int3 cell00, int3 dirA, int3 dirB, int3 normal)
        {
            var cell01 = cell00 + dirA;
            var cell10 = cell00 + dirB;
            var cell11 = cell01 + dirB;

            if (!VertexMap.TryGetValue(cell00, out var index00)) return;
            if (!VertexMap.TryGetValue(cell01, out var index01)) return;
            if (!VertexMap.TryGetValue(cell10, out var index10)) return;
            if (!VertexMap.TryGetValue(cell11, out var index11)) return;

            var centerPos0 = (float3)cell11 * GridSize + GridCorner;

            var dist0 = MySDF.Sample(centerPos0);

            if (dist0 < 0)
            {
                TriQueue.Enqueue(new int3(index00, index01, index11));
                TriQueue.Enqueue(new int3(index00, index11, index10));
            }
            else
            {
                TriQueue.Enqueue(new int3(index01, index00, index11));
                TriQueue.Enqueue(new int3(index11, index00, index10));
            }
        }
    }
}