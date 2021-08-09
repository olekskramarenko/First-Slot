using System.Collections;
using UnityEngine;

public class FreeSpinsController : MonoBehaviour
{
    [SerializeField] private MovingReels movingReels;
    [SerializeField] private PopUpsController popUpsController;
    [SerializeField] private ReelsStateController reelsStateController;
    [SerializeField] private int numberOfFreeSpins;
    private int freeSpinsCounter;

    public void StartFreeSpins()
    {
        popUpsController.ShowFreeSpinsStart();
        freeSpinsCounter = numberOfFreeSpins;
        popUpsController.CounterText.text = freeSpinsCounter.ToString();
        reelsStateController.FreeSpinsGame = true;
    }

    public void StartAutoSpins()
    {
        StartCoroutine(WaitAndStartAutoSpin());
    } 

    IEnumerator WaitAndStartAutoSpin()
    { 
        while ( freeSpinsCounter > 0)
        {
            yield return new WaitUntil(() => reelsStateController.ReelState == ReelStates.ReadyForSpin);
            movingReels.MovingStart();
            freeSpinsCounter--;
            popUpsController.CounterText.text = freeSpinsCounter.ToString();
        }
        if (freeSpinsCounter == 0)
        {
            reelsStateController.FreeSpinsGame = false;
            yield return new WaitUntil(() => reelsStateController.ReelState == ReelStates.ReadyForSpin);
            FinishFreeSpins();
        }
    }

    private void FinishFreeSpins()
    {
        popUpsController.CloseSpinsCounter();
        freeSpinsCounter = numberOfFreeSpins;
    }
}
