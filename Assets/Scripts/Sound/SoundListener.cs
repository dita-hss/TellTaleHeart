using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoundListener : MonoBehaviour
{
    [SerializeField] private UnityEvent<GameObject> E_OnSoundHeard = new UnityEvent<GameObject>();

    private void Start()
    {
        // https://answers.unity.com/questions/8715/how-do-i-use-layermasks.html
        // Add the sound listener layer to this object's layer, gives the object 2 layers, their original and the soundlistener layer
        gameObject.layer = gameObject.layer | LayerMask.NameToLayer("SoundListener");
    }


    /// <summary>
    /// Adds an action to be done when a sound is heard
    /// </summary>
    /// <param name="action">Action to add (must have a GameObject as the paramter, representing the object that emitted the sound)</param>
    public void AddOnSoundHeardAction(UnityAction<GameObject> action)
    {
        E_OnSoundHeard.AddListener(action);
    }


    /// <summary>
    /// Runs an event that runs all the OnSoundHeard actions
    /// </summary>
    /// <param name="from">Represents the GameObject the sound was heard from</param>
    public void HeardSound(GameObject from)
    {
        E_OnSoundHeard.Invoke(from);
    }

}
