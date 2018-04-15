using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : MonoBehaviour
{
    private float myWidth;
    [HideInInspector]
    public enum State { Patrol, Chase, Hit, Stand };
    public State currState = State.Stand;
    private Transform player;
    //private NavMeshAgent agent;
  //  private Animator anim;
    public bool facingRight;
    public float damage;
   // private WeaponHit weapon;
    private bool lockHit = false;
    // Use this for initialization
    void Start()
    {
      //  weapon = GetComponentInChildren<WeaponHit>();
      //  anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //agent = GetComponent<NavMeshAgent>();
        if (transform.rotation.y < 0)
        {
            facingRight = false;
        }
        else
        {
            facingRight = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        StayFixed();
        States();
        Debug.DrawLine(transform.position, transform.position + transform.forward);

    }
    void States()
    {
        switch (currState)
        {
            case State.Stand:
                //anim.SetTrigger("Stand");
                break;
            case State.Patrol:
             //   anim.SetTrigger("Run");
                //agent.stoppingDistance = 1f;
                if (CheckReachablePoint(transform.position + transform.forward))
                {
                    //agent.SetDestination(transform.position + transform.forward);
                }
                else
                {
                    facingRight = !facingRight;
                }
                if (CheckReachablePoint(player.position))
                {
                    currState = State.Chase;
                }
                break;
            case State.Chase:
               // anim.SetTrigger("Run");
                //agent.stoppingDistance = 1f;
                if (player.position.x < transform.position.x)
                {
                    facingRight = false;
                }
                else
                {
                    facingRight = true;
                }
                //agent.SetDestination(player.position);
                if (Vector3.Distance(transform.position, player.position) < 2)
                {
                    currState = State.Hit;
                }

                if (!CheckReachablePoint(player.position))
                {
                    currState = State.Patrol;
                }
                break;
            case State.Hit:
                if (!lockHit)
                {
                    lockHit = !lockHit;
                    //agent.isStopped = true;
                    StartCoroutine(HitPlayer());
                }
                if (Vector3.Distance(transform.position, player.position) > 2)
                {
                    currState = State.Chase;
                }
                break;
        }
    }
    bool CheckReachablePoint(Vector3 destination)
    {
       // agent.
        return NavMesh.CalculatePath(transform.position, destination, NavMesh.AllAreas, new NavMeshPath());
    }
    void StayFixed()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        if (!lockHit)
        {
            if (facingRight)
            {
                transform.eulerAngles = new Vector3(transform.rotation.x, 90, transform.rotation.z);
            }
            else
            {
                transform.eulerAngles = new Vector3(transform.rotation.x, -90, transform.rotation.z);
            }
        }

    }
    IEnumerator HitPlayer()
    {

       // anim.SetTrigger("Attack");
        yield return new WaitForSeconds(2f);
        lockHit = !lockHit;
        //agent.isStopped = false;
    }
    void TurnOnHitbox()
    {
       // weapon.GetComponent<Collider>().enabled = true;
        Debug.Log("------------EnabledCollider");
    }
    void TurnOffHitbox()
    {
        //weapon.GetComponent<Collider>().enabled = false;
        Debug.Log("------------DisanabledCollider");
    }
    void Damage()
    {
        player.SendMessage("RecieveDamage", damage);
       // weapon.GetComponent<Collider>().enabled = false;
    }
}
