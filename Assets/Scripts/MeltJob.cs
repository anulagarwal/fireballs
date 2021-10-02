using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

[BurstCompile]

struct MeltJob : IJobParallelFor
{
    public NativeArray<Vector3> vertices;
    public Vector3 positionHit;
    public Vector3 digVector;

    public float power;

    public float radius;
    public void Execute(int i)
    {
        var vertex = vertices[i];

        float distance = (vertex - positionHit).sqrMagnitude;

        if (distance < radius) {
            vertex -= (digVector * power); 
            vertices[i] = vertex;
        }
    }
}