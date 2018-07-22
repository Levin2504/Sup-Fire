﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowtrigger_5_2 : MonoBehaviour {

    private GameObject target;
    // Use this for initialization


    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(new Vector3(0f, 0.02f, 0f));
    }
    void got(GameObject target)
    {
        Collider capCo = GetComponent<Collider>();
        capCo.enabled = false;
        Destroy(gameObject, 0.5f);
        Rigidbody rigid = GetComponent<Rigidbody>();
        rigid.AddForce((-transform.position + target.transform.position) * 50f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "wall")
            Destroy(gameObject);
        else if (other.tag == "Bullet")
        {
            bulletMove_5_2 bullet = other.GetComponent<bulletMove_5_2>();
            target = bullet.comeFrom;
            target.SendMessage("SetFrozen");
            got(target);

        }
        else if (other.tag == "Missile")
        {
            MissileMove_5_2 missile = other.GetComponent<MissileMove_5_2>();
            target = missile.comeFrom;
            target.SendMessage("SetFrozen");
            got(target);

        }
        else if (other.tag == "Player")
        {
            other.transform.parent.SendMessage("SetFrozen");
            Destroy(gameObject);
        }
    }
}
