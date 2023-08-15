using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField]
    private GameObject arrowPrefab;

    [Header("Teleport")]
    [Range(0, 3)]
    public float teleportCD;

    private PlayerMovement pMove;
    private PlayerAnimation pAnim;

    private void Start()
    {
        pMove = GetComponent<PlayerMovement>();
        pAnim = GetComponent<PlayerAnimation>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pAnim.Switch();
            pMove.Teleport();
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
}
