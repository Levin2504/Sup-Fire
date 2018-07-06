using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class controller_P2_3_1 : MonoBehaviour {

    public Boundary2Stick boundary2stick;

    public float Accelrate;
    public float MaxSpeed;
    public bool isFireing;
    public bulletmove_3_1 bullet;
    public Missilemove_3_1 missile;
    public Transform firepoint;
    public float bulletSpeed;
    public AudioSource audioS;
    public AudioSource audioSB;
    public AudioSource audioR;
    public AudioSource audioM;

    public int special_big;
    public int special_multi;
    public int special_missile;
    public int special_frozen;

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
    public bool heatmode;
    private Quaternion LastDirection;
    private bool isSpecial = false;
    void flagcheck()
    {
        if (special_big == 0)
            isBig = false;
        if (special_multi == 0)
            isMulti = false;
        if (special_missile == 0)
            isMissile = false;
        if (special_frozen == 0)
            isFrozen = false;
        if (special_big == 0 && special_multi == 0 && special_missile == 0)
        {
            gameObject.transform.GetChild(1).transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            this.transform.GetChild(1).GetChild(1).GetChild(1).gameObject.SetActive(false);
            UseTurret1();

        }
    }
    void SetBig()
    {
        isBig = true;
        isMulti = false;
        isMissile = false;
        isFrozen = true;
        audioR.Play();
        special_big = 1;
        special_frozen = 1;
        special_missile = 0;
        special_multi = 0;
        UseTurret1();
        gameObject.transform.GetChild(1).transform.localScale = new Vector3(0.5f, 0.5f, 0.3f);

        this.transform.GetChild(1).GetChild(1).GetChild(1).gameObject.SetActive(false);
    }

    void SetMulti()
    {
        isMulti = true;
        isFrozen = true;
        isBig = false;
        isMissile = false;
        audioR.Play();
        special_multi = 3;
        special_frozen = 3;
        special_big = 0;
        special_missile = 0;
        gameObject.transform.GetChild(1).transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        UseTurret2();
    }

    /*void SetFrozen()//
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

    }*/

    void SetMissile()
    {
        isMissile = true;
        isFrozen = true;
        isBig = false;
        isMulti = false;
        special_big = 0;
        special_multi = 0;
        special_frozen = 3;
        audioR.Play();
        special_missile = 3;
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
        //transform.GetChild(1).transform.Rotate(0f, 90f, 0f);
        LastDirection = new Quaternion(0f, 90f, 0f, 1f);
        float posY = 1 * Mathf.Sin(recoil * Mathf.Deg2Rad);
        float posX = 1 * Mathf.Cos(recoil * Mathf.Deg2Rad);
        left = new Vector3(-posX, -posY, 0f).normalized * recoilIntensity;//-1,0,0 right
        right = new Vector3(posX, posY, 0).normalized * recoilIntensity;//1,0,0 left


    }


    void FixedUpdate()
    {
        flagcheck();
        rigid.position = new Vector3
        (
            Mathf.Clamp(rigid.position.x, boundary2stick.xMin, boundary2stick.xMax),
            Mathf.Clamp(rigid.position.y, boundary2stick.yMin, boundary2stick.yMax),
            Mathf.Clamp(rigid.position.z, boundary2stick.zMin, boundary2stick.zMax)
        );
        Vector3 pos = rigid.position;

        float v_dir = Input.GetAxis("J-V-Direct");
        float h_dir = Input.GetAxis("J-H-Direct");

        Vector3 direction = Vector3.zero;

        direction.x = -h_dir;
        direction.y = v_dir;

        angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, new Vector3(0f, 0f, -1f));
        recoiltest(firepoint.transform.position - gameObject.transform.position);
        if (direction.magnitude >= 0.5)
        {
            transform.GetChild(activeTurret).rotation = rotation;
            LastDirection = rotation;
        }
        else
        {
            transform.GetChild(activeTurret).rotation = LastDirection;
        }

        float h_axis = Input.GetAxis("J-Horizontal");

        if (h_axis != 0)
        {
            MoveAnim.Play("body Animation");
        }
        testbuff();//
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

        rigid.velocity = new Vector3(buff * Accelrate * h_axis, rigid.velocity.y, 0f);//

        if (Input.GetAxis("Fire1") < 0 && remainAmmo >= 1) //fire
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
                if (heatmode)
                    gameObject.SendMessage("Add", Time.time);
                if (isMulti)
                {
                    special_multi -= 1;
                    bulletmove_3_1 newBullet1 = Instantiate(bullet, firepoint.position, firepoint.rotation) as bulletmove_3_1;
                    bulletmove_3_1 newBullet2 = Instantiate(bullet, firepoint.position, firepoint.rotation) as bulletmove_3_1;

                    newBullet1.gameObject.SetActive(true);
                    newBullet1.transform.Translate(new Vector3(0.2f, 0f, 0f));
                    newBullet1.transform.Rotate(new Vector3(0f, 0f, -5f));
                    newBullet1.bulletSpeed = bulletSpeed;
                    newBullet1.SendMessage("SetMulti", true);
                    if (isBig)
                    {
                        special_big -= 1;
                        newBullet1.SendMessage("setBig", true);
                        newBullet1.transform.localScale = new Vector3(1f, 1f, 1f);
                        Animator a = newBullet1.GetComponent<Animator>();
                        a.enabled = false;

                    }
                    if (isFrozen)
                    {
                        special_frozen -= 1;
                        newBullet1.SendMessage("SetFrozen", true);
                        newBullet1.transform.GetChild(0).gameObject.SetActive(true);
                        newBullet1.GetComponent<ParticleSystemRenderer>().material = ice;
                    }



                    newBullet2.gameObject.SetActive(true);
                    newBullet2.transform.Translate(new Vector3(-0.2f, 0f, 0f));
                    newBullet2.transform.Rotate(new Vector3(0f, 0f, 5f));
                    newBullet2.bulletSpeed = bulletSpeed;
                    if (isFrozen)
                    {
                        newBullet2.SendMessage("SetFrozen", true);
                        newBullet2.transform.GetChild(0).gameObject.SetActive(true);
                        newBullet2.GetComponent<ParticleSystemRenderer>().material = ice;
                    }
                    if (isBig)
                    {
                        newBullet2.SendMessage("setBig", true);
                        newBullet2.transform.localScale = new Vector3(1f, 1f, 1f);
                        Animator a = newBullet2.GetComponent<Animator>();
                        a.enabled = false;

                    }
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
                        special_missile -= 1;
                        Missilemove_3_1 newMissile = Instantiate(missile, firepoint.position, firepoint.rotation) as Missilemove_3_1;
                        newMissile.gameObject.SetActive(true);
                        //CameraShaker.Instance.ShakeOnce(2f, 4f, 0f, 1.5f);
                        anim.Play("Missile Launcher Animation");


                        if (isFrozen)
                        {
                            newMissile.SendMessage("SetFrozen", true);
                            special_frozen -= 1;

                        }
                    }
                    else
                    {
                        bulletmove_3_1 newBullet = Instantiate(bullet, firepoint.position, firepoint.rotation) as bulletmove_3_1;
                        newBullet.gameObject.SetActive(true);
                        newBullet.bulletSpeed = bulletSpeed;
                        if (isBig)
                        {
                            newBullet.SendMessage("SetBig", true);
                            audioSB.pitch = Random.Range(0.2f, 0.3f);
                            audioSB.volume = 0.5f;
                            special_big -= 1;
                            newBullet.transform.localScale = new Vector3(1f, 1f, 1f);
                            Animator a = newBullet.GetComponent<Animator>();
                            //                           ParticleSystem p = newBullet.GetComponent<ParticleSystem>();
                            a.enabled = false;
                            if (updownrecoil == 0)
                                rigid.AddForce(2 * left, ForceMode.Impulse);
                            else if (updownrecoil == 1)
                                rigid.AddForce(2 * right, ForceMode.Impulse);
                        }
                        if (isFrozen)
                        {
                            newBullet.SendMessage("SetFrozen", true);
                            special_frozen -= 1;
                            newBullet.GetComponent<ParticleSystemRenderer>().material = ice;
                            newBullet.transform.GetChild(0).gameObject.SetActive(true);
                        }
                        else if (isBig == false)
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
                score[0].SendMessage("leftPlus");
                SetScore = !SetScore;
            }
        }
       

        SpeCount.SendMessage("SetSpe", Mathf.Max(special_big, Mathf.Max(special_frozen, Mathf.Max(special_missile), special_multi)));
    }

    IEnumerator DelayTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        Time.timeScale = 1f;
        Application.targetFrameRate = -1;
        gameObject.SetActive(false);
    }
    void CoolMode()
    {
        heatmode = false;

    }
    void HeatMode()
    {
        heatmode = true;
    }
    void StopFire()
    {
        remainAmmo = 0;

    }
    void ResetAmmo()
    {
        remainAmmo = 5;
    }
}
