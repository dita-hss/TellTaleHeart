using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// keeps everything related to this sytem in itself
namespace KeySystem
{
    public class KeyInventory : MonoBehaviour
    {
        private HashSet<string> keys = new HashSet<string>();

        public bool HasKey(string key)
        {
            return keys.Contains(key);
        }

        public void AddKey(string key)
        {
            keys.Add(key);
        }

        public void RemoveKey(string key)
        { 
            keys.Remove(key);
        }

    }
}