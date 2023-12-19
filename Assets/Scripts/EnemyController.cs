using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyController : MonoBehaviour
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

    }

    private void Flip(bool value)
    {
        spriteRenderer.flipX = value;
        if(!value)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }
    }
}
