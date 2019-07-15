using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;


public class bullet_move_4_5 : MonoBehaviour
{
    public float bulletIniForce;
    public GameObject comeFrom;
    public bool isMulti;
    public bool isBig;
    public bool isFrozen;//
    Rigidbody rigid;
    GameObject[] sparks;
    GameObject waterSplatter;
    GameObject[] explosion;
    GameObject[] delay;
    GameObject[] hit;

    public AudioSource expSound;
    public AudioSource hitSound;
    public AudioSource waterSound;
    RaycastHit hits;
    Ray ray;

    private float gravity = 9.8f;
   

    private bool damagded = false;

    private void Awake()
    {
        sparks = GameObject.FindGameObjectsWithTag("sparks");
    }
    void Start()
    {
        explosion = GameObject.FindGameObjectsWithTag("explosion");
        delay = GameObject.FindGameObjectsWithTag("delay");
        transform.Rotate(0f, 90f, 90f);
        rigid = GetComponent<Rigidbody>();
        rigid.AddRelativeForce(Vector3.right * bulletIniForce, ForceMode.Impulse);
        // angle1 = transform.rotation;
        // angle=transform.rotation.eulerAngles.x;
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
        //transform.Translate(Vector3.right* bulletSpeed * Time.deltaTime);
        //if (reverse)
        //transform.rotation = Quaternion.Euler(180 - angle, 90, 90);
        //else
        //transform.rotation = angle1;
        if (comeFrom.gameObject.name == "Player1")
            rigid.AddForce(Vector3.right * gravity, ForceMode.Acceleration);
        else rigid.AddForce(Vector3.right * -gravity, ForceMode.Acceleration);
        ray = new Ray(gameObject.transform.position, rigid.velocity);

        if (Physics.Raycast(ray, out hits, 0.2f,-1))
        {  if(hits.collider.gameObject.tag=="bricks'")
                rigid.velocity = Vector3.Reflect(rigid.velocity * 0.5f, hits.normal);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        float tempPosX = 0f;
        float tempVelX = 0f;
        if (other.gameObject.tag == "Portal")
        {
            if(other.gameObject.name == "PortalBlue")
            {
                tempPosX = gameObject.transform.position.x + 0.5f;
                tempVelX = -Mathf.Abs(rigid.velocity.x);
            }
            else
            {
                tempPosX = gameObject.transform.position.x - 0.5f;
                tempVelX = Mathf.Abs(rigid.velocity.x);
            }
            gameObject.transform.position = new Vector3(-tempPosX, -gameObject.transform.position.y - 2.0f, gameObject.transform.position.z);
            rigid.velocity = new Vector3(tempVelX, -rigid.velocity.y, rigid.velocity.z);
        }
    }
    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "wall")
        {
            hitSound.pitch = 0.1f * 1.05946f * Random.Range(8, 15);
            //0.8-1.5 as normal, 0.5-0.8 as big, need more modification
            hitSound.Play();
            GameObject newSparks = Instantiate(sparks[0], transform.position, transform.rotation) as GameObject;
            if (isBig)
            {
                newSparks.transform.localScale = new Vector3(2f, 2f, 2f);
                CameraShaker.Instance.ShakeOnce(2f, 4f, 0f, 3f);
            }
            else
            {
                CameraShaker.Instance.ShakeOnce(1.25f, 4f, 0f, 1.0f);
            }
            if (comeFrom.activeSelf)
            {
                comeFrom.SendMessage("SetAmmo", isMulti ? 0.5f : 1f);
            }
            Destroy(gameObject);
            Destroy(newSparks, 0.5f);
        }
        else if (other.gameObject.tag == "Player")
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

