using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingTest : MonoBehaviour {

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "IcePlatform")
        {
            gameObject.transform.parent.SendMessage("SetLife", -5);
            gameObject.transform.parent.SendMessage("Falling");
        }
    }
}
