using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TapTap
{
    public class ButtonPress : MonoBehaviour
    {
        [SerializeField] private KeyCode keyCode;
        void Update()
        {
            if (Input.GetKeyDown(keyCode))
            {
                Debug.Log(keyCode.ToString());
            }
        }
    }
}

