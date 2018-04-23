using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leftplat : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W))
        {//gameObject.transform.GetChild(0).transform.position += new Vector3(0, Time.deltaTime, 0);
           // gameObject.transform.GetChild(1).transform.position += new Vector3(0, Time.deltaTime, 0);
           gameObject.transform.position += new Vector3(0, Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            // gameObject.transform.GetChild(0).transform.position -= new Vector3(0, Time.deltaTime, 0);
            // gameObject.transform.GetChild(1).transform.position -= new Vector3(0, Time.deltaTime, 0);
            gameObject.transform.position -= new Vector3(0, Time.deltaTime, 0);
        }

    }
}
