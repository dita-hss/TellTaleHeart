using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KeySystem
{

    public class KeyController : MonoBehaviour, Interactable
    {
        [SerializeField] private string keyId = "redKey";

        public void Interact(KeyInventory inv)
        {
            inv.AddKey(keyId);
            gameObject.SetActive(false);
        }
        
    }

}
