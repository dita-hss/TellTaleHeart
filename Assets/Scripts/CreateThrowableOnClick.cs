using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CreateThrowableOnClick : MonoBehaviour
{
    [SerializeField]
    private Throwable throwObjectPrefab; 
    [SerializeField]
    private Camera camera;
    [SerializeField]
    private float forwardForce = 7.0f;
    private float upwardForce = 4.0f;

    private void Start()
    {
        camera = (camera == null) ? Camera.main : camera;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0 && Mouse.current.leftButton.wasPressedThisFrame)
        {
            CreateThrowable();
        }
    }

    public Throwable CreateThrowable()
    {

        Vector3 cameraDir = camera.transform.forward;

        Throwable returnThrowable = Instantiate(throwObjectPrefab, camera.transform.position + cameraDir, Quaternion.identity, null);

        returnThrowable.Launch((cameraDir * forwardForce) + (Vector3.up * upwardForce));

        return returnThrowable;

    }

}
