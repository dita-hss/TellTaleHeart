using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeySystem {

    [RequireComponent(typeof(KeyDoorController))]
    public class DoorController : MonoBehaviour, Interactable
    {
        private KeyDoorController doorObject;

        private void Start()
        {
            doorObject = GetComponent<KeyDoorController>();
        }

        public void Interact(KeyInventory inv)
        {
            doorObject.PlayAnimation();
        }
    }
}
