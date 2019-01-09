using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestConsumer : MonoBehaviour
{
    private GameObjectFactory _gameObjectFactory;
    private SomeObjectFactory _someObjectFactory;
    private SomeObjectMutator _someObjectMutator;
    private Pool<GameObject> _gameObjectPool;
    private Pool<SomeObject> _someObjectPool;

    public List<SomeObject> SomeObjectsPulledFromPool;

    public List<GameObject> ItemsPulledFromPool;
    public List<GameObject> ItemsStillPulledFromPool;
    public List<GameObject> ItemsReturnedTolPool;
    // Use this for initialization
    void Start()
    {
        //Create Factories
        _gameObjectFactory = new GameObjectFactory();
        _someObjectFactory = new SomeObjectFactory();

        //Create Pools
        _gameObjectPool = new Pool<GameObject>(_gameObjectFactory);
        _someObjectPool = new Pool<SomeObject>(_someObjectFactory);

        //Create Mutator
        _someObjectMutator = new SomeObjectMutator();

        //Initialize Pools
        _gameObjectPool.Init(30);
        _someObjectPool.Init(20);

        //Pull items from pool
        GetItemFromPool();
        GetItemFromPool();
        GetItemFromPool();
        GetItemFromPool();
        GetItemFromPool();
        GetItemFromPool();
        GetItemFromPool();

        //Return some items to the pool
        ReturnItemToPool();
        ReturnItemToPool();
        ReturnItemToPool();

        //Do Stuff with SomeObjects
        foreach (var obj in SomeObjectsPulledFromPool)
        {
            Debug.LogErrorFormat("Before Making Some Objects {0} exists", obj.Model.Id);
        }  

        MakeSomeObjectWithModel(new SomeObjectModel { Id = 39 });
        MakeSomeObjectWithModel(new SomeObjectModel { Id = 40 });
        MakeSomeObjectWithModel(new SomeObjectModel { Id = 41 });
        MakeSomeObjectWithModel(new SomeObjectModel { Id = 42 });
        MakeSomeObjectWithModel(new SomeObjectModel { Id = 43 });
        MakeSomeObjectWithModel(new SomeObjectModel { Id = 44 });
        MakeSomeObjectWithModel(new SomeObjectModel { Id = 45 });

        foreach (var obj in SomeObjectsPulledFromPool)
        {
            Debug.LogErrorFormat("After Making Some Objects {0} exists", obj.Model.Id);
        }

        for (int i = SomeObjectsPulledFromPool.Count - 1; i > 4; i--)
        {
            if (SomeObjectsPulledFromPool.Count > 0)
            {
                SomeObject someObject = SomeObjectsPulledFromPool[i];
                ReturnSomeObject(someObject);
            }
        }

        foreach (var obj in SomeObjectsPulledFromPool)
        {
            Debug.LogErrorFormat("After Returning Some Objects {0} exists", obj.Model.Id);
        }

        MakeSomeObjectWithModel(new SomeObjectModel { Id = 51 });
        MakeSomeObjectWithModel(new SomeObjectModel { Id = 52 });


        foreach (var obj in SomeObjectsPulledFromPool)
        {
            Debug.LogErrorFormat("After Making More Some Objects {0} exists", obj.Model.Id);
        }
    }

    void MakeSomeObjectWithModel(SomeObjectModel model)
    {
        SomeObject someObject = _someObjectPool.GetItem();

        if (_someObjectMutator.Mutate(someObject, model))
        {
            SomeObjectsPulledFromPool.Add(someObject);
        }
        else
        {
            throw new System.Exception("FAILED TO MUTATE");
        }
    }
    
    void ReturnSomeObject(SomeObject item)
    {
        if (SomeObjectsPulledFromPool.Contains(item))
        {
            _someObjectPool.ReturnItem(item);
            SomeObjectsPulledFromPool.Remove(item);
        }
    }

    void GetItemFromPool()
    {
        GameObject item = _gameObjectPool.GetItem();
        ItemsPulledFromPool.Add(item);
        ItemsStillPulledFromPool.Add(item);
    }

    void ReturnItemToPool()
    {
        GameObject itemMoving = ItemsStillPulledFromPool[0];
        ItemsReturnedTolPool.Add(itemMoving);
        ItemsStillPulledFromPool.Remove(itemMoving);
        _gameObjectPool.ReturnItem(itemMoving);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
