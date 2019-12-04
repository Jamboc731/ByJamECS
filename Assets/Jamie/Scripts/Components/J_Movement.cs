using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Collections;
using Unity.Mathematics;

public class J_Movement : ComponentSystem
{
    float time;
    protected override void OnUpdate()
    {
        time = Time.deltaTime;

        NativeArray<float3> aPos = new NativeArray<float3>(GetEntities<Position>().Length, Allocator.Temp);
        NativeArray<float3> aDir = new NativeArray<float3>(GetEntities<J_Mover>().Length, Allocator.Temp);
        NativeArray<float> aSp = new NativeArray<float>(GetEntities<J_Mover>().Length, Allocator.Temp);

        for (int i = 0; i < GetEntities<Position>().Length; i++)
        {
            aPos[i] = GetEntities<Position>()[i].Value;
        }

        for (int i = 0; i < GetEntities<J_Mover>().Length; i++)
        {
            aDir[i] = GetEntities<J_Mover>()[i].dir;
            aSp[i] = GetEntities<J_Mover>()[i].speed;
        }

        MoveJob job = new MoveJob
        {
            t = time,
            _posArray = aPos,
            _moveArray = aDir,
            _spArray = aSp
        };

        job.Schedule(GetEntities<Position>().Length, GetEntities<Position>().Length/10);

    }


}

public struct MoveJob : IJobParallelFor
{

    public NativeArray<float3> _posArray;
    public NativeArray<float3> _moveArray;
    public NativeArray<float> _spArray;
    public float t;

    public void Execute(int i)
    {
        _posArray[i] += _moveArray[i] * _spArray[i] * t;
    }
}
