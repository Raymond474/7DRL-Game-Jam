using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    PlayerInputs controls;


    public float movementSpeed = 5f;
    public int attackDamage = 50;
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    public bool isRanged = false;
    public bool isSweep = false;
    public float attackRadius = 2f;
    public Rigidbody2D rb;

    public Transform attackPoint;
    public float attackLength;
    public float attackWidth;
    public LayerMask enemyLayers;

    public GameObject pauseMenuUI;
    public static bool isPaused = false;

    private bool isNorth = false;
    private bool isWest = false;
    private bool isEast = true;
    private Vector3 attackPosition;


    Vector2 movement;
    
    void Awake()
    {
        controls = new PlayerInputs();

        controls.Player.Attack.performed += ctx => Attack();

        controls.Player.Pause.performed += ctx => PauseOpen();

        controls.Player.Move.performed += ctx => movement = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => movement = Vector2.zero;
    }
    
    void FixedUpdate() 
    {
        // Movement
        rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);

        //0.71 is when going diagonal

        //Movement animations

        //right animation
        if (movement == Vector2.right)
        {
            //update attack position
            isWest = false;
            isNorth = false;
            isEast = true;
        }
        //left animation
        if (movement == Vector2.left)
        {
            isWest = true;
            isNorth = false;
            isEast = false;
        }
        //up animation
        if (movement == Vector2.up)
        {
            isWest = false;
            isNorth = true;
            isEast = false;
        }
        //down animation
        if (movement == Vector2.down)
        {
            isWest = false;
            isNorth = false;
            isEast = false;
        }
    }

    void Attack()
    {
        if (isRanged)
        {

        }
        else
        {
            //Cooldown Check
            if (Time.time >= nextAttackTime)
            {
                float x = 0f;
                float y = 0f;

                if (isWest)//facing left
                {
                    x = attackPoint.position.x - 1f;
                    y = attackPoint.position.y;
                }
                else if (isEast)//facing right
                {
                    x = attackPoint.position.x + 1f;
                    y = attackPoint.position.y;
                }
                else if (isNorth)
                {
                    x = attackPoint.position.x;
                    y = attackPoint.position.y + 1f;
                }
                else
                {
                    x = attackPoint.position.x;
                    y = attackPoint.position.y - 1f;
                }

                attackPosition = new Vector3(x, y, 0f);

                if (isSweep)
                {
                    attackPosition.x = 0f;
                    SweepAttack();
                }
                else
                {
                    MeleeAtackNotSweep();
                }
            }
        }
    }

    private void MeleeAtackNotSweep()
    {
        Vector2 attackRange = new Vector2(attackLength, attackWidth);

        if (isNorth || (!isWest && !isEast))//facing North or South
        {
            attackRange = new Vector2(attackWidth, attackLength);
        }

        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackPosition, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            if (enemy.tag == "Enemy")
            {
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            }
                
        }

        nextAttackTime = Time.time + 1f / attackRate;
    }

    private void SweepAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            if (enemy.tag == "Enemy")
            {
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            }
                
        }

        nextAttackTime = Time.time + 1f / attackRate;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Vector2 attackRange = new Vector2(attackLength, attackWidth);

        float x = 0f;
        float y = 0f;

        //this code is not needed here. only to show real time in the editor
        if (isWest)//facing left
        {
            x = attackPoint.position.x - 1f;
            y = attackPoint.position.y;
        }
        else if (isEast)//facing right
        {
            x = attackPoint.position.x + 1f;
            y = attackPoint.position.y;
        }
        else if (isNorth)
        {
            x = attackPoint.position.x;
            y = attackPoint.position.y + 1f;

            attackRange = new Vector2(attackWidth, attackLength);
        }
        else
        {
            x = attackPoint.position.x;
            y = attackPoint.position.y - 1f;

            attackRange = new Vector2(attackWidth, attackLength);
        }

        Vector3 attackPosition = new Vector3(x, y, 0f);
        
        

        if (isSweep)
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        }
        else
        {
            Gizmos.DrawWireCube(attackPosition, attackRange);
        }
    }

    private void PauseOpen()
    {
        if (isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    private void Pause()
    {
        //add turn off UI

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;

        //change music

        isPaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        //change music
        isPaused = false;
    }

    void OnEnable() 
    {
        controls.Player.Enable();
    }

    void OnDisable()
    {
        controls.Player.Disable();
    }
}
