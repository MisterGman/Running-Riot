using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour
{

    Player player;
    public Collider attack;

    private bool attacking = false;
    private float attackTimer = 0;
    private float attackCooldown = 0.3f;

    void Start()
    {
        player = GetComponent<Player>();
        attack.enabled = false;
    }

    void Update()
    {
        Vector2 directionalInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        player.SetDirectionalInput(directionalInput);

        if (Input.GetKeyDown(KeyCode.LeftShift) && directionalInput.x != 0)
        {
            player.OnDashInput();
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.OnJumpInputDown();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            player.OnJumpInputUp();
        }
        if (Input.GetButtonDown("Fire1") && !attacking)
        {
            
            attacking = true;
            attackTimer = attackCooldown;

            attack.enabled = true;
        }
        if (attacking)
        {
            if(attackTimer > 0)
            {
                player.Attack();
                attackTimer -= Time.deltaTime;
            }
            else
            {
                attacking = false;
                attack.enabled = false;
            }
        }
    }
}
