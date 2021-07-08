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
    [SerializeField] private int symbolsCount, reelId;
    private readonly int exitPosition = 223;
    private bool[] slowDownIsActive;
    private int[] symbolReelsCounter;

    void Start()
    {
        symbolReelsCounter = new int[3];
        slowDownIsActive = new bool[3];
    }

    public void ResetSymbolReelsCounter()
    {
        for ( int i=0; i < symbolReelsCounter.Length; i++)
        {
            symbolReelsCounter[i] = 0;
        }
    }
    void ChangeSymbolAndSprite(bool[] slowDownIsActive)
    {
        for (int i = 0; i < allSymbols.Length; i++)
        {
            var symbol = allSymbols[i];
            if (!slowDownIsActive[reelId])
            {
                if (symbol.transform.position.y <= exitPosition)
                {
                    symbol.transform.position += Vector3.up * symbolHeight * symbolsCount;
                    symbol.GetComponent<Image>().sprite = allSymbolImages[Random.Range(0, allSymbolImages.Length)];
                };
            }
            if (slowDownIsActive[reelId])
            {
                if (symbol.transform.position.y <= exitPosition)
                {
                    symbol.transform.position += Vector3.up * symbolHeight * symbolsCount;
                    int symbolFinalId = symbolReelsCounter[reelId];
                    symbolReelsCounter[reelId]++;
                    int symbolId = (reelId * allSymbols.Length) + (symbolFinalId + 1);
                    int finalImageId = FinalResult.GetFinalImageId(symbolId);
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
