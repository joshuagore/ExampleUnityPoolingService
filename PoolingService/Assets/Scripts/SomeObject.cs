using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SomeObjectModel
{
    public int Id;
}

[System.Serializable]
public class SomeObject
{
    [SerializeField]
    public SomeObjectModel Model;

    public SomeObject(int id)
    {
        Model = new SomeObjectModel();
        Model.Id = id;
    }

    public void UpdateModel(SomeObjectModel model)
    {
        Model = model;
    }
}
