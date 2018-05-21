using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlock : MonoBehaviour {
    [SerializeField]
    private GameObject requeredEnemy;
    [SerializeField]
    private GameObject[] wallsToBlock;
    [SerializeField]
    private bool visibleAtBeggining;
	// Use this for initialization
	void Start () {
        foreach (GameObject wall in wallsToBlock)
        {
            wall.SetActive(visibleAtBeggining);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (requeredEnemy == null)
        {
            foreach (GameObject wall in wallsToBlock)
            {
                Destroy(wall);
            }
        }
	}
    private void OnTriggerEnter(Collider other)
    {
        if (wallsToBlock.Length < 1) return;
        if (other.tag == "Player")
        {
            foreach (GameObject wall in wallsToBlock)
            {
                wall.SetActive(true);
            }
        }
    }
}
