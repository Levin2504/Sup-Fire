using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCount : MonoBehaviour {

    public int ACount;

    void SetAmmo(int count)
    {
        ACount = count;
    }

	void FixedUpdate () {
        for (int i = 0; i < 10; i++)
        {
            if(ACount > i)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
	}
}
