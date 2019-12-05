using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Burst;

[BurstCompile]
public class J_Movement : JobComponentSystem
{
    float time;

    private struct MoveJob : IJobProcessComponentData<Position, J_Mover>
    {

        public float t;

        public void Execute(ref Position c0, ref J_Mover c1)
        {
            c0.Value += c1.dir * c1.speed * t;

        }
    }
    protected override JobHandle OnUpdate(JobHandle jH)
    {
        time = Time.deltaTime;

        MoveJob job = new MoveJob
        {
            t = time
        };

        JobHandle moveHandle = job.Schedule(this);

        return moveHandle;

    }


}

//public class MoveJobSystem : JobComponentSystem
//{


//    protected override JobHandle OnUpdate(JobHandle inputDeps)
//    {
//        throw new System.NotImplementedException();
//    }
//}