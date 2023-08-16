using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArrows : MonoBehaviour
{
    public GameObject arrowPrefab;

    private List<Arrow> arrows;

    private void Awake()
    {
        arrows = new List<Arrow>();
    }

    public void GenerateArrow(Vector3 pos)
    {
        GameObject arrowObject = Instantiate(arrowPrefab, Vector3.zero, Quaternion.identity);
        arrowObject.transform.SetParent(transform);
        Arrow arrow = arrowObject.GetComponent<Arrow>();
        arrow.SetTargetPos(pos);
        arrows.Add(arrow);
    }

    public void UpdatePos(Vector3 offset)
    {
        foreach (Arrow arw in arrows)
        {
            arw.UpdateTargetPos(offset);
        }
    }
}
