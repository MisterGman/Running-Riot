using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
    public float maxHP = 1000; // the highest available level of health
    public float currHP = 100; // current health

    public bool invincible;
    public bool dashCooldown;
    private bool dashActive;

    public float maxJumpHeight = 4;
    public float minJumpHeight = 1;
    public float timeToJumpApex = .4f;
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    [HideInInspector]
    public float moveSpeed;
    public float moveSpeedChanger = 10f;

    public Vector2 wallJumpClimb;
    public Vector2 wallJumpOff;
    public Vector2 wallLeap;

    public float wallSlideSpeedMax = 3;
    public float wallStickTime = .25f;
    float timeToWallUnstick;
    bool wallSliding;
    int wallDirX;

    public float gravity;
    float maxJumpVelocity;
    float minJumpVelocity;

   
    float velocityXSmoothing;

    Controller2D controller;


    [HideInInspector]
    public Vector3 velocity;
    [HideInInspector]
    public Vector2 directionalInput;



    public GameObject invisibleCloak;

    void Start()
    {
        moveSpeed = moveSpeedChanger;
        controller = GetComponent<Controller2D>();
        invincible = false;
        invisibleCloak.SetActive(false);

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
    }

    void Update()
    {
        CalculateVelocity();
        HandleWallSliding();

        controller.Move(velocity  * Time.deltaTime, directionalInput);
        if (invincible)
        {
            invisibleCloak.SetActive(true);
        }
        else
        {
            invisibleCloak.SetActive(false);
        }

        if (controller.collisions.above || controller.collisions.below)
        { 
            if (controller.collisions.slidingDownMaxSlope)
            {
                velocity.y += controller.collisions.slopeNormal.y * -gravity * Time.deltaTime;
            }
            else
            {
                velocity.y = 0;
            }
        }
    }

    public void SetDirectionalInput(Vector2 input)
    {
        
        directionalInput = input;
    }

    public void OnJumpInputDown()
    {
        if (wallSliding)
        {
            if (wallDirX == directionalInput.x)
            {
                velocity.x = -wallDirX * wallJumpClimb.x;
                velocity.y = wallJumpClimb.y;
            }
            else if (directionalInput.x == 0)
            {
                velocity.x = -wallDirX * wallJumpOff.x;
                velocity.y = wallJumpOff.y;
            }
            else
            {
                velocity.x = -wallDirX * wallLeap.x;
                velocity.y = wallLeap.y;
            }
        }
        if (controller.collisions.below)
        {
            if (controller.collisions.slidingDownMaxSlope)
            {
                if (directionalInput.x != -Mathf.Sign(controller.collisions.slopeNormal.x))
                { // not jumping against max slope
                    velocity.y = maxJumpVelocity * controller.collisions.slopeNormal.y;
                    velocity.x = maxJumpVelocity * controller.collisions.slopeNormal.x;
                }
            }
            else
            {
                velocity.y = maxJumpVelocity;
            }
        }
    }

    public void OnJumpInputUp()
    {
        if (velocity.y > minJumpVelocity)
        {
            velocity.y = minJumpVelocity;
        }
    }

    public void OnDashInput()
    {
        if (!dashCooldown)
        {
            StartCoroutine(DashCooldown());
            StartCoroutine(Dash());
        }
       
    }

    IEnumerator DashCooldown()
    {
        dashCooldown = true;
        yield return new WaitForSeconds(.6f);
        dashCooldown = false;
    }
    IEnumerator Dash()
    {
        moveSpeed = 30;
        dashActive = true;
        invincible = true;
        yield return new WaitForSeconds(0.2f);
        moveSpeed = moveSpeedChanger;
        dashActive = false;
        invincible = false;

    }
    void HandleWallSliding()
    {
        wallDirX = (controller.collisions.left) ? -1 : 1;
        wallSliding = false;
        if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0)
        {
            wallSliding = true;

            if (velocity.y < -wallSlideSpeedMax)
            {
                velocity.y = -wallSlideSpeedMax;
            }

            if (timeToWallUnstick > 0)
            {
                velocityXSmoothing = 0;
                velocity.x = 0;

                if (directionalInput.x != wallDirX && directionalInput.x != 0)
                {
                    timeToWallUnstick -= Time.deltaTime;
                }
                else
                {
                    timeToWallUnstick = wallStickTime;
                }
            }
            else
            {
                timeToWallUnstick = wallStickTime;
            }

        }

    }

    void CalculateVelocity()
    {
        if (this.GetComponent<GraplingHook>().hooked)
        {
            gravity = 0;
        } else if (dashActive)
        {
            gravity = -35;
        }
        else
        {
            gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        }

        float targetVelocityX = directionalInput.x * (moveSpeed);
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
    }

    public void RecieveDamageFromEnemy(float damage) //through this method object receives damage
    {
        if (invincible)
        {
            damage = 0;
        } 
        currHP = currHP - damage;
        if (currHP < 0) currHP = 0;
        if (currHP > maxHP) currHP = maxHP;
        if (currHP <= 0) Die();
        if (!invincible)
        {
            StartCoroutine(BeginTimeout());
        }   
    }

    IEnumerator BeginTimeout()
    {
        invincible = true;
        yield return new WaitForSeconds(0.8f); 
        invincible = false;
    }
    protected  void Die() // things that happen while the object dies
    {
        Destroy(gameObject);
    }

    public void Attack()
    {
        velocity.x = (controller.collisions.faceDir > 0) ? 2 : -2;

    }

    public IEnumerator Knockback(float knockDur, float knockbackPwrY, float knockbackPwrX)
    {
        float timer = 0;

        while (knockDur > timer)
        {

            timer += Time.deltaTime;

            // velocity.x = (controller.collisions.faceDir> 0) ? knockbackDir.x * 10 * controller.collisions.faceDir : knockbackDir.x * 10 * -controller.collisions.faceDir;
            velocity.x = (controller.collisions.faceDir > 0) ? -knockbackPwrX : knockbackPwrX;
            velocity.y = knockbackPwrY;
        }

        yield return 0;

    }

}
