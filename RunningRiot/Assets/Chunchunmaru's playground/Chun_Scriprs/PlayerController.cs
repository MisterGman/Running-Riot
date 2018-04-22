using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerPhysics))]
public class PlayerController : MonoBehaviour {

    public float gravity = 20;
    public float speed = 8;
    public float acceleration = 30;
    public float jumpHeight = 12;

    private float currSpeed;
    private float targSpeed;
    private Vector2 amountToMove;

    private PlayerPhysics playerPhysics;
	// Use this for initialization
	void Start () {
        playerPhysics = GetComponent<PlayerPhysics>();
		
	}
	
	// Update is called once per frame
	void Update () {
        targSpeed = Input.GetAxisRaw("Horizontal") * speed;
        currSpeed = IncrementTowards(currSpeed, targSpeed, acceleration);

        Debug.Log(playerPhysics.grounded);
        if (playerPhysics.grounded)
        {
            amountToMove.y = 0;
           
            if (Input.GetButtonDown("Jump"))
            {
                amountToMove.y = jumpHeight;
            }
        }
        else
        {
            amountToMove.y -= gravity * Time.deltaTime;
        }
        amountToMove.x = currSpeed;
        
        //amountToMove.y = 0;
        playerPhysics.Move(amountToMove * Time.deltaTime);
	}

    private float IncrementTowards(float n, float target, float a)
    {
        if (n == target)
        {
            return n;
        }
        else
        {
            float dir = Mathf.Sign(target - n); // n must be increased/decreased to get closer to target
            n += a * Time.deltaTime * dir;
            return (dir == Mathf.Sign(target - n)) ? n : target; // if n passed target return n, otherwise - target
        }
    }
}
