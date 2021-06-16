using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingSymbols : MonoBehaviour
{
    [SerializeField] private Sprite[] allSymbolImages;

    void Update()
    {
        foreach (Transform child in transform)
        {
            if (child.position.y <= 223)
            {
                //var symbol = child.gameObject.GetComponent<RectTransform>();
                //print(symbol.rect.height);
                child.position = new Vector3(child.position.x, child.position.y + 800, child.position.z);
                child.GetComponent<Image>().sprite = allSymbolImages[Random.Range(0, allSymbolImages.Length)];
            };
        }

    }
}
