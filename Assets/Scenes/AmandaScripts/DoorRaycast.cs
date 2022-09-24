using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DoorRaycast : MonoBehaviour
{
    [SerializeField] private int rayLength = 5;
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private string excludeLayerName = null;


    private MyDoorController rayCastedObj;

    [SerializeField] private KeyCode openDoorKey = KeyCode.Mouse0;
    [SerializeField] private Image crosshair = null;

    private bool isCrossHairActive;
    private bool doOnce;

    private const string interactableTag = "InteractiveObject";


    private void Update()
    {
    
    }


}
