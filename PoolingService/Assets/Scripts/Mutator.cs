using UnityEngine;
using System.Collections;

public interface Mutator<T, K>
{
    bool Mutate(T item, K model);
}

public class SomeObjectMutator : Mutator<SomeObject, SomeObjectModel>
{
    public bool Mutate(SomeObject item, SomeObjectModel model)
    {
        item.UpdateModel(model);
        return true;
    }
}
