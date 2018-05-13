﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
public class BulletMove_3 : MonoBehaviour
{

    public float bulletSpeed;
    public GameObject comeFrom;
    public bool isMulti;
    public bool isBig;
    public bool isFrozen;//
    public GameObject left;//
    public GameObject right;//
    public GameObject foodholder;

    GameObject[] sparks;
    GameObject[] explosion;
    GameObject[] delay;
    GameObject[] hit;

    public AudioSource expSound;
    public AudioSource hitSound;

    private bool damagded = false;

    private void Awake()
    {
        sparks = GameObject.FindGameObjectsWithTag("sparks");
        explosion = GameObject.FindGameObjectsWithTag("explosion");
        

    }
    void Start()
    {
        
        delay = GameObject.FindGameObjectsWithTag("delay");
        transform.Rotate(0f, 90f, 90f);

    }

    void SetMulti(bool multi)
    {
        isMulti = multi;
    }

    void SetBig(bool big)
    {
        isBig = big;
    }
    void SetFrozen(bool Frozen)//
    {
        isFrozen = Frozen;
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.right * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerStay(Collider other)
    {   if (other.gameObject.name == "FoodHolder")
            foodholder.SendMessage("hit", true);

        if (other.tag == "wall")
        {
            hitSound.pitch = 0.1f * 1.05946f * Random.Range(8, 15);
            //0.8-1.5 as normal, 0.5-0.8 as big, need more modification
            hitSound.Play();
            GameObject newSparks = Instantiate(sparks[0], transform.position, transform.rotation) as GameObject;
            if (isBig)
            {
                newSparks.transform.localScale = new Vector3(2f, 2f, 2f);
                CameraShaker.Instance.ShakeOnce(1.5f, 4f, 0f, 2f);
            }
            else
            {
                CameraShaker.Instance.ShakeOnce(1f, 4f, 0f, 0.8f);
            }
            comeFrom.SendMessage("SetAmmo", isMulti ? 0.5f : 1f);
            Destroy(gameObject);
            Destroy(newSparks, 0.5f);
        }
        else if (other.tag == "Player")
        {
            GameObject newExplosion = Instantiate(explosion[0], transform.position, transform.rotation) as GameObject;
            GameObject newDelay = Instantiate(delay[0], transform.position, transform.rotation) as GameObject;
            newDelay.SetActive(true);
            if (isBig)
            {
                newExplosion.transform.localScale = new Vector3(2f, 2f, 2f);
            }
            else if (isFrozen)//
            {
                other.transform.parent.SendMessage("Buff_Time", Time.time);
                if(other.transform.parent.name == "Player1")
                {
                    left.SendMessage("Buff_Time", Time.time);

                }
                else if (other.transform.parent.name == "Player2")
                {
                    right.SendMessage("Buff_Time", Time.time);

                }

            }
            expSound.pitch = Random.Range(0.7f, 1.5f);
            expSound.Play();

            CameraShaker.Instance.ShakeOnce(isBig ? 6f : 3f, 20f, 0f, 0.5f);
            comeFrom.SendMessage("SetAmmo", isMulti ? 0.5f : 1f);
            Destroy(gameObject);
            Destroy(newExplosion, 2.0f);
            Destroy(newDelay, 2.0f);
            if (!damagded)
            {
                other.transform.parent.SendMessage("SetLife", isBig ? -2 : -1);
                damagded = !damagded;

            }
        }


    }
}