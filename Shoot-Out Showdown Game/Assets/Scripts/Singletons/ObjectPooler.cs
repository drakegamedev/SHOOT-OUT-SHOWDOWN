using System.Collections.Generic;
using UnityEngine;

// Generates Pool of Objects for Optimization
public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;

    [System.Serializable]
    struct Pool
    {
        public string Id;
        public GameObject Prefab;
    }

    [Header("Properties")]
    [SerializeField] private List<Pool> pools;                                                          // Pool List
    public Dictionary<string, List<GameObject>> PoolDictionary { get; private set; } = new();           // Pool Dictionary

    #region Singleton
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // Initialize Pools
        foreach (Pool pool in pools)
        {
            List<GameObject> objectPool = new();
            PoolDictionary.Add(pool.Id, objectPool);
        }
    }

    // Spawns an Object from the Pool
    public GameObject SpawnFromPool(string id, Vector3 position, Quaternion rotation)
    {
        if (!PoolDictionary.ContainsKey(id))
        {
            Debug.LogWarning("Pool with tag " + id + " doesn't exist.");
            return null;
        }

        // Recycle Object
        foreach (GameObject go in PoolDictionary[id])
        {
            if (!go.activeInHierarchy)
            {
                GameObject recycledObject = go;
                recycledObject.SetActive(true);

                // Remove from Pool, then Set Position and Rotation
                recycledObject.transform.SetParent(null);
                recycledObject.transform.position = position;
                recycledObject.transform.rotation = rotation;

                return recycledObject;
            }
        }

        // Spawn New Object
        foreach (Pool objPool in pools)
        {
            if (objPool.Id == id)
            {
                // Spawn object and add poolable component
                GameObject newObject = Instantiate(objPool.Prefab);

                newObject.AddComponent<Poolable>();

                // Set Position and Rotation
                newObject.transform.position = position;
                newObject.transform.rotation = rotation;

                // Add Gameobject to List
                PoolDictionary[id].Add(newObject);

                newObject.transform.parent = null;

                // Initialize Name
                newObject.transform.name = newObject.transform.name + PoolDictionary[id].Count.ToString();

                return newObject;
            }
        }

        return null;
    }
}
