using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {

    TextMesh text;
    int left;
    int right;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Score");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Start () {
        text = this.GetComponent<TextMesh>();
        left = 0;
        right = 0;
	}
	
    void leftPlus()
    {
            left += 1;
    }

    void rightPlus()
    {
            right += 1;
    }

	void FixedUpdate () {
        text.text = left.ToString() + "  :  " + right.ToString();
	}
}
