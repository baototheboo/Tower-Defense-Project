using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class ObjectPool : MonoBehaviour
    {
        [System.Serializable]
        public class Pool
        {
            public string tag;
            public GameObject prefab;
            public int size;
        }

        public static ObjectPool Instance;

        private void Awake()
        {
            Instance = this;
        }

        public List<Pool> pools;
        public Dictionary<string, Queue<GameObject>> poolDictionary;

        private void Start()
        {
            poolDictionary = new Dictionary<string, Queue<GameObject>>();
            foreach (Pool pool in pools)
            {
                Queue<GameObject> objectPool = new Queue<GameObject>();
                for(int i = 0; i < pool.size; i++)
                {
                    GameObject obj = Instantiate(pool.prefab);
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);
                }

                poolDictionary.Add(pool.tag, objectPool);
            }
        }

        public GameObject SpawnFromPool(String tag, Vector3 position, Quaternion rotation)
        {
            if (!poolDictionary.ContainsKey(tag)){
                Debug.Log("Null by tag");
                return null;
            }
            GameObject objectToSpawn = poolDictionary[tag].Dequeue();
            
            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;

            poolDictionary[tag].Enqueue(objectToSpawn);

            return objectToSpawn;
        }

        public void AddToPool(string tag)
        {
            int index = 0;
            if (tag.Equals("Enemy1"))
            {
                index = 0;
            } else if (tag.Equals("Enemy2"))
            {
                index = 1;
            } else if (tag.Equals("Enemy3"))
            {
                index = 2;
            }
            GameObject obj = Instantiate(pools[index].prefab);
            obj.SetActive(false);
            poolDictionary[tag].Enqueue(obj);
        }
    }
}
