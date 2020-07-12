﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBasic : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public float speed = 0.5f;

    private float waitTime;
    public float starWaitTime = 2;

    public Transform[] moveSpots;

    private int i = 0;
    private Vector2 actualPos;


    void Start()
    {
        waitTime = starWaitTime;
    }

    
    void Update()
    {
        StartCoroutine(CheckEnemyMoving());

        transform.position = Vector2.MoveTowards(transform.position, moveSpots[i].transform.position, speed * Time.deltaTime);
        
        if (Vector2.Distance(transform.position, moveSpots[i].transform.position)<0.1f)
        {
            if (waitTime<=0)
            {
                if (moveSpots[i]!= moveSpots[moveSpots.Length -1])
                {
                    i++;
                }
                else
                {
                    i = 0;
                }
                waitTime = starWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    IEnumerator CheckEnemyMoving()
    {
        actualPos = transform.position;
        yield return new WaitForSeconds(0.5f);

        if (transform.position.x > actualPos.x)
        {
            spriteRenderer.flipX = true;
        }
        else if (transform.position.x < actualPos.x)
        {
            spriteRenderer.flipX = false;
        }


    }
}
