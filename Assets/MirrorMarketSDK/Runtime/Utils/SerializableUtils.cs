
using System;
using System.Collections.Generic;
using UnityEngine;

public class SerializeDictionary
{
    public static string DicToJson<TKey, TValue>(Dictionary<TKey, TValue> dic)
    {
        return JsonUtility.ToJson(new SerializeDictionary<TKey, TValue>(dic));
    }

    public static Dictionary<TKey, TValue> DicFromJson<TKey, TValue>(string str)
    {
        return JsonUtility.FromJson<SerializeDictionary<TKey, TValue>>(str).ToDictionary();
    }
}

[Serializable]
public class SerializeDictionary<TKey, TValue> : ISerializationCallbackReceiver
{
    [SerializeField]
    List<TKey> keys;
    [SerializeField]
    List<TValue> values;

    Dictionary<TKey, TValue> target;
    public Dictionary<TKey, TValue> ToDictionary() { return target; }

    public SerializeDictionary(Dictionary<TKey, TValue> target)
    {
        this.target = target;
    }

    public void OnBeforeSerialize()
    {
        keys = new List<TKey>(target.Keys);
        values = new List<TValue>(target.Values);
    }

    public void OnAfterDeserialize()
    {
        var count = Math.Min(keys.Count, values.Count);
        target = new Dictionary<TKey, TValue>(count);
        for (var i = 0; i < count; ++i)
        {
            target.Add(keys[i], values[i]);
        }
    }
}

public class SerializeList
{
    public static string ListToJson<T>(List<T> l) 
    {
        return JsonUtility.ToJson(new SerializationList<T>(l));
    }

    public static List<T> ListFromJson<T>(string str) 
    {
        return JsonUtility.FromJson<SerializationList<T>>(str).ToList();
    }
}

[Serializable]
public class SerializationList<T>
{
    [SerializeField]
    List<T> target;
    public List<T> ToList() { return target; }

    public SerializationList(List<T> target)
    {
        this.target = target;
    }
}