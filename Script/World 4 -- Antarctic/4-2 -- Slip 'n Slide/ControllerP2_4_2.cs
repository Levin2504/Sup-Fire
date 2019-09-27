using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
/*
[System.Serializable]
public class Boundary2Stick_2_5
{
    public float xMin, xMax, yMin, yMax, zMin, zMax;
}
*/
public class ControllerP2_4_2 : MonoBehaviour
{
    //public Boundary2Stick_2_5 boundary2stick;

    public float Accelrate;
    public float MaxSpeed;
    public bool isFireing;
    public bulletMove_4_2 bullet;
    public MissileMove_4_2 missile;
    public Transform firepoint;
    public float bulletSpeed;
    public AudioSource audioS;
    public AudioSource audioSB;
    public AudioSource audioR;
    public AudioSource audioM;
    public AudioSource waterSound;

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
    private GameObject waterSplatter;
    private bool SetScore = false;
    private bool hasFall = false;
    private Quaternion LastDirection;
    private bool isSpecial = false;

    void SetBig()
    {
        ResetBarrel();
        isBig = true;
        isMulti = false;
        isMissile = false;
        isFrozen = false;//
        audioR.Play();
        special = 5;
        isSpecial = true;
        UseTurret1();
        gameObject.transform.GetChild(1).transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        this.transform.GetChild(1).GetChild(1).GetChild(1).gameObject.SetActive(false);
    }

    void SetMulti()
    {
        ResetBarrel();
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
        ResetBarrel();
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
        ResetBarrel();
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

    void ResetBarrel()
    {
        if (isMulti) gameObject.transform.GetChild(2).GetChild(1).transform.localScale = new Vector3(1f, 1f, 1f);
        else if (isMissile) gameObject.transform.GetChild(3).GetChild(1).transform.localScale = new Vector3(1f, 1f, 1f);
        else gameObject.transform.GetChild(1).GetChild(1).transform.localScale = new Vector3(1f, 1f, 1f);
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
        //transform.GetChild(1).transform.Rotate(0f, 90f, 0f);
        LastDirection = new Quaternion(0f, 90f, 0f, 1f);
    }


    void FixedUpdate()
    {

        Vector3 pos = rigid.position;

        float v_dir = Input.GetAxis("J-V-Direct");
        float h_dir = Input.GetAxis("J-H-Direct");

        Vector3 direction = Vector3.zero;

        direction.x = -h_dir;
        direction.y = v_dir;

        angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, new Vector3(0f, 0f, -1f));
        //recoil = recoilIntensity * -(firepoint.transform.position - gameObject.transform.position).normalized;
        Vector3 recoilDirection = new Vector3(
            firepoint.transform.position.x - gameObject.transform.position.x, 
            firepoint.transform.position.y - gameObject.transform.position.y, 
            0).normalized;
        recoil = recoilIntensity * -recoilDirection;

        if (direction.magnitude >= 0.5)
        {
            transform.GetChild(activeTurret).rotation = rotation;
            LastDirection = rotation;
        }
        else
        {
            transform.GetChild(activeTurret).rotation = LastDirection;
        }

        h_axis = Input.GetAxis("J-Horizontal");
        v_axis = Input.GetAxis("J-Vertical");

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

        //rigid.velocity = new Vector3(buff * Accelrate * h_axis, buff * Accelrate * v_axis, 0f);//
        Vector3 movement = new Vector3(buff * Accelrate * h_axis, buff * Accelrate * v_axis, 0f);
        rigid.AddForce(movement);

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

                if (isMulti)
                {
                    special -= 1;
                    bulletMove_4_2 newBullet1 = Instantiate(bullet, firepoint.position, firepoint.rotation) as bulletMove_4_2;
                    bulletMove_4_2 newBullet2 = Instantiate(bullet, firepoint.position, firepoint.rotation) as bulletMove_4_2;

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
                        MissileMove_4_2 newMissile = Instantiate(missile, firepoint.position + new Vector3(0f, -0f, 0.2f), firepoint.rotation) as MissileMove_4_2;
                        newMissile.gameObject.SetActive(true);
                        //CameraShaker.Instance.ShakeOnce(2f, 4f, 0f, 1.5f);
                        anim.Play("Missile Launcher Animation");
                    }
                    else
                    {
                        bulletMove_4_2 newBullet = Instantiate(bullet, firepoint.position, firepoint.rotation) as bulletMove_4_2;
                        newBullet.gameObject.SetActive(true);
                        newBullet.bulletSpeed = bulletSpeed;
                        if (isBig)
                        {
                            audioSB.pitch = Random.Range(0.2f, 0.3f);
                            audioSB.volume = 1.0f;
                            special -= 1;
                            newBullet.transform.localScale = new Vector3(1f, 0.1f, 1f);
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
                            rigid.AddForce(recoil, ForceMode.Impulse);
                            audioS.pitch = Random.Range(1f, 5f);
                        }
                        else
                        {
                            //CameraShaker.Instance.ShakeOnce(1.25f, 4f, 0f, 1.5f);
                            rigid.AddForce(recoil, ForceMode.Impulse);
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

    private void Falling()
    {
        if (!hasFall)
        {
            waterSound.Play();
            waterSplatter = GameObject.Find("FX_WaterSplatter");
            GameObject newSplatters = Instantiate(waterSplatter, transform.position, new Quaternion()) as GameObject;
            ParticleSystem SplattersParticle = newSplatters.GetComponent<ParticleSystem>();
            var main = SplattersParticle.main;
            main.startSize = 0.5f;
            main.startSpeed = 5f;
            Destroy(newSplatters, 1.5f);
            hasFall = !hasFall;
        }
    }
}

