using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class ResultPopUp : MonoBehaviour, IPopUp
{
    [SerializeField] private Image shadowImage;
    [SerializeField] private RectTransform freeSpinsResultRT;
    [SerializeField] private PopUpsController popUpsController;
    [SerializeField] private PrizeCalculation prizeCalculation;
    [SerializeField] private Text prizeForFSText;
    private float fadeValue = 0.65f;
    private int showingTime = 3;
    private float countingTime = 1.4f;

    public delegate void PlaySoundEvent(SoundType sound);
    public static event PlaySoundEvent OnSoundPLayed;

    public delegate void StopSoundEvent(SoundType sound);
    public static event StopSoundEvent OnSoundStopped;

    public void ShowPopUp()
    {
        freeSpinsResultRT.DOScale(1, 1);
        shadowImage.DOFade(fadeValue, 1);
        shadowImage.raycastTarget = true;
        var prizeFS = prizeCalculation.FreeSpinsPrize;
        prizeForFSText.DOCounter(0, prizeFS, countingTime);
        if (OnSoundPLayed != null) OnSoundPLayed(SoundType.prizeCounter);
        StartCoroutine(ShowPopUpAndWait());
    }
    IEnumerator ShowPopUpAndWait()
    {
        yield return new WaitForSecondsRealtime(showingTime);
        ClosePopUp();
    }
    public void ClosePopUp()
    {
        if (OnSoundPLayed != null) OnSoundStopped(SoundType.freeSpins);
        if (OnSoundPLayed != null) OnSoundPLayed(SoundType.background);
        freeSpinsResultRT.DOScale(0, 1);
        shadowImage.DOFade(0, 1);
        shadowImage.raycastTarget = false;
        prizeCalculation.ResetFreeSpinsPrize();
    }

}
