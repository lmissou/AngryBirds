using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        Instantiate(Resources.Load(PlayerPrefs.GetString("nowLevel")));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
