using UnityEngine;
using System.Collections.Generic;

public class TestInventory : MonoBehaviour
{
    public W.ComUIInventory inventory;

    public List<W.ItemData> newItemDatas;

    private void Start()
    {
        //foreach(var itemData in newItemDatas)
        //{
        //    OnAddItem(itemData);
        //}
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Q))
        {
            OnAddItem(ItemRandom);
        }

        if(Input.GetKeyUp(KeyCode.W))
        {
            OnRemoveItem(ItemRandom);
        }
    }

    private void OnAddItem(W.ItemData itemData)
    {
        inventory.AddItem(itemData, UintRandom);
    }

    private void OnRemoveItem(W.ItemData itemData)
    {
        inventory.RemoveItem(itemData, UintRandom);
    }

    private W.ItemData ItemRandom => newItemDatas[Random.Range(0, newItemDatas.Count)];
    private int UintRandom => Random.Range(1, 5);
}
