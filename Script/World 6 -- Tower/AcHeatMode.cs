using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcHeatMode : MonoBehaviour {
    public Queue<float> timetable= new Queue<float>();
    public Material Overheat;
    public Material normal;
    public bool IsOverheat;
    private float Overheadtime;
    public float punishTime;
	// Use this for initialization
	void Start () {
        ResetQueue();
        IsOverheat = false;
    }
	

    void Add(float FireTime)
    {
        Debug.Log(FireTime);
        float firstfire = timetable.Dequeue();
        timetable.Enqueue(FireTime);
        timetable.TrimExcess();
        if (Mathf.Abs(firstfire - FireTime) <= 1.2 && (firstfire != 0))
        {
            Debug.Log("overheat!");
            IsOverheat = true;
            Overheadtime = Time.time;
        }

    }

     void ResetQueue()
    {
        Debug.Log("reset");
        timetable.Clear();
        timetable.Enqueue(0);
        timetable.Enqueue(0);
        timetable.Enqueue(0);
    }

    private void FixedUpdate()
    {
        if (IsOverheat)
        {
            transform.GetChild(1).GetChild(1).GetComponent<MeshRenderer>().material = Overheat;
            gameObject.SendMessage("StopFire");
            if (Time.time - Overheadtime > punishTime)
            {
                IsOverheat = false;
                gameObject.SendMessage("ResetAmmo");
            }
        }
        else
        {
            transform.GetChild(1).GetChild(1).GetComponent<MeshRenderer>().material = normal;

        }
    }


}
