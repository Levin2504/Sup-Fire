using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class reset : MonoBehaviour {

    private Scene scene;


    void Start()
    {
        scene = SceneManager.GetActiveScene();
        Cursor.visible = false;

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Joystick1Button7))
        {
 //           Application.LoadLevel(scene.name);
            SceneManager.LoadScene(scene.name);
        }
    }
}
