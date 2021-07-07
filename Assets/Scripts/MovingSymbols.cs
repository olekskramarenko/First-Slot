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
    private bool[] slowDownIsActive;
    private int[] symbolReelsCounter;

    void Start()
    {
        symbolReelsCounter = new int[3];
        slowDownIsActive = new bool[3];
    }
    void ChangeSymbolAndSprite(bool[] slowDownIsActive)
    {
        for (int i = 0; i < allSymbols.Length; i++)
        {
            var symbol = allSymbols[i];
            int reelId = allSymbols[i].GetComponent<Symbol>().symbolData.ReelId;
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
                    //int reelId = allSymbols[i].GetComponent<Symbol>().symbolData.ReelId;
                    int symbolFinalId = symbolReelsCounter[reelId];
                    symbolReelsCounter[reelId]++;
                    int symbolId = (reelId * allSymbols.Length) + (symbolFinalId + 1);
                    print("### reelId = " + reelId + " symbolFinalId = " + symbolFinalId + " symbolId = " + symbolId);
                    int finalImageId = FinalResult.GetFinalImageId(symbolId);
                    symbol.GetComponent<Image>().sprite = allSymbolImages[finalImageId];
                };
            }
        }


        //if (!slowDownIsActive)
        //{
        //    for (int i = 0; i < allSymbols.Length; i++)
        //    {
        //        var symbol = allSymbols[i];
        //        if (symbol.transform.position.y <= exitPosition)
        //        {
        //            symbol.transform.position += Vector3.up * symbolHeight * symbolsCount;
        //            symbol.GetComponent<Image>().sprite = allSymbolImages[Random.Range(0, allSymbolImages.Length)];
        //        };
        //    }
        //}
        //if (slowDownIsActive)
        //{
        //    for (int i = 0; i < allSymbols.Length; i++)
        //    {
        //        var symbol = allSymbols[i];
        //        if (symbol.transform.position.y <= exitPosition)
        //        {
        //            symbol.transform.position += Vector3.up * symbolHeight * symbolsCount;
        //            int reelId = allSymbols[i].GetComponent<Symbol>().symbolData.ReelId;
        //            int symbolFinalId = symbolReelsCounter[reelId];
        //            symbolReelsCounter[reelId]++;
        //            int symbolId = (reelId * allSymbols.Length) + (symbolFinalId + 1);
        //            print("### reelId = " + reelId + " symbolFinalId = " + symbolFinalId + " symbolId = " + symbolId);
        //            int finalImageId = FinalResult.GetFinalImageId(symbolId);
        //            symbol.GetComponent<Image>().sprite = allSymbolImages[finalImageId];
        //        };
        //    } 
        //}
    }
    void Update()
    {
        slowDownIsActive = AllReels.GetComponent<MovingReels>().SlowDownIsActive;

        ChangeSymbolAndSprite(slowDownIsActive);
    }
}
