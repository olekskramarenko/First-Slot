using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PrizeAnimator : MonoBehaviour
{
    [SerializeField] private Text prizeText;
    [SerializeField] private PrizeCalculation prizeCalculation;
    [SerializeField] private Symbol[] symbols;
    [SerializeField] private float forwardScale, pulseScale, symbolsFade, animTime;
    private int prevPrize = 0;
    private bool isAnimPlaying;
    private bool isStopPushed;

    public bool IsAnimPlaying { get => isAnimPlaying; set => isAnimPlaying = value; }
    public bool IsStopPushed { get => isStopPushed; set => isStopPushed = value; }

    public delegate void PlaySoundEvent(SoundType sound);
    public static event PlaySoundEvent OnSoundPLayed;

    public void UpdatePrizeCounter()
    {
        var prize = prizeCalculation.TotalPrize;
        if (prevPrize != prize) 
        {
            prizeText.DOCounter(prevPrize, prize, 1);
            if (OnSoundPLayed != null) OnSoundPLayed(SoundType.prizeCounter);
            prevPrize = prize;
        }
    }
    public void PlayWinAnimation(ResultsLists resultList)
    {
        if (isStopPushed) return;
        if (OnSoundPLayed != null) OnSoundPLayed(SoundType.prize);
        var winSymbolsLineList = resultList.WinSymbolsLineList;
        var otherSymbolsLineList = resultList.OtherSymbolsLineList;
        foreach (Symbol winSymbol in winSymbolsLineList)
        {
            isAnimPlaying = true;
            winSymbol.ParticleSystem.Play();
            var symbolRT = winSymbol.SymbolRT;
            symbolRT.DOScale(forwardScale, animTime / 3).OnComplete(() =>
              {
                  symbolRT.DOScale(pulseScale, animTime / 6).SetLoops(2, LoopType.Yoyo).OnComplete(() =>
                  {
                      symbolRT.DOScale(1, animTime / 3).OnComplete(() =>
                      {
                          isAnimPlaying = false;
                      });
                  });
              });
        }
        foreach (Symbol otherSymbol in otherSymbolsLineList)
        {
            var image = otherSymbol.SymbolImage;
            image.DOFade(symbolsFade, animTime / 6).OnComplete(() =>
            {
                image.DOFade(symbolsFade, animTime / 1.6f).OnComplete(() =>
                {
                    image.DOFade(1, animTime / 6);
                });
            });
        }
    }

    public void PLaySmallAnimation(Symbol symbol)
    {
        if (OnSoundPLayed != null) OnSoundPLayed(SoundType.scatter);
            var symbolRT = symbol.SymbolRT;
            symbolRT.DOScale(forwardScale, animTime / 3).OnComplete(() =>
            {
                symbolRT.DOScale(pulseScale, animTime / 6).SetLoops(1, LoopType.Yoyo).OnComplete(() =>
                {
                    symbolRT.DOScale(1, animTime / 3);
                });
            });
    }
    public void StopWinAnimation()
    {
        isStopPushed = true;
        foreach (Symbol symbol in symbols)
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
