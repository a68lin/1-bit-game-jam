using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerMovement : MonoBehaviour
{
    [Header("Speed")]
    public float moveSpeed;
    public float rotateSpeed;

    private Rigidbody2D rb;

    private bool stateIsLight;
    private Vector3 teleportOffset;

    private void Start()
    {
        stateIsLight = true;

        // [TODO] Don't use hard code, use the transform.position of tilemap
        teleportOffset = new Vector2((float)139.999909, (float)48.000009);

        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 dir)
    {
        rb.velocity = new Vector2(dir.x * moveSpeed, dir.y * moveSpeed);

        if (dir != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, dir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotateSpeed * Time.deltaTime);
        }
    }

    public void Teleport()
    {
        if (stateIsLight)
        {
            transform.position += teleportOffset;
        }
        else
        {
            transform.position -= teleportOffset;
        }

        stateIsLight = !stateIsLight;
    }
}
