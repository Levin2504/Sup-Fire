using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSnow : MonoBehaviour {

    public IceBreak floe;
    //private float StartPointX = -10f;
    private float StartPointY = -7.5f;
	
	void Start ()
    {
        SpawnFloor();
	}
	
	void SpawnFloor()
    {
        while (StartPointY <= 5.5f)
        {
            //while (StartPointX <= 10f)
            for(int i = -10; i < 11; i++)
            {
                Vector3 pos = new Vector3(i, StartPointY, -0.5f);
                IceBreak FloorPart = Instantiate(floe, pos, transform.rotation) as IceBreak;
                FloorPart.gameObject.SetActive(true);
                //StartPointX++;
            }
            StartPointY++;
        }
    }
}
