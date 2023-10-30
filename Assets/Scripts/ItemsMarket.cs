using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class ItemsMarket : MonoBehaviour
{
    public int id;
    public Sprite spite;
    public int price;
    public TextMeshProUGUI priceText;
    public bool isActive = true;
    public Color colorActive;
    public Color colorInactive;
    public Image image;

    void Start()
    {
        priceText.text = price.ToString();
        image = GetComponent<Image>();

        Button button = transform.AddComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        if (isActive)
        {
            isActive = false;
            image.color = colorInactive;
        }
        else
        {
            isActive = true;
            image.color = colorActive;
        }
    }
}
