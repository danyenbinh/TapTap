﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitView : MonoBehaviour
{
	void Start()
    {
        SceneManager.LoadSceneAsync(TTConstant.SCENE_LOADFIRST, LoadSceneMode.Additive);
    }
}
