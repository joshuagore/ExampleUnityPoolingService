using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T>
{
    private List<T> _availablePool;

    private IFactory<T> _factory;

    //Constructor
    public Pool(IFactory<T> factory)
    {
        _factory = factory;
        _availablePool = new List<T>();
    }

    //Public Methods
    public void Init(int numOfItems)
    {
        for (int i = 0; i < numOfItems; i++)
        {
            CreateItem();
        }
    }

    public T GetItem()
    {
        if (_availablePool.Count < 1)
        {
            CreateItem();
        }
        T item = _availablePool[0];
        _availablePool.Remove(item);
        return item;
    }

    public void ReturnItem(T item)
    {
        AddItem(item);
    }

    //Private Methods
    private void CreateItem()
    {
        AddItem(_factory.CreateItem);
    }
    
    private void AddItem(T item)
    {
        _availablePool.Add(item);
    }

    private void RemoveItem(T item)
    {
        _availablePool.Remove(item);
    }
}
