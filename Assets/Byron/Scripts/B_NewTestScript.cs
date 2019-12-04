using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Unity.Rendering;

public class B_NewTestScript : MonoBehaviour
{
    [SerializeField] private Mesh mesh;
    [SerializeField] private Material material;
    [SerializeField] private int numberToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        EntityManager entityManager = World.Active.GetOrCreateManager<EntityManager>();
        EntityArchetype entityArchetype = entityManager.CreateArchetype(
            typeof(B_LevelEntity),
            typeof(Position),
            typeof(RenderMesh),
            typeof(LocalToWorld),
            typeof(B_MoveSpeedComponent)
            );
        NativeArray<Entity> entityArray = new NativeArray<Entity>(numberToSpawn, Allocator.Temp);
        entityManager.CreateEntity(entityArchetype, entityArray);
        for (int i = 0; i < entityArray.Length; i++)
        {
            Entity entity = entityArray[i];
            entityManager.SetComponentData(entity, new B_LevelEntity { Level = Random.Range(10f, 20f)});
            entityManager.SetComponentData(entity, new Position { Value = new Vector3(Random.Range(0f, 10f), Random.Range(0f, 10f), Random.Range(0f, 10f)) });
            entityManager.SetSharedComponentData(entity, new RenderMesh
            {
                mesh = mesh,
                material = material,
            });
            entityManager.SetComponentData(entity, new B_MoveSpeedComponent { moveSpeed = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0f) });
        }
        entityArray.Dispose();
    }

}
