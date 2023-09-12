using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public int maxHealth = 100;
    public int maxStamina = 100;
    public int attackDamage = 10;
    private int direction = 1;

    private bool isJumping = false;
    private int currentHealth;
    private int currentStamina;

    private Rigidbody2D rigidbody2D;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        currentStamina = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");

        // movimento horizontal
        rigidbody2D.velocity = new Vector2(moveX * moveSpeed, rigidbody2D.velocity.y);
        //PlayerAnimation("PlayerIddle");
        if(moveX != 0)
        {
            animator.SetBool("taCorrendo", true);
            if (moveX > 0)
            {
                Flip(false);
            }
            else if (moveX < 0)
            {
                Flip(true);
            }
        }
        else
        {
            animator.SetBool("taCorrendo", false);
        }

        // pulo
        if(Input.GetButtonDown("Jump") && !isJumping)
        {
            rigidbody2D.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
        }

        // Ataque
        if (Input.GetButtonDown("Fire1"))
        {
            if (currentStamina.Equals(0))
            {
                Debug.Log("Recuperandop Stamina");
                for (int i = 0; i < maxStamina; i += 10)
                {
                    Debug.Log($"Aumentando Stamina: {i}");
                    RestoreStamina(i);
                }
            }
            else
            {
                Attack();
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

    private void Attack()
    {
        if(currentStamina >= 10)
        {
            animator.SetTrigger("taAttacando");
            currentStamina -= 10;
            Debug.Log("Player ataca!");
            // Lógica para causar dano ao inimigo
        }
        else
        {
            Debug.Log("Player está sem stamina para atacar.");
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player morreu!");
        animator.SetBool("taMorto", true);
    }

    public void RestoreHealth(int amount)
    {
        currentHealth += amount;

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void RestoreStamina(int amount)
    {
        currentStamina += amount;

        if (currentStamina > maxStamina)
        {
            currentStamina = maxStamina;
        }
    }

    public void PlayerAnimation(string animationName)
    {
        this.animator.Play(animationName);
    }

    void Flip(bool value)
    {
        //PlayerAnimation("PlayerRun");
        spriteRenderer.flipX = value;
        if (!value)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }
    }
}
