using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foodTrigger_4_5 : MonoBehaviour {

    public float cdTime;
    public float countDown;
  

    void Update()
    {
        countDown -= Time.deltaTime;



        if (countDown < 0)
        {
            int childNum;
            countDown = cdTime;
            childNum = 0;

            GameObject randomChild = transform.GetChild(childNum).gameObject;
            GameObject newChild = Instantiate(randomChild, new Vector3(0f, -0.875f, -0.5f), new Quaternion(0f, 0f, 0f, 0f)) as GameObject;
            newChild.SetActive(true);
        }
    }
}
