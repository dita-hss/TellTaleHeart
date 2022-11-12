using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Billboard : MonoBehaviour
{

    [SerializeField] private Sprite _lookAtSprite;
    [SerializeField] private Sprite _lookAwaySprite; 

    [SerializeField] private GameObject _lookDirReference;
    [SerializeField] private Image _imageToChange;

    private Camera _cam;

    


    private void Start()
    {
        _cam = Camera.main;
    }

    void LateUpdate()
    {
        Vector3 lookPos = _cam.transform.position;
        lookPos.y = transform.position.y;

        if (_imageToChange)
        {
            if (_lookAtSprite != null && Vector3.Angle(_lookDirReference.transform.forward.normalized, _cam.transform.forward.normalized)
                < 90)
            {
                _imageToChange.sprite = _lookAtSprite;
            }
            else if (_lookAwaySprite != null)
            {
                _imageToChange.sprite = _lookAwaySprite;
            }
        }


        transform.LookAt(lookPos);
    }
}
