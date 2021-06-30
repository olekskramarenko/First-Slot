using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingSymbols : MonoBehaviour
{
    [SerializeField] private Sprite[] allSymbolImages;
    [SerializeField] private GameObject AllReels;
    [SerializeField] private float symbolHeight;
    [SerializeField] private int symbolsCount;
    private readonly int exitPosition = 223;



    void Start()
    {
        int[][] finalScreens = new int[5][];
        finalScreens[0] = new int[9] { 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        finalScreens[1] = new int[9] { 2, 2, 2, 2, 2, 2, 2, 2, 2 };
        finalScreens[2] = new int[9] { 3, 3, 3, 3, 3, 3, 3, 3, 3 };
        finalScreens[3] = new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        finalScreens[4] = new int[9] { 4, 4, 4, 4, 4, 4, 4, 4, 4 };

    }
     
    void Update()
    {
        if (!AllReels.GetComponent<MovingReels>().SlowDownIsActive)
        {
            foreach (Transform child in transform)
            {
                if (child.position.y <= exitPosition)
                {
                    child.position += Vector3.up * symbolHeight * symbolsCount;
                    child.GetComponent<Image>().sprite = allSymbolImages[Random.Range(0, allSymbolImages.Length)];
                };
            }
        }
        if (AllReels.GetComponent<MovingReels>().SlowDownIsActive)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                if (child.position.y <= exitPosition)
                {
                    child.position += Vector3.up * symbolHeight * symbolsCount;
                    child.GetComponent<Image>().sprite = allSymbolImages[1];
                };
            }
        }
    }
}
