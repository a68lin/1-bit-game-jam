using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class Arrow : MonoBehaviour
{
    [Range(0, 1)]
    public float radius;

    [Header("Track Range")]
    public float rangeMin;
    public float rangeMax;
    [Range(0, 1)]
    public float destoryRange;

    [Header("Scale Range")]
    public float scaleMin;
    public float scaleMax;

    [Range(1, 3)]
    public float scaleChangeRate;

    private DialogManager dialog;
    private Vector3 targetPos;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void Start()
    {
        dialog = GameObject.FindWithTag("DialogManager").GetComponent<DialogManager>();
    }

    private void Update()
    {
        startTracking();
        checkIfReachedDestination();
    }

    public void SetTargetPos(Vector3 pos)
    {
        targetPos = pos;
        gameObject.SetActive(true);
    }

    public void UpdateTargetPos(Vector3 offset)
    {
        targetPos += offset;
    }

    private void startTracking()
    {
        // Set position
        Vector3 playerPos = transform.parent.position;
        Vector3 dir = (targetPos - playerPos).normalized;
        transform.position = playerPos + dir * radius;

        // Set orientation
        float angle = Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        // Set scale
        float distance = Mathf.Max(Vector3.Distance(targetPos, playerPos), rangeMin);
        float t = Mathf.Pow((distance - rangeMin) / (rangeMax - rangeMin), 1 / scaleChangeRate);
        float scaleFactor = Mathf.Lerp(scaleMax, scaleMin, Mathf.Clamp(t, 0, 1));
        transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
    }

    private void checkIfReachedDestination()
    {
        if (Vector3.Distance(transform.parent.position, targetPos) < destoryRange)
        {
            dialog.ShowDialog("Reached target :)", 1f, 0.5f, 1);
            Destroy(transform.gameObject);
        }
    }
}
