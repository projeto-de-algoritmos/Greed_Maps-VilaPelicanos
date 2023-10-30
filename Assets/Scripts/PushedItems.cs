using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PushedItems : MonoBehaviour
{
    public int count;
    public Sprite sprite;

    public TextMeshProUGUI value;
    public Image image;

    public CanvasGroup canvas;

    public void SetItem(int count, Sprite sprite)
    {
        this.count = count;
        this.sprite = sprite;

        value.text = count.ToString();
        image.sprite = sprite;

        canvas.alpha = 1;
    }

    public void DesactiveItem()
    {
        canvas.alpha = 0;
    }
}
