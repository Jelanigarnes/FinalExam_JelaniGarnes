using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    //using singleton to have access to the script
    public static ObjectPooler instance;

    public List<Pool> pools;
    Queue<GameObject> objectPool;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in pools)
        {
            objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tags, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tags, Vector3 position, Quaternion rotation)
    {
        if (poolDictionary.ContainsKey(tags))
        {
            Debug.LogWarning("Pool with the tag" + tags + "doesn't exist");
            return null;
        }
        GameObject objectToSpawn = poolDictionary[tags].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        poolDictionary[tags].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

}
