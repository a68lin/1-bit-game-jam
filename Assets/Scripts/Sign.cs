using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour
{

    public string text;
    public bool isAnchor;

    [Header("Text Settings")]
    [Range(0, 10)]
    public float appearTime;
    [Range(0, 10)]
    public float fadeTime;

    [Header("Fixed Assets")]
    public Sprite usedSignSprite;
    public GameObject target;

    private PlayerArrows pArrows;
    private PlayerMovement pMove;

    private SpriteRenderer spriteRenderer;
    private MapEditor map;
    private DialogManager dialog;

    private bool isTriggered;
    private bool detectPlayer = false;

    private void Start()
    {
        isTriggered = false;

        GameObject player = GameObject.FindWithTag("Player");
        pArrows = player.GetComponent<PlayerArrows>();
        pMove = player.GetComponent<PlayerMovement>();

        dialog = GameObject.FindWithTag("DialogManager").GetComponent<DialogManager>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        map = GameObject.FindWithTag("Maps").GetComponent<MapEditor>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && detectPlayer)
        {
            if (isAnchor)
            {
                pMove.MoveTo(target.transform.position);
            }
            else
            {
                dialog.ShowDialog(text, appearTime, fadeTime);
            }
        }  
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        detectPlayer = true;

        if (transform.CompareTag("Destination"))
        {
            dialog.ShowDialog(text, appearTime, fadeTime);
            return;
        }

        if (!isAnchor && !isTriggered)
        {
            isTriggered = true;

            dialog.ShowDialog(text, appearTime, fadeTime);

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
        detectPlayer = false;
    }
}
