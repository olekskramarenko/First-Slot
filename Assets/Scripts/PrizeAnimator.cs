using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PrizeAnimator : MonoBehaviour
{
    [SerializeField] private Text prizeText;
    [SerializeField] private PrizeCalculation prizeCalculation;
    [SerializeField] private Symbol[] symbols;
    private int prevPrize = 0;
    private bool isAnimPlaying;
    private bool isStopPushed;

    public bool IsAnimPlaying { get => isAnimPlaying; set => isAnimPlaying = value; }
    public bool IsStopPushed { get => isStopPushed; set => isStopPushed = value; }

    public void UpdatePrizeCounter()
    {
        var prize = prizeCalculation.TotalPrize;
        if (prevPrize != prize)
        {
            prizeText.DOCounter(prevPrize, prize, 1);
            prevPrize = prize;
        }
    }
    public void PlayWinAnimation(ResultsLists resultList)
    {
        if (isStopPushed) return;
        var winSymbolsLineList = resultList.WinSymbolsLineList;
        var otherSymbolsLineList = resultList.OtherSymbolsLineList;
        foreach (Symbol winSymbol in winSymbolsLineList)
        {
            isAnimPlaying = true;
            winSymbol.ParticleSystem.Play();
            var symbolRT = winSymbol.SymbolRT;
            symbolRT.DOScale(1.2f, 0.8f).OnComplete(() =>
            {
                symbolRT.DOScale(1.06f, 0.4f).SetLoops(2, LoopType.Yoyo).OnComplete(() =>
                {
                    symbolRT.DOScale(1, 0.8f).OnComplete(() =>
                    {
                        isAnimPlaying = false;
                    });
                });
            });
        }
        foreach (Symbol otherSymbol in otherSymbolsLineList)
        {
            //otherSymbol.SymbolAnimation.Play("shadow");
            var image = otherSymbol.SymbolImage;
            image.DOFade( 0.31f, 0.4f).OnComplete(()=> 
            {
                image.DOFade(0.31f, 1.5f).OnComplete(() => 
                {
                    image.DOFade(1, 0.4f);
                });
            });
        }
    }

    public void StopWinAnimation()
    {
        isStopPushed = true;
        foreach ( Symbol symbol in symbols)
        {
            DOTween.Kill(symbol.SymbolRT);
            DOTween.Kill(symbol.SymbolImage);
            symbol.SymbolRT.DOScale(1, 0.1f);
            symbol.SymbolImage.DOFade(1, 0.1f);
            symbol.ParticleSystem.Stop();
        }
        isAnimPlaying = false;
    }
}
