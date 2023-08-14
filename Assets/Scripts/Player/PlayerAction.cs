using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField]
    private GameObject arrowPrefab;

    private PlayerAnimation anim;

    private void Start()
    {
        anim = GetComponent<PlayerAnimation>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Switch"))
        {
            anim.Switch();
        }
    }

    public void GenerateArrow(Vector3 pos)
    {
        GameObject arrowObject = Instantiate(arrowPrefab, Vector3.zero, Quaternion.identity);
        arrowObject.transform.SetParent(transform);
        Arrow arrow = arrowObject.GetComponent<Arrow>();
        arrow.SetTargetPos(pos);
    }
}
