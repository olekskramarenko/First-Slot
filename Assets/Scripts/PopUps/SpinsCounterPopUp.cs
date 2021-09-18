using UnityEngine;
using DG.Tweening;

public class SpinsCounterPopUp : MonoBehaviour, IPopUp
{
    [SerializeField] private RectTransform freeSpinsCounterRT;
    [SerializeField] private FreeSpinsController freeSpinsController;
    public void ClosePopUp()
    {
        freeSpinsCounterRT.DOScale(0, 1);
    }

    public void ShowPopUp()
    {
        freeSpinsCounterRT.DOScale(1, 1).OnComplete(() =>
        {
            freeSpinsController.StartAutoSpins();
        });
    }
}
