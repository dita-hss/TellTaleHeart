using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KeySystem
{
    public class KeyDoorController : MonoBehaviour, Interactable
    {

        private Animator doorAnim;

        private bool doorOpen = false;

        [SerializeField] private string keyId = "redKey";

        [Header("Animation Names")]
        [SerializeField] private string openAnimationName = "DoorOpen";
        [SerializeField] private string closeAnimationName = "DoorClose";

        [SerializeField] private int timeToShowUI = 1;
        [SerializeField] private GameObject showDoorLockedUI = null;
        
        [SerializeField] private int waitTimer = 1;
        [SerializeField] private bool pauseInteraction = false;

        private KeyInventory _keyInventory = null;

        public void Interact(KeyInventory inv)
        {
            PlayAnimation();
        }




        private void Awake()
        {
            doorAnim = gameObject.GetComponent<Animator>();
            _keyInventory = GameObject.FindObjectOfType<KeyInventory>();
        }

        private IEnumerator PauseDoorInteraction()
        {
            pauseInteraction = true;
            yield return new WaitForSeconds(waitTimer);
            pauseInteraction = false;
            // Deleting object for now
            Destroy(gameObject);
        }

        public void PlayAnimation()
        {
            if (_keyInventory.HasKey(keyId))
            {

                
                if (!doorOpen && !pauseInteraction)
                {
                    doorAnim.Play(openAnimationName, 0, 0.0f);
                    doorOpen = true;
                    StartCoroutine(PauseDoorInteraction());
                }
                else if(doorOpen && !pauseInteraction)
                {
                    doorAnim.Play(closeAnimationName, 0, 0.0f);
                    doorOpen = false;
                    StartCoroutine(PauseDoorInteraction());
                }
            }
            else
            {
                StartCoroutine(ShowDoorLocked());
            }
        }

        IEnumerator ShowDoorLocked()
        {
            if (showDoorLockedUI != null)
            {
                showDoorLockedUI?.SetActive(true);
                yield return new WaitForSeconds(timeToShowUI);
                showDoorLockedUI?.SetActive(false);
            }

        }
    }
}
