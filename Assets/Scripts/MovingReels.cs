using UnityEngine;
using DG.Tweening;


public class MovingReels : MonoBehaviour
{
    [SerializeField] private Ease easeStart, easeWay, easeStop;
    [SerializeField] private RectTransform[] allReelsRT; 
    [SerializeField] private FinalResult FinalResult;
    [SerializeField] private float delay;
    [Range(0, 10)] [SerializeField] private float timeStart, timeWay, timeStop;
    [SerializeField] private GameObject playButton, stopButton;
    [SerializeField] private float symbolHeight;
    [SerializeField] private int symbolsCount;
    [SerializeField] private MovingSymbols[] MovingSymbols;
    private bool[] slowDownIsActive;
    private float[] startReelPosition, fullReelDistance;
    private readonly float distanceStart = 2;
    private readonly float distanceWay = 12;
    private readonly float distanceStop = 1; // Важно, при значении 1, будут показываться верные финальные экраны

    private void Start()
    {
        stopButton.SetActive(false);
        startReelPosition = new float[allReelsRT.Length];
        fullReelDistance = new float[allReelsRT.Length];
        slowDownIsActive = new bool[allReelsRT.Length];
    }
    public void MovingStart()
    {
        playButton.SetActive(false);
        for (int i = 0; i < allReelsRT.Length; i++)
        {
            var reel = allReelsRT[i];
            var index = i; // Для корректной работы счетчика циклов вне цикла
            slowDownIsActive[index] = false;
            startReelPosition[index] = reel.localPosition.y;
            reel.DOAnchorPosY(-(fullReelDistance[index]+(distanceStart * symbolHeight * symbolsCount)), timeStart)
                .SetDelay(i * delay)
                .SetEase(easeStart)
                .OnComplete(() => MovingWay(reel, index));
        }
    }
    private void MovingWay(RectTransform reel, int index)
    {
        stopButton.SetActive(true);
        float previousDistance = (distanceWay + distanceStart) * symbolHeight * symbolsCount;
        DOTween.Kill(reel);
        reel.DOAnchorPosY(-(fullReelDistance[index] + previousDistance), timeWay)
            .SetEase(easeWay)
            .OnComplete(() => MovingSlowDown(reel, index, previousDistance));
    }
    private void MovingSlowDown(RectTransform reel, int index, float previousDistance)
    {
        if (!playButton.activeSelf)
        {
            stopButton.SetActive(false); 
        }
        slowDownIsActive[index] = true;
        DOTween.Kill(reel);
        reel.DOAnchorPosY(-(fullReelDistance[index] + previousDistance + (distanceStop  * symbolHeight * symbolsCount)), timeStop, true)
            .SetEase(easeStop)
            .OnComplete(() => SetSymbolDefaultPosition(reel, index));
    }
    private void SetSymbolDefaultPosition(RectTransform reel, int index)
    {
        var finalReelPosition = reel.localPosition.y;
        var lastSpinDistance = -(finalReelPosition - startReelPosition[index]);
        fullReelDistance[index] += lastSpinDistance;
        MovingSymbols[index].ResetSymbolReelsCounter();
        if (!playButton.activeSelf && index == 2)
        {
            playButton.SetActive(true);
            stopButton.SetActive(false);
        }
        if (index == allReelsRT.Length-1)
        {
            FinalResult.SetNextFinalScreen();
        }
    }

    public void MovingStop()
    {
        stopButton.SetActive(false);
        for (int i = 0; i < allReelsRT.Length; i++)
        {
            var reel = allReelsRT[i];
            var index = i;
            DOTween.Kill(reel);
            var distBeforeStopPressed = -(reel.localPosition.y - startReelPosition[index]);
            var correctedSymbolsDist = CalculateCorrectSymbolsDist(distBeforeStopPressed);
            reel.DOAnchorPosY(-(fullReelDistance[index] + correctedSymbolsDist), 0.1f)
            .SetEase(easeWay)
            .OnComplete(() => MovingSlowDown(reel, index, correctedSymbolsDist));
        } 
    }

    private float CalculateCorrectSymbolsDist(float distBeforeStopPressed) 
    {
        float correctedSymbolsDist;
        correctedSymbolsDist = Mathf.Ceil(distBeforeStopPressed / symbolHeight) * symbolHeight;
        return correctedSymbolsDist;
    }

    public bool GetSlowDownStatus(int reelId)
    {
            return slowDownIsActive[reelId];
    }
}
