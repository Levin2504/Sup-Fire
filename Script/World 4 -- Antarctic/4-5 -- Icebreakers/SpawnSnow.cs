using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSnow : MonoBehaviour {

    public GameObject IceCylinder;
    //private float StartPointX = -10f;
    private float StartPointY = -6.375f;
	
	void Start ()
    {
        SpawnFloor();
	}
	
	void SpawnFloor()
    {
        while (StartPointY <= 4.625f)
        {
            //while (StartPointX <= 10f)
            for(int i = -10; i < 11; i += 2)
            {
                if (i == 0) continue;
                Vector3 pos = new Vector3(i, StartPointY, -0.5f);
                GameObject FloorPart = Instantiate(IceCylinder, pos, Quaternion.Euler(90, 0, 0)) as GameObject;
                FloorPart.gameObject.SetActive(true);
                //StartPointX++;
            }
            StartPointY += 0.5f;
            if (StartPointY >= 4.625f) break;
            for (int i = -9; i < 11; i += 2)
            {
                Vector3 pos = new Vector3(i, StartPointY, -0.5f);
                GameObject FloorPart = Instantiate(IceCylinder, pos, Quaternion.Euler(90, 0, 0)) as GameObject;
                FloorPart.gameObject.SetActive(true);
                //StartPointX++;
            }
            StartPointY += 0.5f;
        }
    }
}
