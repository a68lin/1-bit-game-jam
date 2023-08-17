using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class content
{
    public Text message;
    public Image image;
    public float timeWhenDisappear;
    public float fadeTime;
    public float appearTime;
    public content(Text text, Image image, string info, float timeAppear, float timeFade)
    {
        this.message = text;
        this.image = image;
        this.timeWhenDisappear = Time.time + timeAppear;
        this.fadeTime = timeFade;
        this.appearTime = timeAppear;
        this.message.text = info;
    }
}

public class DialogManager : MonoBehaviour
{
    public GameObject[] canvas;

    [Header("Appear time")]
    [Range(0, 10)]
    public float appearTime;

    [Header("Fade time")]
    [Range(0, 10)]
    public float fadeTime;

    private List<content> contents = new List<content>();

    private void Update()
    {
        List<content> toRemove = new List<content>();

        if (contents == null)
        {
            return;
        }

        foreach (content dialog in contents)
        {
            if (dialog.message == null)
            {
                toRemove.Add(dialog);
                continue;
            }

            Color color = dialog.message.color;
            if (color.a > 0 && Time.time >= dialog.timeWhenDisappear)
            {
                color.a -= Time.deltaTime / dialog.fadeTime;
                dialog.message.color = color;

            }

            color = dialog.image.color;
            if (color.a > 0 && Time.time >= dialog.timeWhenDisappear)
            {
                color.a -= Time.deltaTime / dialog.fadeTime;
                dialog.image.color = color;
            }
        }

        foreach(content dialog in toRemove)
        {
            contents.Remove(dialog);
        }
    }

    public void ShowDialog(string text, float timeAppear, float timeFade, int index = 0)
    {
        if (text != "")
        {
            GameObject copy = Instantiate<GameObject>(canvas[index], transform.position, Quaternion.identity);

            contents.Add(
                new content(
                    copy.GetComponentInChildren<Text>(),
                    copy.GetComponentInChildren<Image>(),
                    text, timeAppear, timeFade));

            Destroy(copy, timeAppear + timeFade);
        }
    }

    public void ShowDialog(string text, int index = 0)
    {
        GameObject copy = Instantiate<GameObject>(canvas[index], transform.position, Quaternion.identity);

        contents.Add(
            new content(copy.GetComponentInChildren<Text>(),
                        copy.GetComponentInChildren<Image>(),
                        text, appearTime, fadeTime));

        Destroy(copy, appearTime + fadeTime);
    }
}
