using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brick : MonoBehaviour {
  
    private Vector3 place;
    public bool foodornot;
    private GameObject target;
 
	// Use this for initialization
	void Start () {
        place = transform.position + new Vector3(0, 1, 0);
	}

    void befoodhoodler(bool set)
    {
        foodornot = set;
       

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision other)
    {

        if (foodornot == true)
        {


                int childNum;

                childNum = Random.Range(0, 4);
                GameObject randomChild = transform.GetChild(childNum).gameObject;
                GameObject newChild = Instantiate(randomChild, place, new Quaternion(0f, 0f, 0f, 0f)) as GameObject;
            bullet_move_l10 bullet = other.gameObject.GetComponent<bullet_move_l10>();
            target = bullet.comeFrom;

            if(childNum==0)
            target.SendMessage("SetBig");
            else if(childNum==1)
                target.SendMessage("SetNulti");
            else if (childNum == 2)
                target.SendMessage("SetMissile");
            else if (childNum == 3)
                target.SendMessage("SetFrozen");

            newChild.SetActive(true);
                foodornot = false;
 
        }
    }

  

}
