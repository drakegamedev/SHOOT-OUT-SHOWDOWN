using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;

    [System.Serializable]
    public struct Pool
    {
        public string Id;
        public GameObject Prefab;
    }

    [SerializeField] private List<Pool> pools;                                                  // List of Pool Groups
    private Dictionary<string, List<GameObject>> poolDictionary = new();                        // Dictionary of Pool Groups

    #region Singleton
    private void Awake()
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
    private void Start()
    {
        // Initialize pools
        foreach (Pool pool in pools)
        {
            List<GameObject> objectPool = new();
            poolDictionary.Add(pool.Id, objectPool);
        }
    }

    // Spawns an Object from the Pool
    public GameObject SpawnFromPool(string id, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(id))
        {
            Debug.LogWarning("Pool with tag " + id + " doesn't exist.");
            return null;
        }

        // Recycle Object
        foreach (GameObject go in poolDictionary[id])
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

                // Set Position and Rotation
                newObject.transform.position = position;
                newObject.transform.rotation = rotation;

                // Add Gameobject to List
                poolDictionary[id].Add(newObject);

                newObject.transform.parent = null;

                // Initialize Name
                newObject.transform.name = newObject.transform.name + poolDictionary[id].Count.ToString();

                return newObject;
            }
        }

        return null;
    }

    // Returns the Poolable GameObject Back to the Pool
    public void ReturnToPool(GameObject go)
    {
        go.transform.SetParent(transform);
        go.SetActive(false);
    }
}
