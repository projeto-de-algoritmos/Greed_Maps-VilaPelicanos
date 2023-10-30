using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Market : MonoBehaviour
{
    public List<ItemsMarket> marketList = new();
    public List<PushedItems> pushedItems = new();

    public long money;

    private int currentPushedItems = 0;

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

        Coins(queue);
    }

    public void ResetItens()
    {
        foreach (PushedItems item in pushedItems)
        {
            item.DesactiveItem();
        }
    }

    public void Coins(Queue queue)
    {
        foreach (ItemsMarket item in queue)
        {
            AddPushedItem(item, 115);
        }

        currentPushedItems = 0;
    }

    public void AddPushedItem(ItemsMarket item, int value)
    {
        if (value == 0)
            return;

        if (currentPushedItems == pushedItems.Count)
        {
            Debug.Log("AAAAAAAAAAAAAA");
            return;
        }

        PushedItems pushedItem = pushedItems[currentPushedItems];

        while (value > 99)
        {
            pushedItem.SetItem(99, item.spite);
            value -= 99;
            currentPushedItems++;
            pushedItem = pushedItems[currentPushedItems];

            if (currentPushedItems == pushedItems.Count - 1)
            {
                pushedItem.SetItem(value, item.spite);
                return;
            }
        }

        pushedItem.SetItem(value, item.spite);
        currentPushedItems++;
    }
}
