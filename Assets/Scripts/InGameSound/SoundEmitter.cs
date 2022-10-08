using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEmitter : MonoBehaviour
{
    public float range = 5.0f;
    private LayerMask layerMask;

    private void Awake()
    {
        // Bitshifting required to set LayerMask
        layerMask = 1 << LayerMask.NameToLayer("SoundListener");
    }


    /// <summary>
    /// Emits a sound, the range is dependent on the SoundEmitter's "range" variable
    /// </summary>
    public void EmitSound()
    {
        Collider[] listenersHit = Physics.OverlapSphere(transform.position, range, layerMask);

        foreach (Collider hit in listenersHit)
        {
            // Ignore this object (just in case it has an emitter and a listener)
            if (hit.gameObject.Equals(gameObject)) { continue; }

            // If what we hit has a soundlistener (which it should), then make it run its HeardSound event
            SoundListener hitListener = hit.gameObject.GetComponent<SoundListener>();
            hitListener?.HeardSound(transform.position);

        }
    }


    private void OnDrawGizmosSelected()
    {
        // Display the range in editor
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }


}