using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    public float spawnTimerMax;
    float timer;
    public float minX;
    public float minY;

    void Start()
    {
        timer = spawnTimerMax;
    }

    void Update()
    {
        if (GameManager.isGameRunning)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {//time to spawn a trash
                timer += spawnTimerMax;
                SpawnTrash();
            }
        }
    }
    Vector3 GetRandomPos()
    {
        return new Vector3(Random.Range(minX, -minX), Random.Range(minY, -minY));
    }

    void SpawnTrash()
    {
        MunizCodeKit.Factory.PrefabFactory.instance.CreateItem(MunizCodeKit.Factory.PrefabFactory.FactoryProduct.randomTrash, GetRandomPos());
    }


}
