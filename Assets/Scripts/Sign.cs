using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour
{
    public string text;
    public bool isAnchor;

    [Header("Fixed Assets")]
    public Sprite usedSignSprite;
    public GameObject target;

    private PlayerArrows pArrows;
    private PlayerMovement pMove;

    private SpriteRenderer spriteRenderer;
    private MapEditor map;

    private bool isTriggered;
    private bool detectPlayer = false;

    private void Awake()
    {
        isTriggered = false;

        GameObject player = GameObject.FindWithTag("Player");
        pArrows = player.GetComponent<PlayerArrows>();
        pMove = player.GetComponent<PlayerMovement>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        map = GameObject.FindWithTag("Maps").GetComponent<MapEditor>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && detectPlayer)
        {
            pMove.MoveTo(target.transform.position);
        }  
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (transform.CompareTag("Destination"))
        {
            // [TODO] Game success check
            Debug.Log(text);

            return;
        }

        if (isAnchor)
        {
            detectPlayer = true;
            return;
        }

        if (!isAnchor && !isTriggered)
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

    private void OnTriggerExit2D(Collider2D other)
    {
        if (isAnchor)
        {
            detectPlayer = false;
        }
    }
}
