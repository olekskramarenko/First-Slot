using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class PopUpsController : MonoBehaviour
{
    [SerializeField] private Image shadowImage;
    [SerializeField] private RectTransform freeSpinsCounterRT;
    [SerializeField] private RectTransform freeSpinsStartRT;
    [SerializeField] private RectTransform freeSpinsResultRT;
    [SerializeField] private Text spinsLeftText, prizeForFSText;
    [SerializeField] private FreeSpinsController freeSpinsController;
    [SerializeField] private PrizeCalculation prizeCalculation;
    private float fadeValue = 0.65f;

    public Text CounterText { get => spinsLeftText; set => spinsLeftText = value; }

    public delegate void PlaySoundEvent(SoundType sound);
    public static event PlaySoundEvent OnSoundPLayed;

    public delegate void StopSoundEvent(SoundType sound);
    public static event StopSoundEvent OnSoundStopped;

    public void ShowFreeSpinsStart()
    {
        ShowAndCloseStartPopUp();
    }

    private void ShowAndCloseStartPopUp()
    {
        freeSpinsStartRT.DOScale(1, 1);
        shadowImage.DOFade(fadeValue, 1);
        shadowImage.raycastTarget = true;
        StartCoroutine(ShowStartPopUpAndWait());
    }

    IEnumerator ShowStartPopUpAndWait()
    {
        yield return new WaitForSecondsRealtime(2);
        CloseStartPopUp();
        ShowSpinsCounter();
    }

    private void CloseStartPopUp()
    {
        freeSpinsStartRT.DOScale(0, 1);
        shadowImage.DOFade(0, 1);
        shadowImage.raycastTarget = false;
    }

    public void CloseSpinsCounter()
    {
        freeSpinsCounterRT.DOScale(0, 0.5f);
    }

    private void ShowSpinsCounter()
    {
        freeSpinsCounterRT.DOScale(1, 1).OnComplete(() =>
        {
            freeSpinsController.StartAutoSpins();
        });
    }

    public void ShowAndCloseResultPopUp()
    {
        freeSpinsResultRT.DOScale(1, 1);
        shadowImage.DOFade(fadeValue, 1);
        shadowImage.raycastTarget = true;
        var prizeFS = prizeCalculation.FreeSpinsPrize;
        prizeForFSText.DOCounter(0, prizeFS, 1.2f);
        if (OnSoundPLayed != null) OnSoundPLayed(SoundType.prizeCounter);
        StartCoroutine(ShowResultPopUpAndWait());
    }

    IEnumerator ShowResultPopUpAndWait()
    {
        yield return new WaitForSecondsRealtime(3);
        CloseResultPopUp();
    }

    private void CloseResultPopUp()
    {
        if (OnSoundPLayed != null) OnSoundStopped(SoundType.freeSpins);
        if (OnSoundPLayed != null) OnSoundPLayed(SoundType.background);
        freeSpinsResultRT.DOScale(0, 0.5f);
        shadowImage.DOFade(0, 1);
        shadowImage.raycastTarget = false;
        prizeCalculation.ResetFreeSpinsPrize();
    }
}
