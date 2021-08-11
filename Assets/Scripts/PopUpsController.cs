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

    public Text CounterText { get => spinsLeftText; set => spinsLeftText = value; }

    public void ShowFreeSpinsStart()
    {
        ShowAndCloseStartPopUp();
    }

    private void ShowAndCloseStartPopUp()
    {
        freeSpinsStartRT.DOScale(1, 2);
        shadowImage.DOFade(0.65f, 1);
        shadowImage.raycastTarget = true;
        StartCoroutine(ShowStartPopUpAndWait());
    }

    IEnumerator ShowStartPopUpAndWait()
    {
        yield return new WaitForSecondsRealtime(1);
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
}
