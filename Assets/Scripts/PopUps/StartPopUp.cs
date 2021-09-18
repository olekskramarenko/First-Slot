using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class StartPopUp : MonoBehaviour, IPopUp
{
    [SerializeField] private Image shadowImage;
    [SerializeField] private RectTransform freeSpinsStartRT;
    [SerializeField] private PopUpsController popUpsController;
    private float fadeValue = 0.65f;
    private int showingTime = 2;

    public void ShowPopUp()
    {
        freeSpinsStartRT.DOScale(1, 1);
        shadowImage.DOFade(fadeValue, 1);
        shadowImage.raycastTarget = true;
        StartCoroutine(ShowPopUpAndWait());
    }
    IEnumerator ShowPopUpAndWait()
    {
        yield return new WaitForSecondsRealtime(showingTime);
        ClosePopUp();
        popUpsController.ShowSpinsCounter();
    }
    public void ClosePopUp()
    {
        freeSpinsStartRT.DOScale(0, 1);
        shadowImage.DOFade(0, 1);
        shadowImage.raycastTarget = false;
    }

}
