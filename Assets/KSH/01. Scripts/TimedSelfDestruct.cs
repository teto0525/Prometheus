using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedSelfDestruct : MonoBehaviour
{
    public float LifeTime = 1f;

    float SpawnTime;

    void Awake()
    {
        SpawnTime = Time.time;
    }

    void Update()
    {
        if(Time.time > SpawnTime + LifeTime)
        {
            Destroy(gameObject);
        }
    }
}
