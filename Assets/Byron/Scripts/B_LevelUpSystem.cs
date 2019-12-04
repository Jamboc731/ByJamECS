using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class B_LevelUpSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        ForEach((ref B_LevelEntity newLevel) =>
        {
            newLevel.Level += 1f * Time.deltaTime;
        });
    }
}
