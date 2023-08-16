using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [Header("Switch")]
    [Range(0, 10)]
    public float switchCD;
    public DialogManager dialog;
    private float lastSwitchTime;

    private PlayerMovement pMove;
    private PlayerAnimation pAnim;
    private PlayerArrows pArrows;
    private MapEditor map;

    private void Start()
    {
        pMove = GetComponent<PlayerMovement>();
        pAnim = GetComponent<PlayerAnimation>();
        pArrows = GetComponent<PlayerArrows>();
        map = GameObject.FindWithTag("Maps").GetComponent<MapEditor>();

        map.InitMapSets();
        Vector3 startPos = map.UseMapSet(0);
        pMove.MoveTo(startPos);
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

    public void SwitchState()
    {
        Vector3 curPos = transform.position;
        Vector3 offset;
        if (map.SwitchToNextMap(curPos, out offset))
        {
            pMove.MoveTo(curPos + offset);
            pAnim.Switch();
            pArrows.UpdatePos(offset);

            lastSwitchTime = Time.time;
        }
        else
        {
            Debug.Log("Blocked by the wall.");
            dialog.ShowDialog("Blocked by the wall.");
        }
    }
}
