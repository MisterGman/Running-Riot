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
    public bool firedFly;
    public GameObject hookedObj;

    public Collider feets;
    private Player player;
    private Vector3 direction;

    public GameObject myPrefab;

    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        feets.enabled = false;
        hook.SetActive(false);
    }

    public float maxDistance;
    private float currentDistance;

    // Update is called once per frame
    void Update () {
        if (hook.Equals(null))
        {
            hook = (GameObject)Instantiate(myPrefab);
        }
        if (Input.GetKeyDown("4") && fired == false && player.wallVzhuh == false)
        {
            fired = true;
            hook.SetActive(true);
        }

        if (fired == true && hooked == false)
        {
            if(firedFly == false)
            {
                if (player.directionalInput.y > 0 && player.directionalInput.x > 0)
                {
                    firedFly = true;
                   // Debug.Log("Up-right");
                    direction = new Vector3(1, 1, 0);
                }
                else if (player.directionalInput.y > 0 && player.directionalInput.x == 0)
                {
                    firedFly = true;
                    //Debug.Log("Up");
                    direction = new Vector3(0, 1, 0);
                }
                else if (player.directionalInput.y > 0 && player.directionalInput.x < 0)
                {
                    firedFly = true;
                   // Debug.Log("Up-left");
                    direction = new Vector3(1, 1, 0);
                }
                else if (player.directionalInput.y < 0  && player.velocity.y != 0)
                {
                    firedFly = true;
                    //Debug.Log("Down");
                    //if(player.directionalInput.x > 0 || player.directionalInput.x < 0)
                    //{
                    //    direction = new Vector3(-1, -1, 0);
                    //} 
                    //else
                    //{
                        direction = new Vector3(0, -1, 0);
                    //}
                    
                    feets.enabled = true;
                    feets.GetComponent<MeshRenderer>().enabled = true;
                } 
                else
                {
                    firedFly = true;
                   // Debug.Log("Forward");
                    direction = new Vector3(1, 0, 0);
                    feets.enabled = true;
                    feets.GetComponent<MeshRenderer>().enabled = true;

                }
            } else
            {
                player.velocity.y = player.velocity.y / 1.5f;
                player.velocity.x = player.velocity.x / 1.5f;
                hook.transform.Translate(direction * Time.deltaTime * hookTravelSpeed);
            }
          
            
            //Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition).x + " sdadadad " + Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            currentDistance = Vector3.Distance(transform.position, hook.transform.position);
            if (currentDistance >= maxDistance)
            {
                ReturnHook();

            }
        }


            if(hooked == true)
            {
            float distanceToHook = Vector3.Distance(transform.position, hook.transform.position);

            if (distanceToHook < 1.5f || player.wallVzhuh)
            {
                ReturnHook();
            }
            firedFly = false;
                hook.transform.parent = hookedObj.transform;
                transform.position = Vector3.MoveTowards(transform.position, hook.transform.position, playerTravelSpeed * Time.deltaTime);

            }
            else
            {
                hook.transform.parent = hookHolder.transform;
            }
        
		
	}

    public void ReturnHook()
    {
        hook.transform.rotation = hookHolder.transform.rotation;
        hook.transform.position = hookHolder.transform.position;
        hook.SetActive(false);
        fired = false;
        firedFly = false;
        hooked = false;
        feets.enabled = false;
        feets.GetComponent<MeshRenderer>().enabled = false;

    }


}
