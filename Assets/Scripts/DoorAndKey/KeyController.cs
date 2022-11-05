using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KeySystem
{

    public class KeyController : MonoBehaviour, Interactable
    {
        [SerializeField] private string keyId = "redKey";
        [SerializeField] private AudioDataSO _onKeyPickupSound;

        public void Interact(KeyInventory inv)
        {
            inv.AddKey(keyId);
            gameObject.SetActive(false);

            
            SoundManager.Audio?.PlaySFXSound(_onKeyPickupSound, transform.position);
        }
        
    }

}
