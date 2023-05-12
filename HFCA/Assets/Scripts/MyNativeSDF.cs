using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using SDF;
using Unity.Collections;

public class MyNativeSDF : MonoBehaviour
{
    public NativeSDF MySDF;
    public float GridSize;
    public float3 GridCorner;
    
    [ReadOnly]
    public NativeHashMap<int3, int> VertexMap;
    public NativeQueue<int3>.ParallelWriter TriQueue;
    

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

        if (dist0 < 0) {
            TriQueue.Enqueue(new int3(index00, index01, index11));
            TriQueue.Enqueue(new int3(index00, index11, index10));
        } else {
            TriQueue.Enqueue(new int3(index01, index00, index11));
            TriQueue.Enqueue(new int3(index11, index00, index10));
        }
    }
}

    
    

