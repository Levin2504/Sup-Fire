﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerP1_AITEST_DESERT_ACROSSTHECANYON : MonoBehaviour {
    public Boundary1Stick_2_5 boundary1stick;

    public float Accelrate;
    public float MaxSpeed;
    public bool isFireing;
    public bulletMove_2_5 bullet;
    public MissileMove_2_5 missile;
    public Transform firepoint;
    public float bulletSpeed;
    public AudioSource audioS;
    public AudioSource audioSB;
    public AudioSource audioR;
    public AudioSource audioM;
    private float wind;

    private bool isGrounded;
    private float h_axis;
    private float v_axis;

    public int special;

    public Animator anim;
    public Animator MoveAnim;

    public int maxAmmo;
    public float remainAmmo;
    public GameObject AmmoCount;
    public GameObject LifeCount;
    public GameObject SpeCount;


    public float timeBetweenShots;
    private float shotCounter;


    public bool isBig;
    public bool isMulti;
    public bool isMissile;
    public bool isFrozen;//
    public bool buff_frozen;//
    public float buff_exist_time;//
    public float buff_begin_time;//
    public float buff;//

    public Material ice;//
    public Material normal;//

    public float maxLife;
    public float remainLife;


    private Rigidbody rigid;
    private float angle = 0f;
    private int activeTurret = 1;

    public Vector3 recoil;
    public float recoilIntensity;

    private GameObject player;
    private bool SetScore = false;

    private Quaternion LastDirection;
    private bool isSpecial = false;

    
    private Vector3 movement;
    private float  XMovespeed;
    private float YMovespeed;
    private Vector3 target;
    private Vector3 BulletPos;
    private Vector3 BulletSpeed;
    private float BulletPosLastTime;
    public float BulletXmax;
    public float BulletXmin;
    public float BulletYmax;
    public float BulletYmin;
    public float PlayerXmax;
    public float PlayerXmin;
    public float PlayerYmax;
    public float PlayerYmin;
    public float upforceRange;
    public float upforceMagnitude;
    private int hit;
    public GameObject target1;


    void HitDetect(int x)
    {
        hit = x;
    }

    void GetBulletPos(Vector3 x)
    {
        BulletPos = x;
    }
    void GetBulletSpeed(Vector3 x)
    {

        BulletSpeed = x;
    }
    void GetBulletTime(float x)
    {
        BulletPosLastTime = x;
    }

    void MovementSet()
    {
        if (transform.position.x <= PlayerXmin)
            XMovespeed = 1;
        else if (transform.position.x >= PlayerXmax)
            XMovespeed = -1;
        else if (transform.position.y >= PlayerYmax)
            YMovespeed = -1;
        else if (transform.position.y <= PlayerYmin)
            YMovespeed = 1;
        else if (Time.time - BulletPosLastTime < 1)
        {

            
            if (hit==1)
            {
               
                XMovespeed =  BulletSpeed.y*10;
                YMovespeed =  -BulletSpeed.x*10;

            }
            else if(hit==2)
            {

                XMovespeed = BulletSpeed.y * 10;
                YMovespeed = BulletSpeed.x * 10;

            }
            //  else if (BulletPos.x - transform.position.x <= 1 && BulletPos.x - transform.position.x > -2)
            //   Movespeed = 1; ;//move right
        }
        else
        {
            hit = 0;
            XMovespeed = 0;
            YMovespeed = 0;
        }

    }

    int Aimtest(Vector3 targetpos)
    {
       
        Vector3 velocity;
        if (isMissile)
            return 0;
        Vector3 windy = new Vector3(0, upforceMagnitude, 0);
        for (int i = 5; i < 175; i++)
        {
            velocity = Quaternion.Euler(i, 90, 90) * Vector3.right * 8.8f * Time.deltaTime;
            Vector3 p = firepoint.transform.position;
            while (p.x < BulletXmax && p.x > BulletXmin&&p.y<BulletYmax&&p.y>BulletYmin)
            {
                if (p.x < upforceRange && p.x > -upforceRange)
                {
                    if (WindController.wind > 0)
                        velocity += windy * Time.deltaTime * Time.deltaTime;
                    else if (WindController.wind < 0)
                        velocity -= windy * Time.deltaTime * Time.deltaTime;
                }
                p += velocity;

                if (Mathf.Abs(p.x - targetpos.x) <= 0.5&& (Mathf.Abs(p.y - targetpos.y) <= 0.5))
                    return i;
            }
          

        }
        return 90;
    }


    void SetBig()
    {
        isBig = true;
        isMulti = false;
        isMissile = false;
        isFrozen = false;//
        audioR.Play();
        special = 5;
        isSpecial = true;
        UseTurret1();
        gameObject.transform.GetChild(1).transform.localScale = new Vector3(0.5f, 0.5f, 0.3f);
        this.transform.GetChild(1).GetChild(1).GetChild(1).gameObject.SetActive(false);
    }

    void SetMulti()
    {
        isBig = false;
        isMulti = true;
        isMissile = false;
        isFrozen = false;//
        audioR.Play();
        special = 5;
        isSpecial = true;
        gameObject.transform.GetChild(1).transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        UseTurret2();
    }

    void SetFrozen()//
    {
        isBig = false;
        isMulti = false;
        isMissile = false;
        isFrozen = true;//
        audioR.Play();
        special = 5;
        isSpecial = true;
        gameObject.transform.GetChild(1).transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        this.transform.GetChild(1).GetChild(1).GetChild(1).gameObject.SetActive(true);
        UseTurret1();

    }

    void SetMissile()
    {
        isBig = false;
        isMulti = false;
        isMissile = true;
        isFrozen = false;//
        audioR.Play();
        special = 3;
        isSpecial = true;
        gameObject.transform.GetChild(1).transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        UseTurret3();
    }

    void Buff_Time(float buff_begin)//
    {
        buff_begin_time = buff_begin;
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

    void SetLife(int change)
    {
        remainLife += change;
        if (remainLife > maxLife)
        {
            remainLife = maxLife;
        }
    }

    void SetAmmo(float change)
    {
        remainAmmo += change;
        if (remainAmmo > maxAmmo)
        {
            remainAmmo = maxAmmo;
        }
    }

    private void UseTurret1()
    {
        anim.Rebind();
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
        gameObject.transform.GetChild(2).gameObject.SetActive(false);
        gameObject.transform.GetChild(3).gameObject.SetActive(false);
        activeTurret = 1;
        this.firepoint = transform.GetChild(1).GetChild(2).GetComponent<Transform>();
        this.SpeCount = transform.GetChild(1).GetChild(1).GetChild(0).gameObject;
        this.anim = transform.GetChild(1).GetChild(1).GetComponent<Animator>();
        transform.GetChild(activeTurret).rotation = LastDirection;
    }

    private void UseTurret2()
    {
        anim.Rebind();
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
        gameObject.transform.GetChild(2).gameObject.SetActive(true);
        gameObject.transform.GetChild(3).gameObject.SetActive(false);
        activeTurret = 2;
        this.firepoint = transform.GetChild(2).GetChild(2).GetComponent<Transform>();
        this.SpeCount = transform.GetChild(2).GetChild(1).GetChild(2).gameObject;
        this.anim = transform.GetChild(2).GetChild(1).GetComponent<Animator>();
        this.transform.GetChild(1).GetChild(1).GetChild(1).gameObject.SetActive(false);
        transform.GetChild(activeTurret).rotation = LastDirection;
    }

    private void UseTurret3()
    {
        anim.Rebind();
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
        gameObject.transform.GetChild(2).gameObject.SetActive(false);
        gameObject.transform.GetChild(3).gameObject.SetActive(true);
        activeTurret = 3;
        this.firepoint = transform.GetChild(3).GetChild(2).GetComponent<Transform>();
        this.SpeCount = transform.GetChild(3).GetChild(1).GetChild(1).gameObject;
        this.anim = transform.GetChild(3).GetChild(1).GetComponent<Animator>();
        this.transform.GetChild(1).GetChild(1).GetChild(1).gameObject.SetActive(false);
        transform.GetChild(activeTurret).rotation = LastDirection;
    }

    void Start()
    {
        rigid = this.GetComponent<Rigidbody>();
        //        transform.GetChild(1).transform.Rotate(0f, -90f, 0f);
        LastDirection = new Quaternion(0f, 90f, 0f, 1f);
        wind = WindController.wind;
    }


    void FixedUpdate()
    {
        wind = WindController.wind;
        rigid.position = new Vector3
        (
            Mathf.Clamp(rigid.position.x, boundary1stick.xMin, boundary1stick.xMax),
            Mathf.Clamp(rigid.position.y, boundary1stick.yMin, boundary1stick.yMax),
            Mathf.Clamp(rigid.position.z, boundary1stick.zMin, boundary1stick.zMax)
        );
        //   Vector3 pos = rigid.position;

        //   float v_dir = Input.GetAxis("J2-V-Direct");
        // float h_dir = Input.GetAxis("J2-H-Direct");
        //
        //   Vector3 direction = Vector3.zero;

        //  direction.x = -h_dir;
        //  direction.y = v_dir;

        // angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        target = target1.transform.position;
        Quaternion rotation = Quaternion.AngleAxis(Aimtest(target), new Vector3(0f, 0f, -1f));

        recoil = recoilIntensity * -(firepoint.transform.position - gameObject.transform.position).normalized;

     //   if (direction.magnitude >= 0.5)
     //   {
            transform.GetChild(activeTurret).rotation = rotation;
            LastDirection = rotation;
        //   }
        //  else
        //   {
        //       transform.GetChild(activeTurret).rotation = LastDirection;
        //   }

        //  h_axis = Input.GetAxis("J2-Horizontal");
        //  v_axis = Input.GetAxis("J2-Vertical");
        MovementSet();
        if (XMovespeed != 0||YMovespeed!=0)
        {
            MoveAnim.Play("body Animation");
        }

        testbuff();
        if (buff_frozen)//
        {
            gameObject.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = ice;
            buff = 0.6f;
        }
        else
        {
            gameObject.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = normal;
            buff = 1f;
        }

        rigid.velocity = new Vector3(XMovespeed*buff*Accelrate, buff * Accelrate * YMovespeed, 0f);

        if ( remainAmmo >= 1) //fire

        {
            isFireing = true;
        }
        else
        {
            isFireing = false;
        }

        if (isFireing)
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0)
            {
                shotCounter = timeBetweenShots;
                audioS.volume = 0.3f;

                if (isMulti)
                {
                    special -= 1;
                    bulletMove_2_5 newBullet1 = Instantiate(bullet, firepoint.position, firepoint.rotation) as bulletMove_2_5;
                    bulletMove_2_5 newBullet2 = Instantiate(bullet, firepoint.position, firepoint.rotation) as bulletMove_2_5;

                    newBullet1.gameObject.SetActive(true);
                    newBullet1.transform.Translate(new Vector3(0.2f, 0f, 0f));
                    newBullet1.transform.Rotate(new Vector3(0f, 0f, -5f));
                    newBullet1.bulletSpeed = bulletSpeed;
                    newBullet1.SendMessage("SetMulti", true);

                    newBullet2.gameObject.SetActive(true);
                    newBullet2.transform.Translate(new Vector3(-0.2f, 0f, 0f));
                    newBullet2.transform.Rotate(new Vector3(0f, 0f, 5f));
                    newBullet2.bulletSpeed = bulletSpeed;
                    newBullet2.SendMessage("SetMulti", true);

                    //CameraShaker.Instance.ShakeOnce(1.5f, 4f, 0f, 1.5f);
                    rigid.AddForce(1.5f * recoil, ForceMode.Impulse);
                    audioS.pitch = Random.Range(1f, 5f);
                    anim.Play("Double gun Animation");
                }
                else
                {

                    if (isMissile)
                    {
                        special -= 1;
                        MissileMove_2_5 newMissile = Instantiate(missile, firepoint.position, firepoint.rotation) as MissileMove_2_5;
                        newMissile.gameObject.SetActive(true);
                        //CameraShaker.Instance.ShakeOnce(2f, 4f, 0f, 1.5f);
                        anim.Play("Missile Launcher Animation");
                    }
                    else
                    {
                        bulletMove_2_5 newBullet = Instantiate(bullet, firepoint.position, firepoint.rotation) as bulletMove_2_5;
                        newBullet.gameObject.SetActive(true);
                        newBullet.bulletSpeed = bulletSpeed;
                        if (isBig)
                        {
                            audioSB.pitch = Random.Range(0.2f, 0.3f);
                            audioSB.volume = 1.0f;
                            special -= 1;
                            newBullet.transform.localScale = new Vector3(1f, 1f, 1f);
                            Animator a = newBullet.GetComponent<Animator>();
                            //                           ParticleSystem p = newBullet.GetComponent<ParticleSystem>();
                            a.enabled = false;
                            newBullet.SendMessage("SetBig", true);
                            //CameraShaker.Instance.ShakeOnce(2.5f, 4f, 0f, 3f);
                            rigid.AddForce(2.0f * recoil, ForceMode.Impulse);
                        }
                        else if (isFrozen)//
                        {
                            special -= 1;
                            newBullet.SendMessage("SetFrozen", true);
                            newBullet.transform.GetChild(0).gameObject.SetActive(true);
                            newBullet.GetComponent<ParticleSystemRenderer>().material = ice;
                            //CameraShaker.Instance.ShakeOnce(1.25f, 4f, 0f, 1.5f);
                            audioS.pitch = Random.Range(1f, 5f);
                        }
                        else
                        {
                            //CameraShaker.Instance.ShakeOnce(1.25f, 4f, 0f, 1.5f);
                            rigid.AddForce(recoil, ForceMode.Impulse);
                            audioS.pitch = Random.Range(1f, 5f);
                        }
                    }
                    anim.Play("Gun Animation");
                }

                SetAmmo(-1);

                if (!isMissile)
                {
                    if (isBig)
                    {
                        audioSB.Play();
                    }
                    else
                    {
                        audioS.Play();
                    }
                }
                else
                {
                    audioM.pitch = Random.Range(0.8f, 1.2f);
                    audioM.Play();
                }
            }
        }
        else
        {
            //shotCounter = 0;
            shotCounter -= Time.deltaTime;

        }
        AmmoCount.SendMessage("SetAmmo", Mathf.Floor(remainAmmo));

        float liftRatio = ((maxLife - 1) / maxLife) * remainLife / maxLife + 1f / maxLife;

        transform.GetChild(0).transform.localScale = new Vector3(1.5f * liftRatio, 0.3f, 0.5f);

        LifeCount.SendMessage("SetLife", remainLife);

        if (remainLife <= 0)
        {
            StartCoroutine(DelayTime(0.3f));
            Time.timeScale = 0.2f;
            Application.targetFrameRate = 150;
            GameObject[] score = GameObject.FindGameObjectsWithTag("Score");
            if (!SetScore)
            {
                score[0].SendMessage("rightPlus");
                SetScore = !SetScore;
            }
        }
        if (isSpecial && special <= 0)
        {
            isBig = false;
            isMulti = false;
            isMissile = false;
            isFrozen = false;//
            gameObject.transform.GetChild(1).transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            this.transform.GetChild(1).GetChild(1).GetChild(1).gameObject.SetActive(false);
            UseTurret1();
            isSpecial = !isSpecial;
        }

        SpeCount.SendMessage("SetSpe", special);

    }

    IEnumerator DelayTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        Time.timeScale = 1f;
        Application.targetFrameRate = -1;
        gameObject.SetActive(false);
    }

}

