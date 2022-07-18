using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TapTap
{
    [System.Serializable]
    public class PathInfo
    {
        public Transform Begin;
        public Transform Target;
        public KeyCode KeyCode;
        public PathInfo(Transform begin, Transform target, KeyCode keyCode)
        {
            Begin = begin;
            Target = target;
            KeyCode = keyCode;
        }
    }
}
