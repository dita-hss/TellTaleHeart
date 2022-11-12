using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{

    private Camera _cam;

    private void Start()
    {
        _cam = Camera.main;
    }

    void LateUpdate()
    {
        Vector3 lookPos = _cam.transform.position;
        lookPos.y = transform.position.y;
        transform.LookAt(lookPos);
    }
}
