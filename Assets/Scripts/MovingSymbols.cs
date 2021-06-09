using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingSymbols : MonoBehaviour
{
    [SerializeField] private Sprite[] allSymbolImages;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform child in transform)
        {
            if (child.position.y <= 223)
            {
                child.position += Vector3.up * 800.0f;
                child.GetComponent<Image>().sprite = allSymbolImages[Random.Range(0, 7)];
            };
        }

    }
}
