using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AC : MonoBehaviour {
    public Material cool;
    public Material heat;
    public Material coolbg;
    public Material heatbg;
    public GameObject bg;
    public GameObject player1;
    public GameObject player2;
    public float cd;
    private float temp;
    public bool flag;
	// Use this for initialization
	void Start () {
        temp = 0;
        flag = true;
	}

    // Update is called once per frame
    void Update()
    {
        if (Time.time - temp >= cd)
        {
            temp = Time.time;
            if (flag)
            {
                transform.GetChild(4).GetComponent<MeshRenderer>().material = heat;
                bg.GetComponent<MeshRenderer>().material = heatbg;
                player1.SendMessage("HeatMode");
                player1.SendMessage("ResetQueue");
                player2.SendMessage("HeatMode");
                player2.SendMessage("ResetQueue");
                flag = false;
            }
            else
            {
                transform.GetChild(4).GetComponent<MeshRenderer>().material = cool;
                bg.GetComponent<MeshRenderer>().material = coolbg;
                player1.SendMessage("CoolMode");
                player2.SendMessage("CoolMode");
                flag = true;
            }
        }
        
    }


}
