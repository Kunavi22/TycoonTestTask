using System.Collections.Generic;
using System;
using UnityEngine;
using System.Collections;

[Serializable]
class SerializableDict<TKey, TItem> : IEnumerable<SerializableDictItem<TKey, TItem>>
{
    [SerializeField]
    SerializableDictItem<TKey, TItem>[] items;

    private Dictionary<TKey, TItem> dict;

    public Dictionary<TKey, TItem> ToDictionary()
    {
        if (dict == null)
        {
            dict = new Dictionary<TKey, TItem>();

            foreach (SerializableDictItem<TKey, TItem> item in items)
            {
                dict.Add(item.key, item.value);
            }
        }     

        return dict;
    }

    public IEnumerator<SerializableDictItem<TKey, TItem>> GetEnumerator()
    {
        return new SerializableDictEnumerator(items);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return items.GetEnumerator();
    }

    private class SerializableDictEnumerator : IEnumerator<SerializableDictItem<TKey, TItem>>
    {
        SerializableDictItem<TKey, TItem>[] items;
        int index = -1;

        public SerializableDictEnumerator(SerializableDictItem<TKey, TItem>[] items)
        {
            this.items = items;
        }

        public SerializableDictItem<TKey, TItem> Current => items[index];

        object IEnumerator.Current => Current;

        public void Dispose()
        {

        }

        public bool MoveNext()
        {
            if (index + 1 < items.Length)
            {
                index++;
                return true;
            }
            return false;
        }

        public void Reset()
        {
            index = -1;
        }
    }

}

[Serializable]
class SerializableDictItem<TKey, TItem>
{
    [SerializeField]
    public TKey key;
    [SerializeField]
    public TItem value;
}