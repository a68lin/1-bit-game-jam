using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class PlayerMovement : MonoBehaviour
{
    [Header("Speed")]
    public float moveSpeed;
    public float rotateSpeed;

    private Rigidbody2D rb;
    private Vector2 moveDir;
    private PlayerAction action;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        action = GetComponent<PlayerAction>();

        // [TODO] For test purpose, need to be modified later
        action.GenerateArrow(GameObject.Find("DummyPos").transform.position);
    }

    // Update is called once per frame
    private void Update()
    {
        inputManagement();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveDir.x * moveSpeed, moveDir.y * moveSpeed);

        if (moveDir != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, moveDir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotateSpeed * Time.deltaTime);
        }
    }

    private void inputManagement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(moveX, moveY).normalized;
    }
}
