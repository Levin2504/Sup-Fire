using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour {
    public GameObject big;
    public GameObject multi;
    public GameObject missile;
    public GameObject frozen;
    public int itself;  
	// Use this for initialization
	void Start () {
        itself = Random.Range(0, 8);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet"||other.gameObject.tag=="Missile")
        {
            switch (itself)
            {
                case 0:
                   GameObject powerup0= Instantiate(big, transform.position,Quaternion.identity)as GameObject;
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
