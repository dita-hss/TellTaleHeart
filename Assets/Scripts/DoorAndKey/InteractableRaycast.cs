using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KeySystem;


public class InteractableRaycast : MonoBehaviour
{


    [SerializeField] private int rayLength = 5;
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private string excluseLayerName = null;


    private Interactable rayCastedObject;
    [SerializeField] private KeyCode openDoorKey = KeyCode.Mouse0;
    [SerializeField] private Image crosshair = null;
    private bool isCrossHairActive;
    private bool doOnce;
    [SerializeField] private KeyInventory _keyInventory;


    private string interactableTag = "InteractiveObject";

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }


    public void Update()
    {
        RaycastHit hit;
        Vector3 fwd = _camera.transform.forward;//transform.TransformDirection(Vector3.forward);


        //int mask = 1 << layerMaskInteract.value; //LayerMask.NameToLayer(excluseLayerName) | layerMaskInteract.value;

        Physics.Raycast(_camera.transform.position, fwd, out hit, rayLength, layerMaskInteract);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag(interactableTag))
            {
                if (!doOnce)
                {
                    rayCastedObject = hit.collider.gameObject.GetComponent<Interactable>();
                    CrosshairChange(true);
                }

                isCrossHairActive = true;
                doOnce = true;

                if (Input.GetKeyDown(openDoorKey))
                {
                    rayCastedObject.Interact(_keyInventory);
                }

            }
        }

        else;
        {
            if (isCrossHairActive)
            {
                CrosshairChange(false);
                doOnce = false;
            }
        }
        
    }


    void CrosshairChange(bool on)
    {
        if (on && !doOnce)
        {
            crosshair.color = Color.red;
        }
        else
        {
            crosshair.color = Color.white;
            isCrossHairActive = false;
        }
    }

}
