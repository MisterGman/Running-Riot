using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour {

    void OnTriggerEnter(Collider ChangeScene) // can be Collider HardDick if you want.. I'm not judging you
    {
        if (ChangeScene.gameObject.tag == ("Player"))
        {
            Debug.Log("WHY??");
            Application.LoadLevel(1); //1 is the build order it could be 1065 for you if you have that many scenes
        }
    }
}
