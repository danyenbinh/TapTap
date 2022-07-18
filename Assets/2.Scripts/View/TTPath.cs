using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TapTap
{
    public class TTPath : MonoBehaviour
    {
        private List<PathInfo> m_paths = new List<PathInfo>();

        private void Start()
        {
            var listPath = GetComponentsInChildren<TTDrawLine>();           
            for (int i = 0; i < listPath.Length; i++)
            {
                listPath[i].Init();
                m_paths.Add(listPath[i].GetPath());
            }               
        }
        public PathInfo GetPath(int index)
        {
            return m_paths[index];
        }  
    }
}



