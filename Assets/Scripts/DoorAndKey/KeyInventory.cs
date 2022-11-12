using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


// keeps everything related to this sytem in itself
namespace KeySystem
{
    public class KeyInventory : MonoBehaviour
    {
        private HashSet<string> keys = new HashSet<string>();
        public UnityEvent<string, Color> keyAdded = new UnityEvent<string, Color>();
        public UnityEvent<string> keyRemoved = new UnityEvent<string>();

        public bool HasKey(string key)
        {
            return keys.Contains(key);
        }

        public void AddKey(string key)
        {
            keys.Add(key);
            keyAdded.Invoke(key, Color.white);
        }

        public void AddKey(string key, Color outputColor)
        {
            keys.Add(key);
            keyAdded.Invoke(key, outputColor);
        }

        public void RemoveKey(string key)
        { 
            keys.Remove(key);
            keyRemoved.Invoke(key);
        }

    }
}