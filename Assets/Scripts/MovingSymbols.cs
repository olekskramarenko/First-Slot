using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingSymbols : MonoBehaviour
{
    [SerializeField] private GameObject[] allSymbols;
    [SerializeField] private Sprite[] allSymbolImages;
    [SerializeField] private GameObject AllReels;
    [SerializeField] private FinalResult FinalResult;
    [SerializeField] private float symbolHeight;
    [SerializeField] private int symbolsCount;
    private readonly int exitPosition = 223;
    private bool slowDownIsActive;



    void Start()
    {
    }

    void ChangeSymbolAndSprite(bool slowDownIsActive)
    {
        if (!slowDownIsActive)
        {
            for (int i = 0; i < allSymbols.Length; i++)
            {
                var symbol = allSymbols[i];
                if (symbol.transform.position.y <= exitPosition)
                {
                    symbol.transform.position += Vector3.up * symbolHeight * symbolsCount;
                    symbol.GetComponent<Image>().sprite = allSymbolImages[Random.Range(0, allSymbolImages.Length)];
                };
            }
        }
        if (slowDownIsActive)
        {
            for (int i = 0; i < allSymbols.Length; i++)
            {
                var symbol = allSymbols[i];
                int symbolId = allSymbols[i].GetComponent<Symbol>().symbolData.SymbolId;
                int currentFinalScreen = FinalResult.CurrentFinalScreen;
                int finalImageId = FinalResult.GetFinalImageId(symbolId);
                if (symbol.transform.position.y <= exitPosition)
                {
                    symbol.transform.position += Vector3.up * symbolHeight * symbolsCount;
                    symbol.GetComponent<Image>().sprite = allSymbolImages[finalImageId];
                };
            }
        }
    }
     
    void Update()
    {
        slowDownIsActive = AllReels.GetComponent<MovingReels>().SlowDownIsActive;
        ChangeSymbolAndSprite(slowDownIsActive);
    }
}
