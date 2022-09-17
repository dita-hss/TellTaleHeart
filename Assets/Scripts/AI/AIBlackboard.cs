using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://forum.unity.com/threads/how-to-use-a-data-blackboard.892612/
public class AIBlackboard : MonoBehaviour
{
    protected Dictionary<string, BBData> data =
        new Dictionary<string, BBData>();


    public T GetValue<T>(string key)
    {
        BBData<T> getVal = Get<T>(key);
        if (getVal != null)
        {
            return getVal.Value;
        }
        return default(T);
    }

    public BBData<T> Get<T>(string key)
    {
        //DataEntity<T> result;
        if (data.TryGetValue(key, out BBData resultRaw))
        {
            return resultRaw as BBData<T>;
        }
        return null;
    }

    public void Set<T>(string key, T value)
    {
        // Set the preexisting value to the new value
        if (data.ContainsKey(key))
        {
            BBData<T> val = Get<T>(key);
            if (val != null)
            {
                val.Value = value;
                return;
            }
        }

        // If no old value, make a new one
        BBData<T> add = new BBData<T>();
        add.Value = value;

        data.Add(key, add);

    }
}

public abstract class BBData
{
    public abstract System.Type GetTypeOfValue();
    public abstract BBData Copy();
};
public class BBData<T> : BBData
{
    T _storedValue;

    public T Value
    {
        get => _storedValue;
        set
        {
            _storedValue = value;
            OnSetEvent?.Invoke(this);
        }
    }

    public override BBData Copy()
    {
        BBData<T> returnVal = new BBData<T>();
        returnVal.Value = Value;

        return returnVal;
    }

    public delegate void onSet(BBData<T> entity);
    /// <summary>
    /// returns the entity when set, so Get<T>() can
    /// be used to get the prefered type
    /// </summary>
    public event onSet OnSetEvent;

    public BBData() { }
    public BBData(T value)
    {
        _storedValue = value;
    }

    public override System.Type GetTypeOfValue()
    {
        return _storedValue.GetType();
    }

    void ResetValue()
    {
        _storedValue = default;
    }
}