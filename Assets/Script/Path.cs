using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TapTap
{
    public class Path : MonoBehaviour
    {
        [SerializeField] List<PathInfo> paths;

        public PathInfo GetPath(int index)
        {
            return paths[index];
        }  
    }
}



