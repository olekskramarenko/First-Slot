using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingSymbols : MonoBehaviour
{
    [SerializeField] private Sprite[] allSymbolImages;
    [SerializeField] private GameObject AllReels;



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
                if (child.position.y <= 223)
                {
                    child.position += Vector3.up * 800.0f;
                    child.GetComponent<Image>().sprite = allSymbolImages[Random.Range(0, allSymbolImages.Length)];
                };
            }
        }
        if (AllReels.GetComponent<MovingReels>().SlowDownIsActive)
        {
            //foreach (Transform child in transform)
            //{
            //if (child.position.y <= 223)
            //{
            //    child.position += Vector3.up * 800.0f;
            //    child.GetComponent<Image>().sprite = allSymbolImages[1];
            //};
            //}
            for (int i = 1; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).position.y <= 223)
                {
                    transform.GetChild(i).position += Vector3.up * 800.0f;
                    transform.GetChild(i).GetComponent<Image>().sprite = allSymbolImages[1];
                };
            }

        }

    }
}
