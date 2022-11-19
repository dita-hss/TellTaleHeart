using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class MenuButtonLogic : MonoBehaviour, IPointerEnterHandler
{

    [SerializeField] private GameObject hoverBackground;
    private Button _button;
    [SerializeField] private AudioDataSO _buttonClickSound;
    [SerializeField] private AudioDataSO _buttonHoverSound;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverBackground != null)
        {
            hoverBackground.transform.position = transform.position;
        }
        SoundManager.Audio?.PlaySFXSound(_buttonHoverSound, Vector3.zero);
    }

    // Start is called before the first frame update
    void Start()
    {
        _button = GetComponent<Button>();
        
        _button.onClick.AddListener(() => { SoundManager.Audio?.PlaySFXSound(_buttonClickSound, Vector3.zero); });
    }

}
