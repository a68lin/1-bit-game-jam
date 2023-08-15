using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour
{
    public string text;

    [Header("Fixed Assets")]
    public Sprite usedSignSprite;
    public GameObject target;

    private PlayerAction pAction; 
    private SpriteRenderer spriteRenderer;

    private bool isTriggered;

    private void Awake()
    {
        isTriggered = false;

        pAction = GameObject.FindWithTag("Player").GetComponent<PlayerAction>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
            pAction.GenerateArrow(targetPos);

            // Set <Used>
            spriteRenderer.sprite = usedSignSprite;
        }
    }
}
