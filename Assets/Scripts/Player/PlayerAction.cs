using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public GameObject arrowPrefab;

    [Header("Switch")]
    [Range(0, 10)]
    public float switchCD;
    private float lastSwitchTime;

    private PlayerMovement pMove;
    private PlayerAnimation pAnim;
    private MapEditor map;

    private void Start()
    {
        pMove = GetComponent<PlayerMovement>();
        pAnim = GetComponent<PlayerAnimation>();
        map = GameObject.FindWithTag("Maps").GetComponent<MapEditor>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time - lastSwitchTime >= switchCD)
        {
            SwitchState();
        }
    }

    private void FixedUpdate()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        pMove.Move(new Vector2(moveX, moveY).normalized);
    }

    public void GenerateArrow(Vector3 pos)
    {
        GameObject arrowObject = Instantiate(arrowPrefab, Vector3.zero, Quaternion.identity);
        arrowObject.transform.SetParent(transform);
        Arrow arrow = arrowObject.GetComponent<Arrow>();
        arrow.SetTargetPos(pos);
    }

    public void SwitchState()
    {
        pAnim.Switch();
        pMove.Teleport(map.GetCurrentOffset());

        lastSwitchTime = Time.time;
    }
}
