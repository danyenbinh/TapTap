using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TapTap
{
    public class TTObjectPool : MonoBehaviour
    {
        private int m_mountToPool;
        private GameObject m_objectToPool;

        private List<GameObject> _poolObjects = new List<GameObject>();
        
        private void Start()
        {
            m_objectToPool = ResourceObject.GetResource<GameObject>(TTConstant.CONFIG_BLOCK);
            MakePool();
        }
        private void MakePool()
        {
            for (int i = 0; i <= m_mountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(m_objectToPool, transform);
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


            GameObject obj = (GameObject)Instantiate(m_objectToPool, transform);
            obj.SetActive(false);
            _poolObjects.Add(obj);
            return obj;
        }
    }
}

