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
    public Rigidbody2D rb;

    public Transform attackPoint;
    public float attackLength;
    public float attackWidth;
    public LayerMask enemeyLayers;

    public GameObject pauseMenuUI;
    public static bool isPaused = false;


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

        //Movement animations

        //right animation
        if (movement == Vector2.right)
        {

        }
        //left animation
        if (movement == Vector2.left)
        {
            
        }
        //up animation
        if (movement == Vector2.up)
        {
            
        }
        //down animation
        if (movement == Vector2.down)
        {
            
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
                MeleeAtack();
            }

            
        }
    }

    private void MeleeAtack()
    {
        Vector2 attackRange = new Vector2(attackLength, attackWidth);
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackPoint.position, attackRange, enemeyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            if (enemy.tag == "Enemy")
            {
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            }
                
        }

        nextAttackTime = Time.time + 1f / attackRate;
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
