using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeAtt : MonoBehaviour {

    public int SCount;
    void SetSpe(int count)
    {
        SCount = count;
    }

    void FixedUpdate()
    {
        int SpeCount = 0;
        for (int i = 0; i < 5; i++)
        {
            if (SCount > i)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                SpeCount += 1;
            }
            else
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
