﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearPlayerprefs : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		PlayerPrefs.DeleteAll();
	}
}
