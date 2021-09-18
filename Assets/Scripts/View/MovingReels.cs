using UnityEngine;
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
    [SerializeField] private float symbolHeight;
    [SerializeField] private int symbolsCount;
    [SerializeField] private WinLinesChecker winLinesChecker;
    [SerializeField] private ReelsStateController reelStateController;
    [SerializeField] private AnticipationController anticipationController;
    private readonly float distanceStart = 2;
    private readonly float distanceWay = 12;
    private readonly float distanceLongSpin = 12000;
    private readonly float timeLongSpin = 4;
    private readonly float distanceStop = 0.75f; // Important, value 0.75 nedded for correct showing final screens
    private Dictionary<RectTransform, MovingSymbols> reelsDictionary;

    public delegate void ChangeStateEvent(ReelStates reelState);
    public static event ChangeStateEvent OnStateChanged;

    public delegate void PlaySoundEvent(SoundType sound);
    public static event PlaySoundEvent OnSoundPLayed;


    private void Start()
    {
        if (OnStateChanged != null) OnStateChanged(ReelStates.ReadyForSpin);
        reelsDictionary = new Dictionary<RectTransform, MovingSymbols>();
        for (int i = 0; i < allReelsRT.Length; i++)
        {
            reelsDictionary.Add(allReelsRT[i], MovingSymbols[i]);
        }
    }
    public void MovingStart()
    {
        if (OnStateChanged != null) OnStateChanged(ReelStates.StartSpin);
        for (int i = 0; i < allReelsRT.Length; i++)
        {
            var reel = allReelsRT[i];
            reelsDictionary[reel].SlowDownStatus = false;
            reelsDictionary[reel].StartReelPos = reel.localPosition.y;
            reel.DOAnchorPosY(-(reelsDictionary[reel].FullSpinDistance + (distanceStart * symbolHeight * symbolsCount)), timeStart)
                .SetDelay(i * delay)
                .SetEase(easeStart)
                .OnComplete(() => MovingWay(reel));
        }
    }
    private void MovingWay(RectTransform reel)
    {
        if (reelsDictionary[reel].ReelId == 0)
        {
            if (OnStateChanged != null) OnStateChanged(ReelStates.Spin);
        }
        float previousDistance = (distanceWay + distanceStart) * symbolHeight * symbolsCount;
        DOTween.Kill(reel);
        reel.DOAnchorPosY(-(reelsDictionary[reel].FullSpinDistance + previousDistance), timeWay)
            .SetEase(easeWay)
            .OnComplete(() => MovingSlowDown(reel, previousDistance));
    }
    private void MovingSlowDown(RectTransform reel, float previousDistance)
    {
        if (reelsDictionary[reel].ReelId == 0)
        {
            if (OnStateChanged != null) OnStateChanged(ReelStates.SlowDown);
        }
        reelsDictionary[reel].SlowDownStatus = true;
        DOTween.Kill(reel);
        reel.DOAnchorPosY(-(reelsDictionary[reel].FullSpinDistance + previousDistance + (distanceStop * symbolHeight * symbolsCount)), timeStop, true)
            .SetEase(easeStop)
            .OnComplete(() => 
            SetSymbolDefaultPosition(reel));
    }
    private void SetSymbolDefaultPosition(RectTransform reel)
    {
        var reelId = reelsDictionary[reel].ReelId;
        if (OnSoundPLayed != null) OnSoundPLayed(SoundType.reelStop);
        if ( reelId == 0 | reelId == 1)
        {
            winLinesChecker.CheckAnticipation(reelId);
        }
        var finalReelPosition = reel.localPosition.y;
        var lastSpinDistance = -(finalReelPosition - reelsDictionary[reel].StartReelPos);
        reelsDictionary[reel].FullSpinDistance += lastSpinDistance;
        reelsDictionary[reel].ResetSymbolReelsCounter();
        if (reelId == allReelsRT.Length - 1)
        {
            if (OnStateChanged != null) OnStateChanged(ReelStates.ResultShowing);
            winLinesChecker.ShowResult();
            FinalResult.SetNextFinalScreen();
            anticipationController.StopLonSpinAnimation();
        }
    }

    public void DoLongSpin()
    {
        var reel = allReelsRT[allReelsRT.Length - 1];
        DOTween.Kill(reel);
        reelsDictionary[reel].SlowDownStatus = false;
        reelsDictionary[reel].ResetSymbolReelsCounter();
        anticipationController.PlayLongSpinAnimation();
        var distBeforeScattersFound = -(reel.localPosition.y - reelsDictionary[reel].StartReelPos);
        var correctedSymbolsDist = CalculateCorrectSymbolsDist(distBeforeScattersFound);
        reel.DOAnchorPosY(-(reelsDictionary[reel].FullSpinDistance + correctedSymbolsDist + distanceLongSpin), timeLongSpin)
            .SetEase(easeWay)
            .OnComplete(() => MovingSlowDown(reel, correctedSymbolsDist + distanceLongSpin));
    }

    private void MovingStop()
    {
        if (OnStateChanged != null) OnStateChanged(ReelStates.ForceStop);
        for (int i = 0; i < allReelsRT.Length; i++)
        {
            var reel = allReelsRT[i];
            DOTween.Kill(reel);
            var distBeforeStopPressed = -(reel.localPosition.y - reelsDictionary[reel].StartReelPos);
            var correctedSymbolsDist = CalculateCorrectSymbolsDist(distBeforeStopPressed);
            reel.DOAnchorPosY(-(reelsDictionary[reel].FullSpinDistance + correctedSymbolsDist), 0)
            .SetEase(easeWay)
            .SetDelay(i * 0.02f)
            .OnComplete(() => MovingSlowDown(reel, correctedSymbolsDist));
        }
    }

    public void DoForceStop()
    {
        if (!reelStateController.FreeSpinsGame & reelStateController.ReelState != ReelStates.ResultShowing)
        {
            MovingStop();
        }
        else
        {
            if (reelStateController.ReelState == ReelStates.Spin)
            {
                MovingStop();
            }
            else if (reelStateController.ReelState == ReelStates.ResultShowing)
            {
                winLinesChecker.StopAnimation();
            }
        }
    }

    private float CalculateCorrectSymbolsDist(float distBeforeStopPressed)
    {
        float correctedSymbolsDist;
        correctedSymbolsDist = Mathf.Ceil(distBeforeStopPressed / symbolHeight) * symbolHeight;
        return correctedSymbolsDist;
    }
}