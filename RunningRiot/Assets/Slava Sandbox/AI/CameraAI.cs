using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAI : MonoBehaviour
{
    [SerializeField]
    private GameObject viewVisual;
    [SerializeField]
    private float leftBorder;
    [SerializeField]
    private float rightBorder;
    [SerializeField]
    private float wait = 1f;
    private bool rotateleft;
    private enum Direction { left, right, zero, player };
    private Direction direction;
    private FieldOfView fieldCs;
    private Transform player;
    [SerializeField]
    private GameObject enemies;
    // Use this for initialization
    void Start()
    {
        fieldCs = GetComponentInChildren<FieldOfView>();
        direction = Direction.right;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    // Update is called once per frame
    void Update()
    {
        switch (direction)
        {
            case Direction.left:
                transform.Rotate(Vector3.forward * Time.deltaTime * 20);
                if (transform.eulerAngles.z > leftBorder)
                {
                    StartCoroutine(Wait(Direction.right));
                    direction = Direction.zero;
                }
                break;
            case Direction.right:
                transform.Rotate(-Vector3.forward * Time.deltaTime * 20);
                if (transform.eulerAngles.z < rightBorder)
                {
                    StartCoroutine(Wait(Direction.left));
                    direction = Direction.zero;
                }
                break;
            case Direction.zero:
                
                break;
            case Direction.player:
                transform.LookAt(player);
                break;
        }
    }
    IEnumerator Wait(Direction dir)
    {
        yield return new WaitForSeconds(wait);
        direction = dir;
    }
    void Alert()
    {
        fieldCs.deleteMesh();
        fieldCs.enabled = false;
        direction = Direction.player;
        GetComponentInChildren<Light>().enabled = true;
        enemies.SetActive(true);
    }

}
