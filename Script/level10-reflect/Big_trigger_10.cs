using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Big_trigger_10 : MonoBehaviour {
    private float spawntime;
	// Use this for initialization
	void Start () {
        spawntime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time - spawntime >= 1)
            Destroy(gameObject);
	}
}
