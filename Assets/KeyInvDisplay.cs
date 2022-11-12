using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeySystem;
using UnityEngine.UI;
using StarterAssets;

public class KeyInvDisplay : MonoBehaviour
{
    [SerializeField] private Sprite keyImage;
    private GameObject _playerCapsule;
    


    private KeyInventory keyInv;

    private List<KeyImageData> displayKeys = new List<KeyImageData>();
    private List<Image> displayImages = new List<Image>();
    [SerializeField] RectTransform initKeyPos;

    [SerializeField] float keyScale = .1f;
    [SerializeField] float keyOffset = 16.0f; 

    struct KeyImageData
    {
        public Color col;
        public string name; 

        public KeyImageData(Color col, string name)
        {
            this.name = name;
            this.col = col; 
        }
    }

    private 

    // Start is called before the first frame update
    void Start()
    {

        _playerCapsule = FindObjectOfType<FirstPersonController>().gameObject;

        if (_playerCapsule)
        {
            keyInv = _playerCapsule.GetComponent<KeyInventory>();

            keyInv?.keyAdded.AddListener((name, col) => { AddDisplayKey(new KeyImageData(col, name)); });
            keyInv?.keyRemoved.AddListener((name) => { RemoveDisplayKey(GetDataFromName(name)); });
        }
    }

    private void AddDisplayKey(KeyImageData data)
    {
        GameObject newDisplay = new GameObject("Key display " + data.name);
        Image displayImg = newDisplay.AddComponent<Image>();

        displayImg.sprite = keyImage;
        displayImg.color = data.col;
        newDisplay.transform.localRotation = Quaternion.Euler(0, 0, 45);
        newDisplay.transform.localScale = new Vector3(keyScale, keyScale);
        displayImg.rectTransform.sizeDelta = new Vector2(keyImage.rect.width, keyImage.rect.height);

        // displayImg.rectTransform.pivot.

        

        displayImages.Add(displayImg);
        displayKeys.Add(data);

        RepositionSprites();

    }

    private KeyImageData GetDataFromName(string name)
    {
        foreach (KeyImageData data in displayKeys) {
            if (data.name == name)
            {
                return data; 
            }
        }
        return default; 
    }


    private void RemoveDisplayKey(KeyImageData data)
    {
        int index = displayKeys.IndexOf(data);


        Image img = displayImages[index];


        displayKeys.RemoveAt(index);
        displayImages.RemoveAt(index);

        Destroy(img.gameObject);

        RepositionSprites();

    }


    private void RepositionSprites()
    {

        for (int i = 0; i < displayImages.Count; i++)
        {
            Image displayImg = displayImages[i];

            displayImg.rectTransform.parent = initKeyPos;
            if (i > 0)
            {
                displayImg.rectTransform.localPosition = displayImages[i - 1].rectTransform.localPosition
                    + new Vector3((keyImage.bounds.size.x * keyImage.pixelsPerUnit * displayImages[i - 1].rectTransform.localScale.x) + keyOffset, 0, 0);
            } else
            {
                displayImg.rectTransform.parent = initKeyPos;
                displayImg.rectTransform.localPosition = Vector3.zero;
            }
        }
    }
}
