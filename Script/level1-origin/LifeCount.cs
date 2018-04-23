using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCount : MonoBehaviour {

    public int LCount;
    public Animator twink;

    void SetLife(int count)
    {
        LCount = count;
    }

    void FixedUpdate()
    {
        int lifeCount = 0;
        for (int i = 0; i < 10; i++)
        {
            if (LCount > i)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                lifeCount += 1;
            }
            else
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        if (lifeCount == 1) {
            twink.Play("lifeTwink Animation");
        }

    }
}
