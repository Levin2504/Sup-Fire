using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class delay : MonoBehaviour {

    IEnumerator DelayTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        Time.timeScale = 1f;

    }

    

    private void Awake()
    {
        Time.timeScale = 0.1f;
        StartCoroutine(DelayTime(0.01f));
    }
}
