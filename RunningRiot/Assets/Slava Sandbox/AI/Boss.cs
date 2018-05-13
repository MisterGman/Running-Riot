using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour {
    public float health = 300;
    public float currentHealth;
    private float myWidth;
    [HideInInspector]
    public enum State { Patrol, Chase, Hit, Stand, Flying };
    public enum Phase { First,Second,Third};
    public State currState = State.Stand;
    public Phase currPhase = Phase.First;
    private Transform player;
    private NavMeshAgent agent;
    private Animator anim;
    public bool facingRight;
    public float damage = 50f;
    private WeaponHit weapon;
    [SerializeField]
    private bool lockHit = false;
    public float aggroDistance = 5f;
    public float aggroHeightDistance = 10f;
    private bool isGrounded = true;
    [SerializeField]
    private string currentTrigger = "";
    private GameObject particleS;
    private bool animationOnce = false;
    public int multiplier = 1;
    int hp = 5;

    // Use this for initialization
    void Start()
    {
        currentHealth = health;
        particleS = GetComponentInChildren<ParticleSystem>().gameObject;
        particleS.SetActive(false);
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
        if(!gotHit)
            IsGrounded();
        AnimationStates();
        SwitchPhases();
        Phases();
        Damaging();
    }
    void Damaging()
    {
        if (Input.GetButtonUp("Fire1"))
        {
          //  RecieveDamageFromPlayer(50f);
        }
    }
    private void SwitchPhases()
    {
        if (currentHealth > health/2) {
            currPhase = Phase.First;
        }else  if (currentHealth < health / 2 && currPhase!=Phase.Third) { 
            currPhase = Phase.Second;
        } 
    }
    bool burnTheGroundOnce = false;
    void Phases()
    {
        switch (currPhase)
        {
            case Phase.First:
                States();
                break;
            case Phase.Second:
                agent.enabled = false;
                if (!burnTheGroundOnce)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + 20, transform.position.z);
                    BurnTheGround();
                    burnTheGroundOnce = true;
                }
                break;
            case Phase.Third:
                agent.enabled = true;
                States();
                break;
        }
    }
    void BurnTheGround()
    {
        GetComponent<SpawnRainFire>().enabled = true;
    }
    void IsGrounded()
    {
        if (Physics2D.Raycast(transform.position, -Vector3.up, 0.1f)&& currState!=State.Hit)
        {
            agent.speed = 3.5f;
            isGrounded = true;
            particleS.SetActive(false);
        }
        else
        {
            agent.speed = 15f*multiplier;
            isGrounded = false;
            particleS.SetActive(true);
        }
    }
    State currentStateIf;
    private bool gotHit;

    void AnimationStates()
    {
        if (currState!= currentStateIf)
        {
            currentStateIf = currState;
            switch (currState)
            {
                case State.Stand:
                    SetTrigger("Idle");
                    break;
                case State.Chase:
                    SetTrigger("Run");
                    break;
            }
        }
        
    }
    void States()
    {
        switch (currState)
        {
            case State.Stand:
                if (player.position.x < transform.position.x)
                {
                    facingRight = false;
                }
                else
                {
                    facingRight = true;
                }
                if (lockHit) currState = State.Hit;
                
                                
                agent.isStopped = true;
                if (Vector3.Distance(transform.position, player.position) < aggroDistance && Mathf.Abs(transform.position.z - player.position.z) < aggroHeightDistance)
                {
                    currState = State.Chase;
                    agent.isStopped = false;
                }
                break;
            case State.Chase:
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
                if (Vector3.Distance(transform.position, player.position) < 10 && isGrounded && !lockHit)
                {
                    currState = State.Hit;
                }

                if (Vector3.Distance(transform.position, player.position) > aggroDistance || Mathf.Abs(transform.position.z - player.position.z) > aggroHeightDistance)
                {
                    currState = State.Stand;
                }
                break;
            case State.Hit:
                if (!lockHit)
                {
                    lockHit = true;
                    StartCoroutine(HitPlayer());
                }
               
                break;
        }
    }
    void SetTrigger(string trigger)
    {
        anim.ResetTrigger("Run");
        anim.ResetTrigger("Idle");
        anim.ResetTrigger("Attack");
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(trigger)) return;
        anim.SetTrigger(trigger);
    }
    bool CheckReachablePoint(Vector3 destination)
    {
        return NavMesh.CalculatePath(transform.position, destination, NavMesh.AllAreas, new NavMeshPath());
    }
    void StayFixed()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        if (facingRight)
            {
                transform.eulerAngles = new Vector3(transform.rotation.x, 90, transform.rotation.z);
            }
            else
            {
                transform.eulerAngles = new Vector3(transform.rotation.x, -90, transform.rotation.z);
            }

    }
    IEnumerator HitPlayerEasy()
    {
        SetTrigger("Attack");
        agent.speed = 0f;
        agent.isStopped = true;
        yield return new WaitForSeconds(2.5f);
        lockHit = false;
        agent.speed = 3.5f;
        agent.isStopped = false;
        currState = State.Chase;
    }
    IEnumerator HitPlayer()
    {
        SetTrigger("Idle");
        agent.speed = 0f;
        agent.isStopped = true;
        yield return new WaitForSeconds(1f);
        agent.isStopped = false;
        SetTrigger("Attack");
        if (facingRight)
        {
            agent.SetDestination(transform.position + new Vector3(10,0,0));
        } else
        {
            agent.SetDestination(transform.position + new Vector3(-10, 0, 0));
        }
        
        agent.speed = 15f*multiplier;
        yield return new WaitForSeconds(1f);
        agent.speed = 3.5f;
        currState = State.Chase;
        yield return new WaitForSeconds(5f/ multiplier);
        lockHit = false;
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
    }
    void RecieveDamageFromPlayer(float damage)
    {
        
        currentHealth -= damage;
        StartCoroutine(GotHit());
        //Destroy(this.gameObject);
    }
    IEnumerator GotHit()
    {
        gotHit = true;
        float currAgentSpeed = agent.speed;
        agent.speed = 0f;
        yield return new WaitForSeconds(2f);
        agent.speed = currAgentSpeed;
        gotHit = false;
    }
}
