using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Unity.Rendering;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Burst;

public class J_Test : MonoBehaviour
{

    [SerializeField] GameObject go;

    [SerializeField] bool dots = true;
    [SerializeField] int numberOfSpawns = 20000;

    private void Start()
    {
        if (dots)
        {
            EntityManager eMan = World.Active.GetOrCreateManager<EntityManager>();

            EntityArchetype eArch = eMan.CreateArchetype(
                typeof(J_LevelComponent),
                typeof(Position),
                typeof(RenderMesh),
                typeof(LocalToWorld),
                typeof(J_Mover)
                );

            NativeArray<Entity> entArray = new NativeArray<Entity>(numberOfSpawns, Allocator.Temp);

            eMan.CreateEntity(eArch, entArray);

            for (int i = 0; i < entArray.Length; i++)
            {
                Entity e = entArray[i];

                eMan.SetComponentData(e, new J_LevelComponent { level = UnityEngine.Random.Range(0, 10) });
                eMan.SetComponentData(e, new J_Mover
                {
                    speed = UnityEngine.Random.Range(0f, 10f),
                    dir = math.normalize(new float3(UnityEngine.Random.Range(-100, 100), 0, UnityEngine.Random.Range(-100, 100)))
                });

                eMan.SetSharedComponentData(e, new RenderMesh
                {
                    mesh = go.GetComponent<MeshFilter>().sharedMesh,
                    material = go.GetComponent<Renderer>().sharedMaterial
                });
            }

            entArray.Dispose();

        }
        else
        {
            for (int i = 0; i < numberOfSpawns; i++)
            {
                Instantiate(go);
            }
        }

    }

}