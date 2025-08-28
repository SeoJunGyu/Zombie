using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ItemSpawner : MonoBehaviour
{
    public List<Item> items;

    public float spawnInterval = 2f;
    private float spawnTime = 0f;
    public float despawnTime = 5f;
    public float range = 10f;

    void Update()
    {
        spawnTime += Time.deltaTime;
        if(spawnTime > spawnInterval)
        {
            Vector3 randomPoint = transform.position + Random.insideUnitSphere * range;
            if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
            {
                var item = Instantiate(items[Random.Range(0, items.Count)], hit.position, Quaternion.identity);

                Destroy(item.gameObject, despawnTime);
            }
            spawnTime = 0f;
        }
        
    }
}
