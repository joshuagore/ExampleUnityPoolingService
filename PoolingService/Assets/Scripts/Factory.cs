using UnityEngine;
using System.Collections;

public interface IFactory<T>
{
    T CreateItem { get; }
}

public class SomeObjectFactory : IFactory<SomeObject>
{
    private int currentId = 0; 
    public SomeObject CreateItem
    {
        get
        {
            SomeObject someObject = new SomeObject(currentId);
            currentId++;
            return someObject;
        }
    }
}

public class GameObjectFactory : IFactory<GameObject>
{
    private int currentId = 0;
    public GameObject CreateItem
    {
        get
        {
            GameObject gameObject = new GameObject(name:string.Format("{0}", currentId));
            currentId++;
            return gameObject;
        }
    }
}