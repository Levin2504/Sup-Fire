using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour {
    public float cd;
    private float temp;
    private int lastkid;
    public Material normal;
    public Material food;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Time.time - temp >= cd)
        {

            int childNum;
            childNum = Random.Range(0, 14);
 
            gameObject.transform.GetChild(childNum).GetComponent<MeshRenderer>().material = food;
            gameObject.transform.GetChild(childNum).gameObject.SendMessage("befoodhoodler", true);
            gameObject.transform.GetChild(lastkid).GetComponent<MeshRenderer>().material = normal;
            gameObject.transform.GetChild(lastkid).gameObject.SendMessage("befoodhoodler", false);

            lastkid = childNum;
           temp = Time.time;

        }
    }
}
