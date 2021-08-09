using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class WinLinesChecker : MonoBehaviour
{
    [SerializeField] private Symbol[] symbols; // 12 symbols
    [SerializeField] private GameConfig gameConfig;
    [SerializeField] private ReelsStateController reelsStateController;
    [SerializeField] private PrizeCalculation prizeCalculation;
    [SerializeField] private PrizeAnimator prizeAnimator;
    [SerializeField] private FreeSpinsController freeSpinsController;
    int numberOfReels = 3;

    private ResultsLists GetWinSymbols(int[] winLine)
    {
        var resultsList = new ResultsLists();
        foreach (Symbol symbol in symbols)
        {
            SeparateAndSetSymbols(symbol, winLine, resultsList);
        }
        return resultsList;
    }

    private ResultsLists SeparateAndSetSymbols(Symbol symbol, int[] winLine, ResultsLists resultsList)
    {
        bool coincidence = false;
        foreach (var value in winLine)
        {
            if (symbol.SymbolFinalId == value)
            {
                resultsList.WinSymbolsLineList.Add(symbol);
                coincidence = true;
                /*return;*/ // Todo: may be problem with double values of winLines
            }
        }
        if (!coincidence)
        {
            resultsList.OtherSymbolsLineList.Add(symbol);
        }
        return resultsList;
    }

    private void PlayAnimation(ResultsLists resultList)
    {
        var winSymbolsLineList = resultList.WinSymbolsLineList;
        var otherSymbolsLineList = resultList.OtherSymbolsLineList;
        foreach (Symbol winSymbol in winSymbolsLineList)
        {
            winSymbol.SymbolAnimation.Play("pulse");
            winSymbol.ParticleSystem.Play();
        }
        foreach (Symbol otherSymbol in otherSymbolsLineList)
        {
            otherSymbol.SymbolAnimation.Play("shadow");
        }
    }

    public void ShowResult()
    {
        var winLines = gameConfig.WinLines;
        StartCoroutine(WaitAndCheckLines(winLines));
    }

    private void WinLineCheck(WinLinesData winLine)
    {
        var resultsList = GetWinSymbols(winLine.WinLine);
        var winSymbol1 = resultsList.WinSymbolsLineList[0];
        var winSymbol2 = resultsList.WinSymbolsLineList[1];
        var winSymbol3 = resultsList.WinSymbolsLineList[2];
        if (winSymbol1.SymbolType == winSymbol2.SymbolType && winSymbol2.SymbolType == winSymbol3.SymbolType)
        {
            PlayAnimation(resultsList);
            prizeCalculation.CalculatePrize(resultsList.WinSymbolsLineList);
        }
    }

    IEnumerator WaitAndCheckLines(WinLinesData[] winLines)
    {
        foreach (var winLine in winLines)
        {
            yield return new WaitUntil(() => !symbols[symbols.Length - 1].SymbolAnimation.isPlaying);
            WinLineCheck(winLine);
        }
        yield return new WaitUntil(() => !symbols[symbols.Length - 1].SymbolAnimation.isPlaying);
        prizeAnimator.UpdatePrizeCounter();
        reelsStateController.StartGame();
        CheckScatters();
    }

    private void CheckScatters()
    {
        bool[] scatterOnReels = CheckScattersOnEachReel();
        bool threeScattersFound = Array.TrueForAll(scatterOnReels, value => value == true);
        if (threeScattersFound)
        {
            print("### threeScattersFound " + scatterOnReels);
            freeSpinsController.StartFreeSpins();

        };

    }

    private bool[] CheckScattersOnEachReel()
    {
        bool[] scatterOnReels = new bool[numberOfReels];
        foreach (Symbol symbol in symbols)
        {
            if (symbol.SymbolFinalId == 0 | symbol.SymbolFinalId == 1 | symbol.SymbolFinalId == 2)
            {
                if (symbol.SymbolType == SymbolType.scatter)
                {
                    scatterOnReels[0] = true;
                }
            }
            else if (symbol.SymbolFinalId == 4 | symbol.SymbolFinalId == 5 | symbol.SymbolFinalId == 6)
            {
                if (symbol.SymbolType == SymbolType.scatter)
                {
                    scatterOnReels[1] = true;
                }
            }
            else if (symbol.SymbolFinalId == 8 | symbol.SymbolFinalId == 9 | symbol.SymbolFinalId == 10)
            {
                if (symbol.SymbolType == SymbolType.scatter)
                {
                    scatterOnReels[2] = true;
                }
            }
        }
        return scatterOnReels;
    }


}
