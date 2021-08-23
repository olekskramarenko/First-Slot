using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLinesChecker : MonoBehaviour
{
    [SerializeField] private Symbol[] symbols; // 12 symbols
    [SerializeField] private GameConfig gameConfig;
    [SerializeField] private ReelsStateController reelsStateController;
    [SerializeField] private PrizeCalculation prizeCalculation;
    [SerializeField] private PrizeAnimator prizeAnimator;

    public delegate void ChangeStateEvent(ReelStates reelState);
    public static event ChangeStateEvent OnStateChanged;

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

        if (OnStateChanged != null) OnStateChanged(ReelStates.ReadyForSpin);

        prizeAnimator.UpdatePrizeCounter();
        

    }
}
