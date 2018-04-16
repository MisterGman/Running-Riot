using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraplingHook : MonoBehaviour {

    public GameObject hook;
    public GameObject hookHolder;

    public float hookTravelSpeed;
    public float playerTravelSpeed;

    public static bool fired;
    public  bool hooked;
    public GameObject hookedObj;

    public float maxDistance;
    private float currentDistance;
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButton(0) && fired == false)
            fired = true;

        if(fired == true && hooked == false)
        {
            hook.transform.Translate(new Vector3(1,1,0) * Time.deltaTime * hookTravelSpeed);
            currentDistance = Vector3.Distance(transform.position, hook.transform.position);

            if(currentDistance >= maxDistance)
            {
                ReturnHook();
            }

            if(hooked == true)
            {
                hook.transform.parent = hookedObj.transform;
                transform.position = Vector3.MoveTowards(transform.position, hook.transform.position, playerTravelSpeed * Time.deltaTime);
                float distanceToHook = Vector3.Distance(transform.position, hook.transform.position);

                this.GetComponent<Player>().timeToJumpApex = 10;
                 if (distanceToHook < 1)
                {
                    ReturnHook();
                }
            }
            else
            {
                hook.transform.parent = hookHolder.transform;
                this.GetComponent<Player>().timeToJumpApex = .4f;
            }
        }
		
	}

    void ReturnHook()
    {
        hook.transform.rotation = hookHolder.transform.rotation;
        hook.transform.position = hookHolder.transform.position;
        fired = false;
        hooked = false;
    }


}
