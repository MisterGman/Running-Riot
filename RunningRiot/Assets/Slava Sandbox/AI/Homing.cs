using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Homing : MonoBehaviour {
    public float damage = 10f;
    public int obsticleLayerMask = 10;
    public Transform target;
    public float speed = 5f;
    private Rigidbody rig;
    public float rotateSpeed = 5f;
    private Vector3 direction;
    private Vector3 targetLastPosition;
    private bool lost;
    private float firstPosition;
    // Use this for initialization
    void Start () {
        
        target = GameObject.FindGameObjectWithTag("Player").transform;
        firstPosition = target.position.x - transform.position.x;
        rig = GetComponent<Rigidbody>();
        Destroy(gameObject, 5f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if(firstPosition > 0)
        {
            if(target.position.x - transform.position.x < 0)
            {
                lost = true;
            }
        }
        if (firstPosition < 0)
        {
            if (target.position.x - transform.position.x > 0)
            {
                lost = true;
            }
        }
        transform.position = new Vector3(transform.position.x,transform.position.y,0);
        if (ifSeeTarget()&& !lost)
        {
            direction = target.position - rig.position;
            direction.Normalize();
        }
        Vector3 rotateAmount = Vector3.Cross(direction, transform.up);
        rig.angularVelocity = -rotateAmount * rotateSpeed;
        rig.velocity = transform.up * speed;
	}
    bool ifSeeTarget()
    {
        if (!Physics2D.Linecast(transform.position, target.position, LayerMask.GetMask("Obsticle")))
        {
            return true;
        }
        lost = true;
        return false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !other.GetComponent<Player>().invincible)
        {
            Destroy(gameObject);
            target.SendMessage("RecieveDamageFromEnemy",damage);
        }
        else if (other.gameObject.layer == obsticleLayerMask)
        {
            Destroy(gameObject);
        }
    }
}
