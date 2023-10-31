using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Market : MonoBehaviour
{
    public List<ItemsMarket> marketList = new();
    public List<PushedItems> pushedItems = new();

    public TMP_InputField moneyText;
    public long money;

    private int currentPushedItems = 0;

    public TextMeshProUGUI responsePierre;

    [TextArea]
    public string startText;
    [TextArea]
    public string errorText;
    [TextArea]
    public string successText;

    private void OnEnable()
    {
        responsePierre.text = startText;
    }

    public void Purchase()
    {
        ResetItens();

        Queue queue = new();

        for (int i = marketList.Count - 1; i >= 0; i--)
        {
            ItemsMarket item = marketList[i];
            if (item.isActive)
            {
                queue.Enqueue(item);
            }
        }
        if (moneyText.text.Length > 0)
            money = long.Parse(moneyText.text);
        else
            money = 0;

        Coins(queue);
    }

    public void ResetItens()
    {
        foreach (PushedItems item in pushedItems)
        {
            item.DesactiveItem();
        }
    }

    public void LoadCoins()
    {
        moneyText.text = money.ToString();
    }

    public void Coins(Queue queue)
    {
        foreach (ItemsMarket item in queue)
        {
            int response = AddPushedItem(item, money);

            if (response == -1)
            {
                ResetItens();
                responsePierre.text = errorText;
                break;
            }
        }

        responsePierre.text = successText;
        currentPushedItems = 0;

        LoadCoins();
    }

    public int AddPushedItem(ItemsMarket item, long value)
    {
        if (value == 0)
            return -1;

        if (currentPushedItems == pushedItems.Count)        
            return -1;
        

        PushedItems pushedItem = pushedItems[currentPushedItems];

        while (value > 99)
        {
            pushedItem.SetItem(99, item.spite);
            value -= 99;
            currentPushedItems++;
            pushedItem = pushedItems[currentPushedItems];

            if (currentPushedItems == pushedItems.Count - 1)
                return -1;
            
        }

        pushedItem.SetItem((int)value, item.spite);
        currentPushedItems++;

        return 0;
    }
}
