using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour
{
    public string text;

    [Header("Fixed Assets")]
    public Sprite usedSignSprite;
    public GameObject target;

    private PlayerArrows pArrows; 
    private SpriteRenderer spriteRenderer;
    private MapEditor map;

    private bool isTriggered;

    private void Awake()
    {
        isTriggered = false;

        pArrows = GameObject.FindWithTag("Player").GetComponent<PlayerArrows>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        map = GameObject.FindWithTag("Maps").GetComponent<MapEditor>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (transform.CompareTag("Destination"))
        {
            // [TODO] Game success check
            Debug.Log(text);

            return;
        }

        if (!isTriggered && other.CompareTag("Player"))
        {
            isTriggered = true;

            // [TODO] Show dialog
            Debug.Log(text);

            // Generate Arrow
            Vector3 targetPos = target.transform.position;
            pArrows.GenerateArrow(targetPos);
            map.DestroyWall(targetPos);

            // Set <Used>
            spriteRenderer.sprite = usedSignSprite;
        }
    }
}
