using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemy : MonoBehaviour {
    private float myWidth;
    [HideInInspector]
    public enum State { Patrol, Chase, Hit, Stand };
    public State currState = State.Stand;
    private Transform player;
    private NavMeshAgent agent;
    private Animator anim;
    public bool facingRight;
    public float damage = 50f;
    private WeaponHit weapon;
    private bool lockHit = false;
    public float aggroDistance = 5f;
    public GameObject bullet;
    public Transform bulletInstantiatePosition;
    int hp = 5;

    // Use this for initialization
    void Start()
    {
        this.tag = "Enemy";
        weapon = GetComponentInChildren<WeaponHit>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
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
                if (lockHit) currState = State.Hit;
                anim.SetTrigger("Stand");
                agent.isStopped = true;
                if (Vector3.Distance(transform.position, player.position) < aggroDistance)
                {
                    currState = State.Chase;
                    agent.isStopped = false;
                }

                break;
            case State.Patrol:
                anim.SetTrigger("Run");
                agent.stoppingDistance = 0f;
                if (CheckReachablePoint(transform.position + transform.forward))
                {
                    agent.SetDestination(transform.position + transform.forward);
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
                anim.SetTrigger("Run");
                agent.stoppingDistance = 1f;
                if (player.position.x < transform.position.x)
                {
                    facingRight = false;
                }
                else
                {
                    facingRight = true;
                }
                agent.SetDestination(player.position);
                if (Vector3.Distance(transform.position, player.position) < 10)
                {
                    currState = State.Hit;
                }

                if (!CheckReachablePoint(player.position))
                {
                    currState = State.Patrol;
                }
                else if (Vector3.Distance(transform.position, player.position) > aggroDistance)
                {
                    currState = State.Stand;
                }
                break;
            case State.Hit:
                if (player.position.x < transform.position.x)
                {
                    facingRight = false;
                }
                else
                {
                    facingRight = true;
                }
                if (!lockHit)
                {
                    lockHit = !lockHit;
                    agent.isStopped = true;
                    StartCoroutine(HitPlayer());
                }
                if (Vector3.Distance(transform.position, player.position) < 2)
                {
                    Teleport();
                }

                if (Vector3.Distance(transform.position, player.position) > 10)
                {
                    currState = State.Chase;
                }
                break;
        }
    }
    void Teleport()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 10;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, 10, 1))
        {
            finalPosition = hit.position;
        }
        agent.enabled = false;
        transform.position =  finalPosition;
        agent.enabled = true;
    }
    bool CheckReachablePoint(Vector3 destination)
    {
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

        //anim.SetTrigger("Attack");
        Shoot();
        yield return new WaitForSeconds(2f);
        lockHit = !lockHit;
        agent.isStopped = false;
    }
    void Shoot()
    {
        Instantiate(bullet,bulletInstantiatePosition.position,Quaternion.Euler(0,0,-transform.eulerAngles.y));
    }
    void TurnOnHitbox()
    {
        weapon.GetComponent<Collider>().enabled = true;
        Debug.Log("------------EnabledCollider");
    }
    void TurnOffHitbox()
    {
        weapon.GetComponent<Collider>().enabled = false;
        Debug.Log("------------DisanabledCollider");
    }
    void Damage()
    {
        player.SendMessage("RecieveDamageFromEnemy", 25);
        weapon.GetComponent<Collider>().enabled = false;
        Debug.Log("--1241414134----------HIT");
    }
    void RecieveDamageFromPlayer()
    {
        Destroy(this.gameObject);
    }
}
