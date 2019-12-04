using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;

public class B_MoverSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        ForEach(( ref Position pos, ref B_MoveSpeedComponent currentSpeed) => {
            pos.Value.x += currentSpeed.moveSpeed.x * Time.deltaTime;
            pos.Value.y += currentSpeed.moveSpeed.y * Time.deltaTime;
            pos.Value.z += currentSpeed.moveSpeed.z * Time.deltaTime;
        });
    }
}
