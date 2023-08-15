using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField]
    private GameObject arrowPrefab;

    [Header("Switch")]
    [Range(0, 10)]
    public float switchCD;
    private float lastSwitchTime;

    private PlayerMovement pMove;
    private PlayerAnimation pAnim;

    private void Start()
    {
        pMove = GetComponent<PlayerMovement>();
        pAnim = GetComponent<PlayerAnimation>();
    }

    private void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.Space) && Time.time - lastSwitchTime >= switchCD)
        {
            pAnim.Switch();
            pMove.Teleport();

            lastSwitchTime = Time.time;
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
