using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearOnKill : MonoBehaviour {
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private GameObject appear;
	// Use this for initialization
	void Start () {
        appear.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (appear.activeSelf) return;
        if (enemy == null)
        {
            appear.SetActive(true);
        }
	}
}
