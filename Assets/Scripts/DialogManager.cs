using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Text message;
    [Header("Remaining time")]
    [Range(0, 20)]
    private float timeToAppear = 5f;
    private float timeWhenDisappear;

    //private void Start()
    //{
    //    ShowDialog("Correct direction!");
    //}

    private void EnableText()
    {
        message.enabled = true;
        timeWhenDisappear = Time.time + timeToAppear;
    }

    public void ShowDialog(string text)
    {
        message.text = text;
        message.color = Color.black;
        message.alignment = TextAnchor.UpperCenter;
        message.fontSize = 18;
        message.fontStyle = FontStyle.Bold;
        EnableText();
        RectTransform rt = message.GetComponent<RectTransform>();
        rt.anchoredPosition3D = new Vector3(0, 70, 0);
        rt.sizeDelta = new Vector2(1000, 50);
    }

    private void Update()
    {
        if (message.enabled && (Time.time >= timeWhenDisappear))
        {
            message.enabled = false;
        }
    }
}
