using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rightwall : MonoBehaviour {
    private Rigidbody itself;
    public bool buff_frozen;
    public float buff_begin_time;
    public float buff_exist_time;
    
    void Start () {
        itself = gameObject.GetComponent<Rigidbody>();
	}
    void testbuff()
    {
        if (buff_begin_time != 0)
        {
            if (Time.time - buff_begin_time >= buff_exist_time)
            {
                buff_frozen = false;
                buff_begin_time = 0;
            }
            else
                buff_frozen = true;
        }
    }
    void Buff_Time(float buff_begin)//
    {
        buff_begin_time = buff_begin;


    }
    // Update is called once per frame
    void Update () {
        testbuff();
        float v_dir = Input.GetAxis("J-Vertical");
        if (buff_frozen)
        {
            v_dir = 0;
        }
        itself.velocity = new Vector3(0,  v_dir, 0);
    }
}
