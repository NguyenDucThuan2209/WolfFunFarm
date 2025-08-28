using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] Vector3 origin;
    [SerializeField] Vector3 offset;
    [SerializeField] Vector2 amount;

    [ContextMenu("SpawnPrefab")]
    public void SpawnPrefab()
    {
        for (int i = 0; i < amount.x; i++)
        {
            for (int j = 0; j < amount.y; j++)
            {
                Vector3 position = origin + new Vector3(i * offset.x, 0, j * offset.z);
                GameObject instance = Instantiate(prefab, position, Quaternion.identity);
                instance.transform.SetParent(transform);
                instance.name = prefab.name + $" ({i}, {j})";
            }
        }
    }
}
