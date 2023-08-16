using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [Header("Switch")]
    [Range(0, 10)]
    public float switchCD;
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

        if (Input.GetKeyDown(KeyCode.Q)) {
            Save();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Load();
        }
    }

    private void FixedUpdate()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        pMove.Move(new Vector2(moveX, moveY).normalized);
    }

    private void SwitchState()
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
            // ShowDialog("Blocked by the wall.")
        }
    }

    private void Save()
    {
        Debug.Log("Save");
        map.SetFlag(transform.position);
    }

    private void Load()
    {
        Debug.Log("Load");
        pMove.MoveTo(map.SwitchToFlag());
    }
}
