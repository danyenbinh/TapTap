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
    }
}
