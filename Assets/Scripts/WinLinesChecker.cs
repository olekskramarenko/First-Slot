using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLinesChecker : MonoBehaviour
{
    [SerializeField] private Symbol[] symbols; // 12 symbols
    [SerializeField] private GameConfig gameConfig;

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

    private void PlayAnimation( List<Symbol> winSymbolsLineList, List<Symbol> otherSymbolsLineList)
    {
        foreach (Symbol winSymbol in winSymbolsLineList)
        {
            winSymbol.SymbolAnimation.Play("pulse");
        }
        foreach (Symbol otherSymbol in otherSymbolsLineList)
        {
            otherSymbol.SymbolAnimation.Play("shadow");
            //otherSymbol.SymbolAnimation.isPlaying;
        }
    }

    public void CheckWinLine()
    {
        var winLines = gameConfig.WinLines;
        foreach (var winLine in winLines)
        {
            var resultsList = GetWinSymbols(winLine.WinLine);
            var winSymbol1 = resultsList.WinSymbolsLineList[0];
            var winSymbol2 = resultsList.WinSymbolsLineList[1];
            var winSymbol3 = resultsList.WinSymbolsLineList[2];

            if (winSymbol1.SymbolType == winSymbol2.SymbolType && winSymbol2.SymbolType == winSymbol3.SymbolType)
            {
                print("### WINLINE FOUND " + winSymbol1 + winSymbol2 + winSymbol3);
                PlayAnimation(resultsList.WinSymbolsLineList, resultsList.OtherSymbolsLineList);
                //foreach (Symbol winSymbol in resultsList.WinSymbolsLineList)
                //{
                //    winSymbol.SymbolAnimation.Play("pulse");
                //}
                //foreach (Symbol otherSymbol in resultsList.OtherSymbolsLineList)
                //{
                //    otherSymbol.SymbolAnimation.Play("shadow");
                //    //otherSymbol.SymbolAnimation.isPlaying;
                //}
            }
        }
    }

    //public void StopSymbolsAnimation()
    //{
    //    foreach (Symbol symbol in symbols)
    //    {
    //        symbol.SymbolAnimation.enabled = false;
    //    }
    //}
    //public void EnableSymbolsAnimation()
    //{
    //    foreach (Symbol symbol in symbols)
    //    {
    //        symbol.SymbolAnimation.enabled = true;
    //    }
    //}
}
