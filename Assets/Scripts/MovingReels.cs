using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MovingReels : MonoBehaviour
{
    [SerializeField] private Ease easeStart, easeWay, easeStop;
    [SerializeField] private RectTransform[] allReels;
    [SerializeField] private float delay;
    [Range(0, 10)] [SerializeField] private float timeStart, timeWay, timeStop;
    [Range(0, 20)] [SerializeField] private float distanceStart, distanceWay, distanceStop;
    [SerializeField] private GameObject playButton, stopButton;
    [SerializeField] private float symbolHeight;
    [SerializeField] private int symbolsCount;
    private bool slowDownIsActive;
    private float fullDistance;
    private float oneSpinDistance;


    private void Start()
    {
        stopButton.SetActive(false);
        oneSpinDistance = (distanceStop + distanceWay + distanceStart) * symbolHeight * symbolsCount;
    }
    public void MovingStart()
    {
        print($"first-full={fullDistance}, one={oneSpinDistance}");
        slowDownIsActive = false;
        playButton.SetActive(false);
        stopButton.SetActive(true);
        for (int i = 0; i < allReels.Length; i++)
        {
            var reel = allReels[i];
            Tweener tweener = reel.DOAnchorPosY(-(fullDistance+(distanceStart * symbolHeight * symbolsCount)), timeStart)
                .SetDelay(i * delay)
                .SetEase(easeStart)
                .OnComplete(() => MovingWay(reel));
        }
    }
    public void MovingWay(RectTransform reel)
    {
        DOTween.Kill(reel);
        reel.DOAnchorPosY(-(fullDistance+((distanceWay + distanceStart) * symbolHeight * symbolsCount)), timeWay)
            .SetEase(easeWay)
            .OnComplete(() => MovingSlowDown(reel));
    }
    public void MovingSlowDown(RectTransform reel)
    {
        slowDownIsActive = true;
        DOTween.Kill(reel);
        reel.DOAnchorPosY(-(fullDistance+((distanceStop + distanceWay + distanceStart) * symbolHeight * symbolsCount)), timeStop)
            .SetEase(easeStop)
            .OnComplete(() => SetSymbolDefaultPosition(reel));

    }
    public void SetSymbolDefaultPosition(RectTransform reel)
    {
        fullDistance += oneSpinDistance/allReels.Length;
        if (!playButton.activeSelf)
        {
            playButton.SetActive(true);
            stopButton.SetActive(false);
        }
    }
    public void MovingStop()
    {
        playButton.SetActive(true);
        stopButton.SetActive(false);
        foreach (RectTransform reel in allReels)
        {
            DOTween.Kill(reel);
            MovingSlowDown(reel);
        }
    }

    public bool SlowDownIsActive
    {
        get
        {
            return slowDownIsActive;
        }
    }
}
