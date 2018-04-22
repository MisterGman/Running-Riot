using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Homing : MonoBehaviour {
    public Transform target;
    public float speed = 5f;
    private Rigidbody rig;
    public float rotateSpeed = 5f;

    // Use this for initialization
    void Start () {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rig = GetComponent<Rigidbody>();
        Destroy(gameObject, 5f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 direction = target.position - rig.position;
        direction.Normalize();
        Vector3 rotateAmount = Vector3.Cross(direction, transform.up);
        rig.angularVelocity = -rotateAmount * rotateSpeed;
        rig.velocity = transform.up * speed;
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Destroy(gameObject);
            //target.SendMessage("RecieveDamageFromEnemy",10);
        }
    }
}
