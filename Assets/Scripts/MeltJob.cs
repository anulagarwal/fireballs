using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

[BurstCompile(FloatPrecision.Low, FloatMode.Fast)]
struct MeltJob : IJobParallelFor
{
    public NativeArray<Vector3> vertices;
    [ReadOnly] public Vector3 positionHit;
    [ReadOnly] public Vector3 digVector;

    [ReadOnly] public float power;
    [ReadOnly] public float radius;
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