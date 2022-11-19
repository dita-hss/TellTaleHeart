using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


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


        [Header("Audio")]
        [SerializeField] private AudioDataSO _doorLocked;
        [SerializeField] private AudioDataSO _doorOpen;

        private KeyInventory _keyInventory = null;

        public UnityEvent E_OnOpen = new UnityEvent();

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
                    //_keyInventory.RemoveKey(keyId);
                    //doorAnim.Play(openAnimationName, 0, 0.0f);
                    doorOpen = true;
                    OpenDoor();
                    E_OnOpen.Invoke();
                    //StartCoroutine(PauseDoorInteraction());
                }
                /*else if(doorOpen && !pauseInteraction)
                {
                    doorAnim.Play(closeAnimationName, 0, 0.0f);
                    doorOpen = false;
                    StartCoroutine(PauseDoorInteraction());
                }*/
            }
            else
            {
                SoundManager.Audio?.PlaySFXSound(_doorLocked, transform.position);
                StartCoroutine(ShowDoorLocked());
            }
        }

        public void OpenDoor()
        {
            SoundManager.Audio?.PlaySFXSound(_doorOpen, transform.position);
            Destroy(gameObject);

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
