﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float RunSpeed = 2;
    public float JumpSpeed = 3;

    Rigidbody2D rb2D;

    public bool betterJump = false;
    public float fallMultiplier = 0.5f;
    public float lowJumpMultiplier = 1f;

    public Animator animator;
    public SpriteRenderer spriteRenderer;

    public Transform bulletSpawner;
    public GameObject bulletPrefab;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        PlayerShooting();
        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            rb2D.velocity = new Vector2(RunSpeed, rb2D.velocity.y);
            //spriteRenderer.flipX = false;
            transform.localScale = new Vector3(1, 1, 1);
            animator.SetBool("Run", true);
        }

        else if(Input.GetKey("a") || Input.GetKey("left"))
        {
            rb2D.velocity = new Vector2(-RunSpeed, rb2D.velocity.y);
            //spriteRenderer.flipX = true;
            transform.localScale = new Vector3(-1, 1, 1);
            animator.SetBool("Run", true);

        }
        else if (Input.GetButtonDown("Fire1"))
        {
            PlayerShooting();
        }
        else
        {
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
            animator.SetBool("Run", false);
            animator.SetBool("Shotidle", false);
        }     

        if (Input.GetKey("space") && CheckGround.isGrounded)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, JumpSpeed);
        }
        if (CheckGround.isGrounded == false)
        {
            animator.SetBool("Jump", true);
            animator.SetBool("Run", false);
        }
        if (CheckGround.isGrounded == true)
        {
            animator.SetBool("Jump", false);
        }
        if (betterJump)
        {
            if (rb2D.velocity.y < 0)
            {
                rb2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier) * Time.deltaTime;
            }
            if (rb2D.velocity.y > 0 && !Input.GetKey("space"))
            {
                rb2D.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier) * Time.deltaTime;
            }
        }
    }
    public void PlayerShooting()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetBool("Shotidle", true);
            Instantiate(bulletPrefab, bulletSpawner.position, bulletSpawner.rotation);
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            animator.SetBool("Shotidle", false);
        }
    }
}
