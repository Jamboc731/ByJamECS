using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_CubeMove : MonoBehaviour
{

    float speed;
    Vector3 dir;

    private void Start()
    {
        speed = Random.Range(0f, 10f);
        dir = new Vector3(Random.Range(-100f, 100f), 0, Random.Range(-100f, 100f)).normalized;
    }
    
    private void Update()
    {
        transform.Translate(dir * speed * Time.deltaTime);
    }

}
