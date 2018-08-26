using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBreak : MonoBehaviour {

    public float hp;
    private float startHP;
    private GameObject iceSplatter;
    public GameObject big;
    public GameObject multi;
    public GameObject missile;
    public GameObject frozen;
    public int itself;

    void Start ()
    {
        startHP = hp;
        itself = Random.Range(0, 8);
    }
	
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet" || other.gameObject.tag == "Missile")
        {
            hp--;
            if (hp < startHP) gameObject.transform.GetChild(1).gameObject.SetActive(true);
            if (hp <= startHP * 0.5f) gameObject.transform.GetChild(2).gameObject.SetActive(true);
            if (hp <= 0)
            {
                iceSplatter = GameObject.Find("FX_IceSplatter");
                GameObject newSplatters = Instantiate(iceSplatter, transform.position, new Quaternion()) as GameObject;
                ParticleSystem SplattersParticle = newSplatters.GetComponent<ParticleSystem>();
                var main = SplattersParticle.main;
                main.startSize = 0.5f;
                main.startSpeed = 5f;
                Destroy(newSplatters, 1.5f);

                switch (itself)
                {
                    case 0:
                        GameObject powerup0 = Instantiate(big, transform.position, Quaternion.identity) as GameObject;
                        powerup0.SetActive(true);
                        break;
                    case 1:
                        GameObject powerup1 = Instantiate(multi, transform.position, Quaternion.identity) as GameObject;
                        powerup1.SetActive(true);
                        break;
                    case 2:
                        GameObject powerup2 = Instantiate(missile, transform.position, Quaternion.identity) as GameObject;
                        powerup2.SetActive(true);
                        break;
                    case 3:
                        GameObject powerup3 = Instantiate(frozen, transform.position, Quaternion.identity) as GameObject;
                        powerup3.SetActive(true);
                        break;
                    default:
                        break;

                }

                Destroy(gameObject);
            }
        }
    }
}
