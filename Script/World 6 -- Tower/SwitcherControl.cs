﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitcherControl : MonoBehaviour {
    public GameObject lights;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);

        lights.SendMessage("lightsoff");
        Destroy(gameObject);
    }

}
