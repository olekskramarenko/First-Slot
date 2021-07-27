using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLinesChecker : MonoBehaviour
{
    [SerializeField] private Symbol[] symbols; // 12 symbols
    [SerializeField] private GameConfig gameConfig;
    [SerializeField] private ReelsStateController reelsStateController;

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
                /*return;*/ // Todo: problem with double values of winLines
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
            //if (winSymbol.SymbolAnimation.isPlaying)
            //{

            //    winSymbol.SymbolAnimation.Play("pulse");
            //}
            winSymbol.SymbolAnimation.Play("pulse");
        }
        foreach (Symbol otherSymbol in otherSymbolsLineList)
        {
            otherSymbol.SymbolAnimation.Play("shadow");
            //otherSymbol.SymbolAnimation.isPlaying;
        }
    }

    public void ShowResult()
    {
        var winLines = gameConfig.WinLines;
        StartCoroutine(WaitAndCheckLines(winLines));
    }

    private void WinLineCheck( WinLinesData winLine)
    {
        var resultsList = GetWinSymbols(winLine.WinLine);
        var winSymbol1 = resultsList.WinSymbolsLineList[0];
        var winSymbol2 = resultsList.WinSymbolsLineList[1];
        var winSymbol3 = resultsList.WinSymbolsLineList[2];
        if (winSymbol1.SymbolType == winSymbol2.SymbolType && winSymbol2.SymbolType == winSymbol3.SymbolType)
        {
            print("### WINLINE FOUND " + winSymbol1 + winSymbol2 + winSymbol3);
            PlayAnimation(resultsList);
        }
    }

    IEnumerator WaitAndCheckLines( WinLinesData[] winLines)
    {
        foreach (var winLine in winLines)
        {
            yield return new WaitUntil(() => !symbols[11].SymbolAnimation.isPlaying);
            WinLineCheck(winLine);
        }
        yield return new WaitUntil(() => !symbols[11].SymbolAnimation.isPlaying);
        reelsStateController.StartGame();
    }

    public void StopSymbolsAnimation()
    { 
        foreach (Symbol symbol in symbols)
        {
            symbol.SymbolAnimation.Stop();
        }
    }
    //public void EnableSymbolsAnimation()
    //{
    //    foreach (Symbol symbol in symbols)
    //    {
    //        symbol.SymbolAnimation.enabled = true;
    //    }
    //}
}
