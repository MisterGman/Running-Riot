﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider))]
public class PlayerPhysics : MonoBehaviour {

    public LayerMask collisionMask;

    private BoxCollider collider;
    private Vector3 s; //Size
    private Vector3 c; //Center

    private float skin = .005f;

    [HideInInspector]
    public bool grounded;

    Ray ray;
    RaycastHit hit;

    void Start()
    {
        collider = GetComponent<BoxCollider>();
        s = collider.size;
        c = collider.center;
    }
    public void Move(Vector2 moveAmount)
    {
        float deltaY = moveAmount.y;
        float deltaX = moveAmount.x;
        Vector2 p = transform.position;

        

        for(int i = 0; i<3; i++)
        {
            float dir = Mathf.Sign(deltaY);
            float x = (p.x + c.x - s.x / 2) + s.x / 2 * i; // Left, center, right colliders
            float y = p.y + c.y + s.y / 2 * dir; // Bottom of collider

            ray = new Ray(new Vector2(x, y), new Vector2(0, dir));
            Debug.DrawRay(ray.origin, ray.direction);
            if (Physics.Raycast(ray, out hit, Mathf.Abs(deltaY) + skin, collisionMask))
            {
                float dst = Vector3.Distance(ray.origin, hit.point);
                
                if(dst > skin)
                {
                    deltaY = dst * dir - skin * dir;
                    Debug.Log(deltaY);
                }
                else
                {
                    deltaY = 0;
                    Debug.Log(0);
                }
                grounded = true;
                break;
            }
            else
            {
                grounded = false;
            }
        }

        Vector2 finalTransform = new Vector2(deltaX, deltaY);
        transform.Translate(moveAmount);
    }
}