using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;

public class MovingReels : MonoBehaviour
{

    [SerializeField] private RectTransform[] allReelsRT;
    [SerializeField] private MovingSymbols[] MovingSymbols;
    [SerializeField] private FinalResult FinalResult;
    [SerializeField] private float delay;
    [Range(0, 10)] [SerializeField] private float timeStart, timeWay, timeStop;
    [SerializeField] private Ease easeStart, easeWay, easeStop;
    [SerializeField] private Button playButton, stopButton;
    [SerializeField] private RectTransform playButtonRT, stopButtonRT;
    [SerializeField] private float symbolHeight;
    [SerializeField] private int symbolsCount;
    private readonly float distanceStart = 2;
    private readonly float distanceWay = 12;
    private readonly float distanceStop = 0.75f; // Important, value 0.75 nedded for correct showing final screens
    private Dictionary<RectTransform, MovingSymbols> reelsDictionary;


    private void Start()
    {
        stopButton.interactable = false;
        stopButtonRT.localScale = Vector3.zero;
        reelsDictionary = new Dictionary<RectTransform, MovingSymbols>();
        for (int i = 0; i < allReelsRT.Length; i++)
        {
            reelsDictionary.Add(allReelsRT[i], MovingSymbols[i]);
        }
    }
    public void MovingStart()
    {
        playButton.interactable = false;
        playButtonRT.localScale = Vector3.zero;
        stopButtonRT.localScale = Vector3.one;
        for (int i = 0; i < allReelsRT.Length; i++)
        {
            var reel = allReelsRT[i];
            reelsDictionary[reel].ReelState = ReelState.Spin;
            reelsDictionary[reel].StartReelPos = reel.localPosition.y;
            reel.DOAnchorPosY(-(reelsDictionary[reel].FullSpinDistance + (distanceStart * symbolHeight * symbolsCount)), timeStart)
                .SetDelay(i * delay)
                .SetEase(easeStart)
                .OnComplete(() => MovingWay(reel));
        }
    }
    private void MovingWay(RectTransform reel)
    {
        stopButton.interactable = true;
        float previousDistance = (distanceWay + distanceStart) * symbolHeight * symbolsCount;
        DOTween.Kill(reel);
        reel.DOAnchorPosY(-(reelsDictionary[reel].FullSpinDistance + previousDistance), timeWay)
            .SetEase(easeWay)
            .OnComplete(() => MovingSlowDown(reel, previousDistance));
    }
    private void MovingSlowDown(RectTransform reel, float previousDistance)
    {
        stopButton.interactable = false;
        reelsDictionary[reel].ReelState = ReelState.SlowDown;
        DOTween.Kill(reel);
        reel.DOAnchorPosY(-(reelsDictionary[reel].FullSpinDistance + previousDistance + (distanceStop * symbolHeight * symbolsCount)), timeStop, true)
            .SetEase(easeStop)
            .OnComplete(() => SetSymbolDefaultPosition(reel));
    }
    private void SetSymbolDefaultPosition(RectTransform reel)
    {
        var finalReelPosition = reel.localPosition.y;
        var lastSpinDistance = -(finalReelPosition - reelsDictionary[reel].StartReelPos);
        reelsDictionary[reel].FullSpinDistance += lastSpinDistance;
        reelsDictionary[reel].ResetSymbolReelsCounter();
        if (reelsDictionary[reel].ReelId == allReelsRT.Length - 1)
        {
            stopButtonRT.localScale = Vector3.zero;
            playButtonRT.localScale = Vector3.one;
            playButton.interactable = true;
            FinalResult.SetNextFinalScreen();
        }
    }

    public void MovingStop()
    {
        stopButton.interactable = false;
        for (int i = 0; i < allReelsRT.Length; i++)
        {
            var reel = allReelsRT[i];
            var index = i;
            DOTween.Kill(reel);
            var distBeforeStopPressed = -(reel.localPosition.y - reelsDictionary[reel].StartReelPos);
            var correctedSymbolsDist = CalculateCorrectSymbolsDist(distBeforeStopPressed);
            reel.DOAnchorPosY(-(reelsDictionary[reel].FullSpinDistance + correctedSymbolsDist), 0)
            .SetEase(easeWay)
            .OnComplete(() => MovingSlowDown(reel, correctedSymbolsDist));
        }
    }
 
    private float CalculateCorrectSymbolsDist(float distBeforeStopPressed)
    {
        float correctedSymbolsDist;
        correctedSymbolsDist = Mathf.Ceil(distBeforeStopPressed / symbolHeight) * symbolHeight;
        return correctedSymbolsDist;
    }
}
