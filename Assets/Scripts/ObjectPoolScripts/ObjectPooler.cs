using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeepItAlive
{
    public class ObjectPooler : MonoBehaviour
    {
        #region private Variable
        [SerializeField] private Dictionary<string, Queue<GameObject>> _poolDictionary;
        [SerializeField] private Transform _pooledObjParent = null;
        #endregion

        #region Public Variable
        public List<Pool> pools;

        public Transform Target;

        public bool EnemyPool;

        public Transform PooldeObjParent => _pooledObjParent;
        #endregion
        #region Singelton

        public static ObjectPooler Instance;



        #endregion

        private void Awake()
        {
            Instance = this;

            _poolDictionary = new Dictionary<string, Queue<GameObject>>();

            foreach (Pool pool in pools)
            {
                Queue<GameObject> objectPool = new Queue<GameObject>();

                for (int i = 0; i < pool.size; i++)
                {
                    GameObject obj = Instantiate(pool.prefab);
                    obj.transform.SetParent(_pooledObjParent);

                    if (EnemyPool)
                    {
                        obj.TryGetComponent<Enemy>(out Enemy enemy);
                        enemy.Target = Target;
                    }

                    obj.SetActive(false);
                    objectPool.Enqueue(obj);
                }

                _poolDictionary.Add(pool.tag, objectPool);
            }
        }

        public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
        {

            if (!_poolDictionary.ContainsKey(tag))
            {
                return null;
            }

            GameObject objectToSpawn = _poolDictionary[tag].Dequeue();

            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;

            objectToSpawn.TryGetComponent<IPooledObject>(out IPooledObject pooledObj);

            pooledObj?.OnObjectSpawn();



            _poolDictionary[tag].Enqueue(objectToSpawn);

            return objectToSpawn;
        }

        [System.Serializable]
        public class Pool
        {
            public string tag;
            public GameObject prefab;
            public int size;
        }
    }
}

