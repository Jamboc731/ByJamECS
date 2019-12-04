using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class J_LevelUpSystem : ComponentSystem
{
    protected override void OnUpdate()
    {

        ForEach((ref J_LevelComponent lc) =>
        {
            lc.level += 1f * Time.deltaTime;
        });

    }
}
