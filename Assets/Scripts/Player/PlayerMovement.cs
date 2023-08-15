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
    public float teleportCD;

    private Rigidbody2D rb;
    private Vector2 moveDir;
    private PlayerAction action;
    private GameObject player;
    private Vector2 teleportOffset;
    private bool stateIsLight;

    private void Start()
    {
        stateIsLight = true;
        player = GameObject.Find("Player");
        teleportOffset = new Vector2((float)139.999909, (float)48.000009);

        rb = GetComponent<Rigidbody2D>();
        action = GetComponent<PlayerAction>();

        // [TODO] For test purpose, need to be modified later
        action.GenerateArrow(GameObject.Find("DummyPos").transform.position);
    }

    // Update is called once per frame
    private void Update()
    {
        inputManagement();
        detectTeleport();
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

    private void detectTeleport()
    {
        if (Input.GetKeyDown("space"))
        {
            Vector3 newPosition;
            if (stateIsLight)
            {
                newPosition = new Vector3(player.transform.position.x + teleportOffset.x, player.transform.position.y + teleportOffset.y, player.transform.position.z);
            } 
            else
            {
                newPosition = new Vector3(player.transform.position.x - teleportOffset.x, player.transform.position.y - teleportOffset.y, player.transform.position.z);
            }

            player.transform.position = newPosition;
            stateIsLight = !stateIsLight;
        }
    }
}
