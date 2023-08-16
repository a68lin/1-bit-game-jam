using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public GameObject[] canvas;
    [Header("Appear time")]
    [Range(0, 10)]
    public float appearTime = 3f;
    [Header("Fade time")]
    [Range(0, 10)]
    public float fadeTime = 2f;
    private Text message;
    private float timeWhenDisappear = 5f;
    //private const Vector3 position = new Vector3(0, 70, 0);

    private void Start()
    {
    }

    private void Update()
    {
        if (message != null)
        {
            Color color = message.color;
            if (color.a > 0 && Time.time >= timeWhenDisappear)
            {
                color.a -= Time.deltaTime / fadeTime;
                message.color = color;
            }
        }
    }

    public void ShowDialog(string text, int index = 0, Vector3 position = default(Vector3), float timeAppear = 3f, float timeFade = 2f)
    {
        fadeTime = timeFade;
        timeWhenDisappear = Time.time + timeAppear;

        GameObject copy = Instantiate<GameObject>(canvas[index], transform.position, Quaternion.identity);

        message = copy.GetComponentInChildren<Text>();

        message.text = text;
        message.color = Color.black;
        message.alignment = TextAnchor.UpperCenter;
        message.fontSize = 18;
        message.fontStyle = FontStyle.Bold;

        RectTransform rt = message.GetComponent<RectTransform>();
        rt.anchoredPosition3D = position;
        rt.sizeDelta = new Vector2(1000, 50);

        Destroy(copy, timeAppear + timeFade);
    }
}
