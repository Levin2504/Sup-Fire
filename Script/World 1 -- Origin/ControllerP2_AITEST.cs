using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;



public class ControllerP2_AITEST : MonoBehaviour
{

    public Boundary1Stick boundary1stick;

    public float Accelrate;
    public float MaxSpeed;
    public bool isFireing;
    public bulletMove bullet;
    public MissileMove missile;
    public Transform firepoint;
    public float bulletSpeed;
    public AudioSource audioS;
    public AudioSource audioSB;
    public AudioSource audioR;
    public AudioSource audioM;

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

    public float recoil;//in angular
    public float recoilIntensity;
    private int updownrecoil;
    private Vector3 left;
    private Vector3 right;

    private GameObject player;
    private bool SetScore = false;

    private Quaternion LastDirection;
    private bool isSpecial = false;

    public GameObject target;
    private Vector3 movement;
    private float Movespeed;
    private float targetXpos;
    private float BulletPos;
    private float BulletPosLastTime;
    public float BulletXmax;
    public float BulletXmin;
    public float BulletYmax;
    public float BulletYmin;
    public float PlayerXmax;
    public float PlayerXmin;



 


    void GetBulletPos(Vector3 x)
    {
        BulletPos = x.x;
        

    }
    void GetBulletTime(float x)
    {
        BulletPosLastTime = x;
    }

    void MovementSet()
    {
        if (Time.time - BulletPosLastTime < 2)
        {

            if (transform.position.x <= PlayerXmin)
                Movespeed = 1;
            else if (transform.position.x >= PlayerXmax)
                Movespeed = -1;
            else if (BulletPos - transform.position.x >= 0.5 && BulletPos - transform.position.x < 2)
                Movespeed = 1;//move left 
            else if (BulletPos - transform.position.x >= -2 && BulletPos - transform.position.x < 0.5)
                Movespeed = -1; ;//move right
        }
        else
        {
            Movespeed = 0;
        }
        
    }

    int Aimtest( )
    {
        targetXpos = target.transform.position.x;
        Vector3 velocity;
        if (isMissile)
            return 0;

        for (int i = -5; i > -60; i--) {
            velocity= Quaternion.Euler(i,90,90) * Vector3.right * 8.8f * Time.deltaTime;
            Vector3 p = firepoint.transform.position;
            while (p.y > BulletYmin && p.x < BulletXmax && p.x > BulletXmin)
            {
                velocity += Physics.gravity * Time.deltaTime * Time.deltaTime;
                p += velocity;
            }
            if (Mathf.Abs(p.x - targetXpos) <= 0.5)
                return i;

        }
        return -45;
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

    void recoiltest(Vector3 dir)
    {
        if (Mathf.Atan(dir.y / dir.x) * Mathf.Rad2Deg >= -45 && Mathf.Atan(dir.y / dir.x) * Mathf.Rad2Deg <= 45 && dir.x > 0)
            updownrecoil = 0;
        else if (Mathf.Atan(dir.y / dir.x) * Mathf.Rad2Deg >= -45 && Mathf.Atan(dir.y / dir.x) * Mathf.Rad2Deg <= 45 && dir.x < 0)
            updownrecoil = 1;
        else
            updownrecoil = 2;


    }

    void Start()
    {
        rigid = this.GetComponent<Rigidbody>();
        //        transform.GetChild(1).transform.Rotate(0f, -90f, 0f);
        LastDirection = new Quaternion(0f, 90f, 0f, 1f);
        float posY = 1 * Mathf.Sin(recoil * Mathf.Deg2Rad);
        float posX = 1 * Mathf.Cos(recoil * Mathf.Deg2Rad);
        left = new Vector3(-posX, -posY, 0f).normalized * recoilIntensity;//-1,0,0 right
        right = new Vector3(posX, posY, 0).normalized * recoilIntensity;//1,0,0 left
    }



    void FixedUpdate()
    {
        rigid.position = new Vector3
        (
            Mathf.Clamp(rigid.position.x, boundary1stick.xMin, boundary1stick.xMax),
            Mathf.Clamp(rigid.position.y, boundary1stick.yMin, boundary1stick.yMax),
            Mathf.Clamp(rigid.position.z, boundary1stick.zMin, boundary1stick.zMax)
        );
       // Vector3 pos = rigid.position;

     

      //  Vector3 direction = Vector3.zero;
      //  direction.x = 1;
       // direction.y = 1;
       
        Quaternion rotation = Quaternion.AngleAxis(Aimtest(), new Vector3(0f, 0f, -1f));
        

        recoiltest(firepoint.transform.position - gameObject.transform.position);
      //  if (direction.magnitude >= 0.5)
        //{
            transform.GetChild(activeTurret).rotation = rotation;
            LastDirection = rotation;
        //}
      //  else
     //   {
       //     transform.GetChild(activeTurret).rotation = LastDirection;
     //   }

        MovementSet();

        if (Movespeed != 0)
        {
            MoveAnim.Play("body Animation");
        }

        testbuff();
        if (buff_frozen)//
        {
            gameObject.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = ice;
            buff = 0.5f;
        }
        else
        {
            gameObject.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = normal;
            buff = 1f;
        }

        rigid.velocity = new Vector3(buff * Accelrate * Movespeed, rigid.velocity.y, 0f);
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
                    bulletMove newBullet1 = Instantiate(bullet, firepoint.position, firepoint.rotation) as bulletMove;
                    bulletMove newBullet2 = Instantiate(bullet, firepoint.position, firepoint.rotation) as bulletMove;

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
                    //rigid.AddForce(1.5f * recoil, ForceMode.Impulse);
                    if (updownrecoil == 0)
                        rigid.AddForce(1.5f * left, ForceMode.Impulse);
                    else if (updownrecoil == 1)
                        rigid.AddForce(1.5f * right, ForceMode.Impulse);
                    audioS.pitch = Random.Range(1f, 5f);
                    anim.Play("Double gun Animation");
                }
                else
                {

                    if (isMissile)
                    {
                        special -= 1;
                        MissileMove newMissile = Instantiate(missile, firepoint.position, firepoint.rotation) as MissileMove;
                        newMissile.gameObject.SetActive(true);
                        //CameraShaker.Instance.ShakeOnce(2f, 4f, 0f, 1.5f);
                        anim.Play("Missile Launcher Animation");
                    }
                    else
                    {
                        bulletMove newBullet = Instantiate(bullet, firepoint.position, firepoint.rotation) as bulletMove;
                        newBullet.gameObject.SetActive(true);
                        newBullet.bulletSpeed = bulletSpeed;
                        if (isBig)
                        {
                            audioSB.pitch = Random.Range(0.2f, 0.3f);
                            audioSB.volume = 0.5f;
                            special -= 1;
                            newBullet.transform.localScale = new Vector3(1f, 1f, 1f);
                            Animator a = newBullet.GetComponent<Animator>();
                            //                           ParticleSystem p = newBullet.GetComponent<ParticleSystem>();
                            a.enabled = false;
                            newBullet.SendMessage("SetBig", true);
                            //CameraShaker.Instance.ShakeOnce(2.5f, 4f, 0f, 3f);
                            if (updownrecoil == 0)
                                rigid.AddForce(2 * left, ForceMode.Impulse);
                            else if (updownrecoil == 1)
                                rigid.AddForce(2 * right, ForceMode.Impulse);
                        }
                        else if (isFrozen)//
                        {
                            special -= 1;
                            newBullet.SendMessage("SetFrozen", true);
                            newBullet.transform.GetChild(0).gameObject.SetActive(true);
                            newBullet.GetComponent<ParticleSystemRenderer>().material = ice;
                            //CameraShaker.Instance.ShakeOnce(1.25f, 4f, 0f, 1.5f);
                            if (updownrecoil == 0)
                                rigid.AddForce(left, ForceMode.Impulse);
                            else if (updownrecoil == 1)
                                rigid.AddForce(right, ForceMode.Impulse);
                            audioS.pitch = Random.Range(1f, 5f);
                        }
                        else
                        {
                            //CameraShaker.Instance.ShakeOnce(1.25f, 4f, 0f, 1.5f);
                            if (updownrecoil == 0)
                                rigid.AddForce(left, ForceMode.Impulse);
                            else if (updownrecoil == 1)
                                rigid.AddForce(right, ForceMode.Impulse);
                            audioS.pitch = Random.Range(1f, 5f);
                        }
                        anim.Play("Gun Animation");
                    }
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
