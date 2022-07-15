using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TapTap
{
    [System.Serializable]
    public class ObjectPoolIteam
    {
        public int mountToPool;
        public GameObject objectToPool;
    }
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private ObjectPoolIteam itemsToPool;

        private List<GameObject> _poolObjects = new List<GameObject>();
        
        public void Awake()
        {
            MakePool();
        }
        private void MakePool()
        {
            for (int i = 0; i <= itemsToPool.mountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(itemsToPool.objectToPool, transform);
                obj.SetActive(false);
                _poolObjects.Add(obj);
            }
        }
     
        public GameObject GetPoolObject()
        {
            for (int i = 0; i < _poolObjects.Count; i++)
            {
                if (_poolObjects[i].activeInHierarchy == false)
                {
                    return _poolObjects[i];
                }
            }


            GameObject obj = (GameObject)Instantiate(itemsToPool.objectToPool, transform);
            obj.SetActive(false);
            _poolObjects.Add(obj);
            return obj;
        }
    }
}

